using MediatR;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Abstractions;

public interface IOperation : IRequest<Result>
{
}

public interface IOperation<TResponse> : IRequest<Result<TResponse>>
{
}
