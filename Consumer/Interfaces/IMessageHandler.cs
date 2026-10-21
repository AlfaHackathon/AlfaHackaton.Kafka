namespace AlfaHackaton.Kafka.Consumer.Interfaces;

public interface IMessageHandler<TMessage>
{
    Task HandlerAsync(TMessage message, CancellationToken cancellationToken);
}