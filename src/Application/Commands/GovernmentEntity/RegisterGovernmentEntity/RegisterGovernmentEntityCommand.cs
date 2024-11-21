namespace SB.Challenge.Application;
using System;
using MediatR;

public class RegisterGovernmentEntityCommand : IRequest<Guid>
{
    public string Name { get; set; }
}
