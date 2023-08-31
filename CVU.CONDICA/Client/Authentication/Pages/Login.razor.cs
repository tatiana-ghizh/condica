using Microsoft.AspNetCore.Components;
using MudBlazor;
using Blazored.LocalStorage;

namespace CVU.CONDICA.Client.Authentication.Pages
{
    public partial class Login
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }

        protected async override Task OnInitializedAsync()
        {
            loginTheme = codwerLoginTheme;
        }

        MudTheme loginTheme = null;
        MudTheme codwerLoginTheme = new MudTheme
        {
            Palette = new Palette
            {
                TextPrimary = "#98CB00",
                Error = "FF0099",
            }
        };
    }
}