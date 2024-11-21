namespace SB.Challenge.Application;
using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SB.Challenge.Domain;
using SB.Challenge.Infrastructure;

public class RegisterPersonCommandHandler : IRequestHandler<RegisterPersonCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IExecutionStrategyWrapper _executionStrategyWrapper;

    public RegisterPersonCommandHandler(IUnitOfWork unitOfWork, IExecutionStrategyWrapper executionStrategyWrapper)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _executionStrategyWrapper = executionStrategyWrapper ?? throw new ArgumentNullException(nameof(executionStrategyWrapper));
    }

    public async Task<Guid> Handle(RegisterPersonCommand request, CancellationToken cancellationToken)
    {
        var person = new Person();
        person.Register(request.Name, request.LastName, request.Email);

        await _executionStrategyWrapper.ExecuteAsync(async () =>
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    await _unitOfWork.Repository<Person>().Add(person);
                    await _unitOfWork.SaveEntitiesAsync(cancellationToken);
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw new SBChallengeException($"Database Error : {ex.Message}");
                }
            }
        });

        return person.Id;
    }
}
