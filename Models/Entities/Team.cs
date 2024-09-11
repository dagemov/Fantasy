namespace Models.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Image { get; set; }
        public Country Country { get; set; } = null!;
        public int CountryId { get; set; }
    }
}