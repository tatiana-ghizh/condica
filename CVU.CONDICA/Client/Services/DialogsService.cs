using CVU.CONDICA.Client.Shared.Components;
using MudBlazor;

namespace CVU.CONDICA.Client.Services
{
    public class DialogsService
    {
        private readonly IDialogService dialogService;

        public DialogsService(IDialogService dialogService)
        {
            this.dialogService = dialogService;
        }

        public async Task<bool> OpenConfirmationDialog(string contentText)
        {
            var parameters = new DialogParameters();

            parameters.Add("ContentText", contentText);

            var dialog = dialogService.Show<ConfirmationDialog>("Confirma", parameters);

            var result = await dialog.Result;

            return !result.Cancelled;
        }

        public void OpenCudDialog<TComponent>(string dialogTitle, int? Id, Func<Task> Callback)
        {
            var options = new DialogOptions { CloseOnEscapeKey = true, FullWidth = true };

            var parameters = new DialogParameters();

            parameters.Add("Id", Id);
            parameters.Add("Callback", Callback);

            dialogService.Show<CudDialog<TComponent>>(dialogTitle, parameters, options);
        }
    }
}
