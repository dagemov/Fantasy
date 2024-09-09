using Fantasy.Frontend.Repositories.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Models.DTOS;
using Shared.Resources;

namespace Fantasy.Frontend.Pages.Countries;

public partial class CountriesIndex
{
    //Vars
    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Inject] private IRepository Repository { set; get; } = null!;

    private List<CountryDTO>? Countries { get; set; }

    //Load Data from backend List
    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<List<CountryDTO>>("api/Countries");

        Countries = responseHttp.Response;
    }
}