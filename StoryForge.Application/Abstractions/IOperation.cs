using MediatR;

namespace StoryForge.Application.Abstractions;

public interface IOperation : IRequest
{
}

public interface IOperation<TResponse> : IRequest<TResponse>
{
}
