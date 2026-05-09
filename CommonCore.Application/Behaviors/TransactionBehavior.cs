using CommonCore.Core.Interfaces;
using MediatR;

namespace CommonCore.Application.Behaviors;

public class TransactionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public TransactionBehavior(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            var response = await next();

            await _unitOfWork.CommitTransactionAsync();

            return response;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();

            throw;
        }
    }
}
