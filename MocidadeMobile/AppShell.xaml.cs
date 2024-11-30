using MocidadeMobile.Views;

namespace MocidadeMobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(Login), typeof(Login));
        }
    }
}
