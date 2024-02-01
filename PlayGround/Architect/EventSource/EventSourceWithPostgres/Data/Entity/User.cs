namespace EventSourceWithPostgres.Data.Entity;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public string? Address { get; set; }
}
