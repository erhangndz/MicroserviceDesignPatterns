using System.Text;
using System.Text.Json;
using EventSourcing.Shared.Events;
using EventStore.ClientAPI;

namespace EventSourcing.API.EventStores;

public abstract class AbstractStream(IEventStoreConnection eventStoreConnection, string streamName)
{
    protected readonly LinkedList<IEvent> Events = new LinkedList<IEvent>();
    private string _streamName { get; } = streamName;

    private readonly IEventStoreConnection _eventStoreConnection = eventStoreConnection;


    public async Task SaveAsync()
    {
        var newEvents = Events.ToList().Select(e => new EventData(
            Guid.NewGuid(),
            e.GetType().Name,
            true,
            JsonSerializer.SerializeToUtf8Bytes(e, inputType:e.GetType()),
            Encoding.UTF8.GetBytes(e.GetType().FullName)
        )).ToList();

        await _eventStoreConnection.AppendToStreamAsync(
            _streamName,
            ExpectedVersion.Any,
            newEvents
        );

        Events.Clear();
    }
}