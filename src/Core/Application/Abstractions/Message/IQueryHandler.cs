using Application.Libraries;
using MediatR;

namespace Application.Abstractions.Message;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse> { }
