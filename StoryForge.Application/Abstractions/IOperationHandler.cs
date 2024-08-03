using MediatR;

namespace StoryForge.Application.Abstractions;

public interface IOperationHandler<TOperation> : IRequestHandler<TOperation>
    where TOperation : IOperation
{
}

public interface IOperationHandler<TOperation, TResponse> : IRequestHandler<TOperation, TResponse>
    where TOperation : IOperation<TResponse>
{
}
