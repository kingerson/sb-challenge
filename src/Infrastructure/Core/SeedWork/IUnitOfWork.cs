namespace SB.Challenge.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using SB.Challenge.Domain;

public interface IUnitOfWork : IDisposable
{
    IRepository<T> Repository<T>() where T : Entity;
    Task<int> SaveEntitiesAsync(CancellationToken cancellationToken);
    Task<IDbContextTransaction> BeginTransactionAsync();
    IExecutionStrategy CreateExecutionStrategy();
}
