namespace SB.Challenge.Infrastructure;
using System;
using System.Threading.Tasks;

public interface IExecutionStrategyWrapper
{
    Task ExecuteAsync(Func<Task> operation);
}
