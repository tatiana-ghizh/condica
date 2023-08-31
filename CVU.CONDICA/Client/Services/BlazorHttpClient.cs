using Blazored.LocalStorage;
using CVU.CONDICA.ExceptionHandling.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net;
using System.Text;

namespace CVU.CONDICA.Client.Services
{
    public class BlazorHttpClient
    {
        private readonly HttpClient httpClient;
        private readonly ISnackbar snackbar;
        private readonly ILocalStorageService localStorage;
        private readonly NavigationManager navigationManager;

        public BlazorHttpClient(HttpClient httpClient, ISnackbar snackbar, ILocalStorageService localStorage, NavigationManager navigationManager)
        {
            this.httpClient = httpClient;
            this.snackbar = snackbar;
            this.localStorage = localStorage;
            this.navigationManager = navigationManager;
        }

        public async Task<TResponse> Get<TResponse>(string url)
        {
            await AddTokenIfExists();

            using var response = await httpClient.GetAsync(url);

            if (!await IsSuccessfull(response, false)) return default;

            return await TryDeserialize<TResponse>(response);
        }

        public async Task<bool> Post<TRequest>(string url, TRequest model)
        {
            await AddTokenIfExists();

            using var response = await httpClient.PostAsJsonAsync(url, model);

            return await IsSuccessfull(response);
        }

        public async Task<TResponse> Post<TRequest, TResponse>(string url, TRequest model)
        {
            await AddTokenIfExists();

            using var response = await httpClient.PostAsJsonAsync(url, model);

            if (!await IsSuccessfull(response)) return default;

            return await TryDeserialize<TResponse>(response);
        }

        public async Task<TResponse> Put<TRequest, TResponse>(string url, TRequest model)
        {
            await AddTokenIfExists();

            using var response = await httpClient.PutAsJsonAsync(url, model);

            if (!await IsSuccessfull(response)) return default;

            return await TryDeserialize<TResponse>(response);
        }

        public async Task<bool> Patch<TRequest>(string url, TRequest model)
        {
            await AddTokenIfExists();

            var serializedDoc = JsonConvert.SerializeObject(model);
            var requestContent = new StringContent(serializedDoc, Encoding.UTF8, "application/json-patch+json");

            using var response = await httpClient.PatchAsync(url, requestContent);

            return await IsSuccessfull(response);
        }

        public async Task Put(string url)
        {
            await AddTokenIfExists();

            using var response = await httpClient.PutAsync(url, null);

            await IsSuccessfull(response);
        }

        public async Task<bool> Delete(string url)
        {
            await AddTokenIfExists();

            using var response = await httpClient.DeleteAsync(url);

            return await IsSuccessfull(response);
        }

        private async Task<bool> IsSuccessfull(HttpResponseMessage response, bool raiseSuccessMessage = true)
        {
            bool successfull = true;

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                await localStorage.RemoveItemAsync("token");

                snackbar.Add("Sesiune expirata", Severity.Error);

                navigationManager.NavigateTo("/", true);
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                successfull = false;

                var reason = await response.Content.ReadFromJsonAsync<ErrorResponse>();

                if (reason.FailureReason == FailureReason.ValidationErrors)
                {
                    var message = new StringBuilder();

                    reason.Messages.ToList().ForEach(v => message.AppendLine(v));

                    snackbar.Add(message.ToString(), Severity.Error);
                }
                else
                {
                    RaiseError(reason?.FailureReason);
                }
            }
            else if (raiseSuccessMessage)
            {
                snackbar.Add("Actiunea a fost procesata cu succes", Severity.Success);
            }

            return successfull;
        }

        private void RaiseError(FailureReason? reason)
        {
            string message = "Eroare internă. Vă rugam încercați din nou. Dacă problema persistă, contactați echipa de suport.";

            //switch (reason)
            //{

            //}

            snackbar.Add(message, Severity.Error);
        }

        private async Task AddTokenIfExists()
        {
            var token = await localStorage.GetItemAsync<string>("token");

            var authorizationAdded = this.httpClient.DefaultRequestHeaders.TryGetValues("Authorization", out IEnumerable<string> values);

            if (!authorizationAdded && !string.IsNullOrEmpty(token))
            {
                var bearer = $"bearer {token.Replace("\'", "")}";

                this.httpClient.DefaultRequestHeaders.Add("Authorization", bearer);
            }
        }

        private async Task<TResponse> TryDeserialize<TResponse>(HttpResponseMessage response)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<TResponse>();
            }
            catch
            {
                snackbar.Add("Eroare la deserializarea raspunsului sau server nevalabil");
            }

            return default;
        }
    }
}
