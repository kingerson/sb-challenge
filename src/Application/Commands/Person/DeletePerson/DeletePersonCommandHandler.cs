namespace SB.Challenge.Application;
using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SB.Challenge.Domain;
using SB.Challenge.Infrastructure;

public class DeletePersonCommandHandler : IRequestHandler<DeletePersonCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;
    public DeletePersonCommandHandler(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));

    public async Task<bool> Handle(DeletePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _unitOfWork.Repository<Person>().GetById(request.Id);

        if (person is null)
            throw new SBChallengeException($"Person with id : {request.Id} not found");

        person.Delete();

        var strategy = _unitOfWork.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            using (var transaction = await _unitOfWork.BeginTransactionAsync())
            {
                try
                {
                    _unitOfWork.Repository<Person>().Update(person);
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

        return true;
    }
}