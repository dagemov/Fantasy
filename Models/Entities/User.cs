using Microsoft.AspNetCore.Identity;
using Models.Enums;

namespace Models.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? Image { get; set; }
    public ICollection<Building>? Buildings { get; set; }
    public int BuildingsCountr => Buildings == null ? 0 : Buildings.Count;
    public bool Status { get; set; }
    public UserType UserType { get; set; }
}