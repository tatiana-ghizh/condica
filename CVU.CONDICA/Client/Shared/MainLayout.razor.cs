using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace CVU.CONDICA.Client.Shared
{
    public partial class MainLayout
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        protected async override Task OnInitializedAsync()
        {
            currentTheme = codwerTheme;
        }

        MudTheme currentTheme = null;
        MudTheme codwerTheme = new MudTheme
        {
            Palette = new Palette
            {
                AppbarBackground = "#00467F",
                Primary = "#00467F",
                Error = "#FF0099",
                Success = "#98CB00",
                Warning = "#FFEB3B"
            }
        };
    }
}