using Domain.Abstractions.Queries;
using Domain.Abstractions.Queries.Paging;
using Domain.Users;

using Infrastructure.Database;

namespace Application.Users;

public sealed record UserDto(Guid Id, string Email);

public sealed class GetUsersQuery() : PagedQuery<UserDto>;

internal sealed class GetUsersQueryHandler(IUserRepository userRepository) : IQueryHandler<GetUsersQuery, Paged<UserDto>>
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<Paged<UserDto>> HandleAsync(GetUsersQuery query, CancellationToken cancellationToken = default)
    {
        IQueryable<UserDto> users = _userRepository
            .GetAll()
            .Select(u => new UserDto(u.Id, u.Email.Value));

        return await users.PaginateAsync(query, cancellationToken);
    }
}
