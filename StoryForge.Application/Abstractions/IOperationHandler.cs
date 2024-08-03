using MediatR;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Abstractions;

public interface IOperationHandler<TOperation> : IRequestHandler<TOperation, Result>
    where TOperation : IOperation
{
}

public interface IOperationHandler<TOperation, TResponse> : IRequestHandler<TOperation, Result<TResponse>>
    where TOperation : IOperation<TResponse>
{
}
