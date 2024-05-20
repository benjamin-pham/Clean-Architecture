using Application.Libraries;
using MediatR;

namespace Application.Abstractions.Message;

public interface IQuery<TResponse> : IRequest<Result<TResponse>> { }
