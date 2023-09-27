using Domain.Entities;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Requests;

public record GetUserResponseById
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required Role Role { get; init; }
    public Basket? Basket { get; set; }
}

public record GetUserByIdQuery : IRequest<GetUserResponseById?>
{
    public required Guid Id { get; init; }
}

internal class GetUserByIdHandler : IRequestHandler<GetUserByIdQuery, GetUserResponseById?>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<GetUserResponseById?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserById(request.Id).ConfigureAwait(false);
        if (user is not null)
        {
            var response = new GetUserResponseById
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
                Basket = user.Basket,
            };
            return response;
        }
        return default;
    }
}