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

    [Display(Name = "Image", ResourceType = typeof(Literals))]
    public string? Image { get; set; }

    public string ImageFull => string.IsNullOrEmpty(Image) ? "/Images/NoImagenItem.png" : Image;

    [Display(Name = "Country", ResourceType = typeof(Literals))]
    public int CountryId { get; set; }

    public string? CountryName { get; set; }
}