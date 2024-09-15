using Domain.Abstractions;
using Domain.Todos;

namespace Unit.Tests.Tasks;

public class NameTests
{
    private static readonly string _name = new string(Enumerable.Range(0, 256).Select(_ => 'a').ToArray());

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Name_Should_ReturnIsFailure_NameMustBeProvide_WhenValueIsNullOrWhiteSpaces(string? value)
    {
        Result<Name> name = Name.Init(value);

        name.IsFailure.Should().BeTrue();
        name.Errors.Should().Contain(expected: TodoItemErrors.NameMustBeProvide);
    }

    [Fact]
    public void Name_Should_ReturnIsFailure_NameIsOutOfRange_WhenValueIsOutOfRange()
    {
        Result<Name> name = Name.Init(_name);

        name.IsFailure.Should().BeTrue();
        name.Errors.Should().Contain(TodoItemErrors.NameIsOutOfRange);
    }

    [Theory]
    [InlineData("Sasha")]
    [InlineData("Oleksandr")]
    public void Name_Should_ReturnIsSuccess_WhenValueIsValid(string value)
    {
        Result<Name> name = Name.Init(value);

        name.IsFailure.Should().BeFalse();
    }
}
