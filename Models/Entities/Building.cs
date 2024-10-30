namespace Models.Entities
{
    public class Building
    {
        public int Id { get; set; }
        public string NameStreet { get; set; } = null!;
        public string NumberStret { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string FullAdress => "#" + NumberStret + " - " + NameStreet + "-" + ZipCode;
        public int UserId { get; set; }
        public User User { get; set; }
    }
}