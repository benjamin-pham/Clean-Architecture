using MediatR;

namespace Application.Abstractions.Message;
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
{
}