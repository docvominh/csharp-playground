using JasperFx.Core;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace MartenEventSource.Controllers;

[ApiController]
[Route("[controller]")]
public class QuickStartController : ControllerBase
{
    private readonly IDocumentStore _store;

    public QuickStartController(IDocumentStore store)
    {
        _store = store;
    }

    [HttpGet("{questId:guid}")]
    public async Task<QuestParty> Get(Guid questId, CancellationToken cancellationToken)
    {
        await using var session = _store.LightweightSession();

        // questId is the id of the stream
        var party = session.Events.AggregateStream<QuestParty>(questId);
        //
        // var party_at_version_3 = await session.Events
        //     .AggregateStreamAsync<QuestParty>(questId, 3);
        //
        // var party_yesterday = await session.Events
        //     .AggregateStreamAsync<QuestParty>(questId, timestamp: DateTime.UtcNow.AddDays(-1));


        return party;
    }

    [HttpPost("start-quest")]
    public async Task SaveStream()
    {
        var questId = Guid.NewGuid();

        await using (var session = _store.LightweightSession())
        {
            var started = new QuestStarted { Name = "Destroy the One Ring" };
            var joined1 = new MembersJoined(1, "Hobbiton", "Frodo", "Sam");

            // Start a brand new stream and commit the new events as
            // part of a transaction
            session.Events.StartStream<Quest>(questId, started, joined1);

            // Append more events to the same stream
            var joined2 = new MembersJoined(3, "Buckland", "Merry", "Pippen");
            var joined3 = new MembersJoined(10, "Bree", "Aragorn");
            var arrived = new ArrivedAtLocation { Day = 15, Location = "Rivendell" };
            session.Events.Append(questId, joined2, joined3, arrived);

            // Save the pending changes to db
            await session.SaveChangesAsync();
        }
    }
}

public class Quest
{
    public Guid Id { get; set; }
}

public class ArrivedAtLocation
{
    public int Day { get; set; }

    public string Location { get; set; }

    public override string ToString()
    {
        return $"Arrived at {Location} on Day {Day}";
    }
}

public class MembersJoined
{
    public MembersJoined()
    {
    }

    public MembersJoined(int day, string location, params string[] members)
    {
        Day = day;
        Location = location;
        Members = members;
    }

    public Guid QuestId { get; set; }

    public int Day { get; set; }

    public string Location { get; set; }

    public string[] Members { get; set; }

    public override string ToString()
    {
        return $"Members {string.Join(", ", Members)} joined at {Location} on Day {Day}";
    }

    protected bool Equals(MembersJoined other)
    {
        return QuestId.Equals(other.QuestId) && Day == other.Day && Location == other.Location &&
               Members.SequenceEqual(other.Members);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((MembersJoined)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(QuestId, Day, Location, Members);
    }
}

public class QuestStarted
{
    public string Name { get; set; }
    public Guid Id { get; set; }

    public override string ToString()
    {
        return $"Quest {Name} started";
    }

    protected bool Equals(QuestStarted other)
    {
        return Name == other.Name && Id.Equals(other.Id);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((QuestStarted)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Id);
    }
}

public class QuestEnded
{
    public string Name { get; set; }
    public Guid Id { get; set; }

    public override string ToString()
    {
        return $"Quest {Name} ended";
    }
}

public class MembersDeparted
{
    public Guid Id { get; set; }

    public Guid QuestId { get; set; }

    public int Day { get; set; }

    public string Location { get; set; }

    public string[] Members { get; set; }

    public override string ToString()
    {
        return $"Members {string.Join(", ", Members)} departed at {Location} on Day {Day}";
    }
}

public class MembersEscaped
{
    public Guid Id { get; set; }

    public Guid QuestId { get; set; }

    public string Location { get; set; }

    public string[] Members { get; set; }

    public override string ToString()
    {
        return $"Members {string.Join(", ", Members)} escaped from {Location}";
    }
}

public class QuestParty
{
    public List<string> Members { get; set; } = new();
    public IList<string> Slayed { get; } = new List<string>();
    public string Key { get; set; }
    public string Name { get; set; }

    // In this particular case, this is also the stream id for the quest events
    public Guid Id { get; set; }

    // These methods take in events and update the QuestParty
    public void Apply(MembersJoined joined) => Members.Fill(joined.Members);
    public void Apply(MembersDeparted departed) => Members.RemoveAll(x => departed.Members.Contains(x));
    public void Apply(QuestStarted started) => Name = started.Name;

    public override string ToString()
    {
        return $"Quest party '{Name}' is {Members.Join(", ")}";
    }
}
