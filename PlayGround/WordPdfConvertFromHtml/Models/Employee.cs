namespace WordPdfConvert.Models;

public class Employee
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public List<string> Projects { get; set; }
}