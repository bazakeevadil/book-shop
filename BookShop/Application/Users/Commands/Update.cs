using Application.Contract;
using Application.Shared;
using Domain.Enums;
using Domain.Repositories;
using MediatR;

namespace Application.Users.Commands;

public record UpdateUserCommand : IRequest<UserDto?>
{
    public required string Username { get; init; }

    public required string Password { get; init; }

    public required Role Role { get; init; }
}

internal class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDto?>
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserDto?> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetUserByUsername(command.Username).ConfigureAwait(false);
        if (user is not null)
        {
            user.Username ??= command.Username;
            user.HashPassword ??= command.Password;
            user.Role = command.Role;

            _userRepository.Update(user);
            await _unitOfWork.CommitAsync();

            var response = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Role = user.Role,
            };
            return response;
        }
        return default;
    }
}