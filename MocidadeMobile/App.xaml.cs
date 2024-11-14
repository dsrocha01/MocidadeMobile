using Microsoft.Maui.Controls;
using MocidadeMobile.Models;

namespace MocidadeMobile
{
    public partial class App : Application
    {
        private readonly Usuario _usuario;

        [Obsolete]
        public App(Usuario usuario)
        {
            InitializeComponent();
            _usuario = usuario;
            SetMainPage();
        }

        [Obsolete]
        private void SetMainPage()
        {
            if (DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Version.Major >= 12)
            {
                MainPage = new NavigationPage(new Views.Login());
            }
            else
            {
                // Handle other platforms or show a message
                MainPage = new ContentPage
                {
                    Content = new Label
                    {
                        Text = "Esse App só é suportado no Android 12 ou acima.",
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    }
                };
            }
        }
    }
}
