using System;
using System.Threading.Tasks;

namespace MocidadeMobile.Services
{
    public static class MessageService
    {
        public static async Task SendAlertAsync(string titulo, string mensagem)
        {
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert(titulo, mensagem, "OK");
            }
        }
    }
}
