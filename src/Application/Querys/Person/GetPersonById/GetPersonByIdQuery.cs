namespace SB.Challenge.Application;
using System;
using MediatR;

public sealed record GetPersonByIdQuery(Guid Id) : IRequest<PersonViewModel>;
