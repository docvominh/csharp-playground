namespace EventSourceWithPostgres.Data.Entity;

public class Event
{
    public int Id { get; set; }
    public long StreamId { get; set; }
    public long Version { get; set; }
    public string? Data { get; set; }


    public Event(long streamId, string data)
    {
        StreamId = streamId;
        Data = data;
    }
}
