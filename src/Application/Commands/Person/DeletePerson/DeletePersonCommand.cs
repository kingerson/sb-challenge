namespace SB.Challenge.Application;
using System;
using MediatR;

public class DeletePersonCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
