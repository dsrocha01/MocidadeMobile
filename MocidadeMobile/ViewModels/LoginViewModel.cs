using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using MocidadeMobile.Views;

namespace MocidadeMobile.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string cpf = string.Empty;

        [ObservableProperty]
        private string senha = string.Empty;

        [RelayCommand]
        async Task Logar()
        {

            // Validação básica de CPF (apenas para exemplo, você pode querer uma validação mais robusta)
            if (!string.IsNullOrEmpty(Cpf) && Cpf.Length != 11)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "CPF inválido", "OK");
                return;
            }

            // Simulação de verificação de login
            if (Cpf == "11888608790" && Senha == "senha")
            {
                // Navegar para a MainPage
                await Application.Current.MainPage.Navigation.PushAsync(new Menu());

                // Remover a página de login da pilha de navegação
                var navigationStack = Application.Current.MainPage.Navigation.NavigationStack;
                var loginPage = navigationStack.FirstOrDefault();
                if (loginPage != null)
                {
                    Application.Current.MainPage.Navigation.RemovePage(loginPage);
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Usuário ou senha inválidos", "OK");
            }
        }


    }
}