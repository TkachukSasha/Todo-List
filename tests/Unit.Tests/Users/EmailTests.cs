using Domain.Abstractions;
using Domain.Users;

namespace Unit.Tests.Users;

public class EmailTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Email_Should_ReturnIsFailure_EmailMustBeProvide_WhenValueIsNullOrWhiteSpaces(string? value)
    {
        Result<Email> email = Email.Init(value);

        email.IsFailure.Should().BeTrue();
        email.Errors.Should().Contain(UserErrors.EmailMustBeProvide);
    }

    [Theory]
    [InlineData("testfdsfsdgsd.com")]
    [InlineData("testfdsfsdgsd.ua")]
    public void Email_Should_ReturnIsFailure_EmailIsInvalid_WhenValueDidntMatchRegex(string value)
    {
        Result<Email> email = Email.Init(value);

        email.IsFailure.Should().BeTrue();
        email.Errors.Should().Contain(UserErrors.EmailIsInvalid);
    }

    [Theory]
    [InlineData("thisisaverylongemailaddress12345678901234567890@domain.com")]
    [InlineData("anotherlong.emailaddress.with.dots.and.hyphens123456789@domain.com")]
    public void Email_Should_ReturnIsFailure_EmailIsOutOfRange_WhenValueIsOutOfRange(string value)
    {
        Result<Email> email = Email.Init(value);

        email.IsFailure.Should().BeTrue();
        email.Errors.Should().Contain(UserErrors.EmailIsOutOfRange);
    }

    [Theory]
    [InlineData("sashatkachuk09@ukr.net")]
    [InlineData("tkachukopersonal@gmail.com")]
    public void Email_Should_ReturnIsSuccess_WhenValueIsValid(string value)
    {
        Result<Email> email = Email.Init(value);

        email.IsFailure.Should().BeFalse();
    }
}
