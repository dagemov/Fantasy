namespace Models.Entities;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Image { get; set; }
    public ICollection<Team>? Teams { get; set; }
    public int TeamsCount => Teams == null ? 0 : Teams.Count();
}