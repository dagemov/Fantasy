using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Shared.Resources;

namespace Fantasy.Frontend.Pages;

public partial class Home
{
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;
}