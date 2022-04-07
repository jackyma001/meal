namespace frontend;

public class Meal
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Summary { get; set; }
    public string PhotoPath { get; set; } = "anonymous.png";
    public string Type { get; set; } = string.Empty;
    public DateTime LastDateTime { get; set; }
}
