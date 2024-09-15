using Domain.Abstractions;
using Domain.Abstractions.Commands;
using Domain.Users;

namespace Application.Users;

public sealed record CreateUserCommand(string Email) : ICommand<Result>;

internal sealed class CreateUserCommandHandler(IUserRepository userRepository) : ICommandHandler<CreateUserCommand, Result>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Result> HandleAsync(CreateUserCommand command, CancellationToken cancellationToken = default)
    {
        var emailResult = Email.Init(command.Email);

        if (emailResult.IsFailure)
        {
            return emailResult;
        }

        _userRepository.Add(User.Init(emailResult.Value));

        return Result.Success();
    }
}
