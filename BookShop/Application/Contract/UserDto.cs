﻿using Domain.Enums;

namespace Application.Contract;

public record UserDto
{
    public required Guid Id { get; init; }
    public required string Username { get; init; }
    public required string Password { get; init; }
    public required string HashPassword { get; init; }
    public required Role Role { get; init; }
    public BasketDto BasketDtos { get; init; } = new();
}
