namespace SB.Challenge.Domain;
using System;

public abstract class Entity
{
    public Guid Id { get; set; }
    public string UserRegister { get; set; }
    public string? UserUpdated { get; set; }
    public DateTime? DateTimeUpdated { get; set; }
    public DateTime DateTimeRegister { get; set; }
    public bool IsActive { get; set; }
}
