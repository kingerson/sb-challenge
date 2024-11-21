namespace SB.Challenge.Application;
using System.Collections.Generic;
using MediatR;

public sealed record GetAllGovernmentEntitiesQuery() : IRequest<IEnumerable<GovernmentEntityViewModel>>;
