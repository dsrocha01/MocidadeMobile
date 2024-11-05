using MocidadeMobile.Views;

namespace MocidadeMobile
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new Views.Menu());
        } 
    }
}
