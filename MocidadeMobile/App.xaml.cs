using Microsoft.Maui.Controls;
using MocidadeMobile.Models;
using MocidadeMobile.Services;
using MocidadeMobile.Views;

namespace MocidadeMobile
{
    public partial class App : Application
    {
        private readonly UsuarioViewModel _usuario;

        private readonly SessionService _sessionService;

        public App(UsuarioViewModel usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            _sessionService = new SessionService();
            SetMainPage();
        }


        private void SetMainPage()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Version.Major >= 12)
            {
                //Verifica se já esta logado via session e caso sim, redireciona para home
                UsuarioViewModel sessao = _sessionService.GetUserSession();
                // Navegar para a página principal
                if (sessao != null && sessao.Id > 0 && Application.Current != null)
                {
                    MainPage = new NavigationPage(new Views.Menu(sessao));
                }
                else
                {
                    MainPage = new NavigationPage(new Views.Login());
                }
            }
            else
            {
                // Handle other platforms or show a message
                MainPage = new ContentPage
                {
                    Content = new Grid
                    {
                        VerticalOptions = LayoutOptions.Center,
                        HorizontalOptions = LayoutOptions.Center,
                        Children =
                            {
                                new Label
                                {
                                    Text = "Esse App só é suportado no Android 12 ou acima.",
                                    VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Center
                                }
                            }
                    }
                };
            }
        }
    }
}
