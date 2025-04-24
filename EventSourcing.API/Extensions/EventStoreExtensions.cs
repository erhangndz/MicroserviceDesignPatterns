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

            using var logFactory = LoggerFactory.Create(builder =>
                {
                    builder.SetMinimumLevel(LogLevel.Information);
                    builder.AddConsole();
                });

            var logger = logFactory.CreateLogger("EventStore");
            var connection = EventStoreConnection.Create(connectionString);
            connection.Connected += (sender, args) =>
            {
                logger.LogInformation("EventStore Connection Established");

            };

            connection.ErrorOccurred += (sender, args) =>
            {
                logger.LogError(args.Exception.Message);
            };

        }


    }
}
