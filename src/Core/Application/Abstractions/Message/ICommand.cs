using Application.Libraries;
using MediatR;

namespace Application.Abstractions.Message;

public interface ICommand : IRequest<Result> { }
public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
