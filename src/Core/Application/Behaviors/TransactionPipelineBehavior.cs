using Application.Abstractions;
using Application.Exceptions;
using Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Behaviors;
public class TransactionPipeLineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IApplicationDbContext _dbContext;
    public TransactionPipeLineBehavior(IUnitOfWork unitOfWork, IApplicationDbContext applicationDbContext)
    {
        this.unitOfWork = unitOfWork;
        _dbContext = applicationDbContext;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!IsCommand())
        {
            return await next();
        }
        else
        {
            IExecutionStrategy strategy = _dbContext.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {               
                await unitOfWork.BeginTransactionAsync(cancellationToken);
                try
                {
                    var response = await next();
                    await unitOfWork.SaveAsync(cancellationToken);
                    await unitOfWork.CommitAsync(cancellationToken);
                    return response;
                }
                catch (Exception ex)
                {
                    await unitOfWork.RollBackAsync(cancellationToken);
                    throw new AppException(ex.Message);
                }
                
            });
        }
    }

    private static bool IsCommand()
       => typeof(TRequest).Name.EndsWith("Command");
}
