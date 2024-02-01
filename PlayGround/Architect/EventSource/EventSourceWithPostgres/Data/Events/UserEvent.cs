using EventSourceWithPostgres.Data.Entity;

namespace EventSourceWithPostgres.Data.Events;

public record RegisterUserEvent(string Username, string Address);

public record UpdateUserEvent(User User);
