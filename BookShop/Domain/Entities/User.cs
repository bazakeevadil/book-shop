﻿using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public Role UserRole { get; set; } = new();
}
