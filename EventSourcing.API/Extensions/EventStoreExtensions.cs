using EventStore.ClientAPI;

namespace EventSourcing.API.Extensions
{
    public static class EventStoreExtensions
    {


        public static void AddEventStore(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EventStore");
            services.AddSingleton<IEventStoreConnection>(x =>
            {
                var connection = EventStoreConnection.Create(connectionString);
                connection.ConnectAsync().Wait();
                return connection;
            });
        } 
    }
}
