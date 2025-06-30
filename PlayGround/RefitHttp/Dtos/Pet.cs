namespace RefitHttp.Dtos;

public record Pet(
    long Id,
    Category Category,
    string Name,
    IReadOnlyList<string> PhotoUrls,
    IReadOnlyList<Tag> Tags,
    string Status
);