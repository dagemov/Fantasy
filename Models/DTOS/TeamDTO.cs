using Models.Entities;
using Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOS;

public class TeamDTO
{
    public int Id { get; set; }

    [Display(Name = "Team", ResourceType = typeof(Literals))]
    [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Literals))]
    [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
    public string Name { get; set; } = null!;

    public string? Image { get; set; }
    public int CountryId { get; set; }
}