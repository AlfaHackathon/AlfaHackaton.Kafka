using AlfaHackaton.Kafka.Consumer;
using AlfaHackaton.Kafka.Consumer.Interfaces;
using AlfaHackaton.Kafka.Producer;
using AlfaHackaton.Kafka.Producer.Interfaces;
using AlfaHackaton.Kafka.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AlfaHackaton.Kafka;

public static class DependencyInjection
{
    public static void AddProducer<TMessage>(this IServiceCollection serviceCollection,
        IConfigurationSection configurationSection) 
    {
        serviceCollection.Configure<KafkaSettings>(configurationSection);
        serviceCollection.AddSingleton<IKafkaProducer<TMessage>, KafkaProducer<TMessage>>();
    }

    public static IServiceCollection AddKafka<TMessage, THandler>(this IServiceCollection serviceCollection,
        IConfigurationSection configurationSection) where THandler : class, IMessageHandler<TMessage>
    {
        serviceCollection.Configure<KafkaSettings>(configurationSection);
        serviceCollection.AddHostedService<KafkaConsumer<TMessage>>();
        serviceCollection.AddSingleton<IMessageHandler<TMessage>, THandler>();

        return serviceCollection;
    }
}