using Shared.Resources;
using System.ComponentModel.DataAnnotations;

namespace Models.DTOS
{
    public class CountryDTO
    {
        public int Id { get; set; }

        [Display(Name = "Country", ResourceType = typeof(Literals))]
        [Required(ErrorMessageResourceName = "FieldRequired", ErrorMessageResourceType = typeof(Literals))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(Literals))]
        public string Name { get; set; } = null!;

        public int TeamsCount { get; set; }
        public List<TeamDTO>? Teams { get; set; }
    }
}