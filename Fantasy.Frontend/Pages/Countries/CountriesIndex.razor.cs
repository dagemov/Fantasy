using Fantasy.Frontend.Repositories.Interfaces;
using Microsoft.AspNetCore.Components;
using Models.DTOS;

namespace Fantasy.Frontend.Pages.Countries;

public partial class CountriesIndex
{
    //Vars
    [Inject] private IRepository Repository { set; get; } = null!;

    private List<CountryDTO>? Countries { get; set; }

    //Load Data from backend List
    protected override async Task OnInitializedAsync()
    {
        var responseHttp = await Repository.GetAsync<List<CountryDTO>>("api/Countries");

        Countries = responseHttp.Response;
    }
}