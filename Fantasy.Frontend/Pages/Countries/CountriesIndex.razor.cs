using CurrieTechnologies.Razor.SweetAlert2;
using Fantasy.Frontend.Repositories.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Models.DTOS;
using Models.Entities;
using Shared.Resources;
using System.Net;

namespace Fantasy.Frontend.Pages.Countries;

public partial class CountriesIndex
{
    //Vars
    [Inject] private NavigationManager? Navigation { get; set; }

    [Inject] private IStringLocalizer<Literals> Localizer { get; set; } = null!;

    [Inject] private IRepository Repository { set; get; } = null!;
    [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

    private List<CountryDTO>? Countries { get; set; }

    //Load Data from backend List
    protected override async Task OnInitializedAsync()
    {
        await LoadAsync();
    }

    private async Task LoadAsync()
    {
        var responseHppt = await Repository.GetAsync<List<CountryDTO>>("api/countries");
        if (responseHppt.Error)
        {
            var message = await responseHppt.GetErrorMessageAsync();
            await SweetAlertService.FireAsync(Localizer["Error"], message, SweetAlertIcon.Error);
            return;
        }
        Countries = responseHppt.Response!;
    }

    //Button Radzen Actions
    private void CreateNew()
    {
        Navigation!.NavigateTo("/countries/create");
    }

    private void EditRecord(int Id)
    {
        Navigation!.NavigateTo($"/countries/edit/{Id}");
    }

    private async Task DeleteAsync(CountryDTO country)
    {
        var result = await SweetAlertService.FireAsync(new SweetAlertOptions
        {
            Title = Localizer["Confirmation"],
            Text = string.Format(Localizer["DeleteConfirm"], Localizer["Country"], country.Name),
            Icon = SweetAlertIcon.Question,
            ShowCancelButton = true,
            CancelButtonText = Localizer["Cancel"]
        });

        var confirm = string.IsNullOrEmpty(result.Value);

        if (confirm)
        {
            return;
        }

        var responseHttp = await Repository.DeleteAsync($"api/countries/{country.Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                Navigation!.NavigateTo("/");
            }
            else
            {
                var menssageError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync(Localizer["Error"], Localizer[menssageError!], SweetAlertIcon.Error);
            }
            return;
        }

        await LoadAsync();

        var toast = SweetAlertService.Mixin(new SweetAlertOptions
        {
            Toast = true,
            Position = SweetAlertPosition.TopEnd,
            ShowConfirmButton = true,
            Timer = 3000,
            ConfirmButtonText = Localizer["Yes"]
        });

        await toast.FireAsync(icon: SweetAlertIcon.Success, message: Localizer["RecordDeletedOk"]);
    }
}