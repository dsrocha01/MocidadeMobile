using System.Windows.Input;
using Microsoft.Maui.Controls;
using MocidadeMobile.Views;
using MvvmHelpers;

namespace MocidadeMobile.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public ICommand LogoutCommand { get; }

        public MenuViewModel()
        {
            LogoutCommand = new Command(OnLogout);
        }

        private async void OnLogout()
        {
            // Limpe qualquer estado de autenticação aqui, se necessário

            // Navegue de volta para a página de login
            await Application.Current.MainPage.Navigation.PushAsync(new Login());
        }
    }
}