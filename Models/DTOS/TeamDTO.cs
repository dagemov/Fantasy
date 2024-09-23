using Models.Entities;

namespace Models.DTOS;

public class TeamDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Image { get; set; }
    public int CountryId { get; set; }
}