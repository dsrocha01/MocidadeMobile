using MocidadeMobile.Controllers;
using MocidadeMobile.Models;
namespace MocidadeMobile.Views;

public partial class Menu : Shell
{
    private readonly LoginController _loginController;

    public Menu(UsuarioViewModel loggedInUser)
	{
		InitializeComponent();
        _loginController = new LoginController(this);
        BindingContext = new MenuViewModel(loggedInUser);
    }

    /// <summary>
    /// Hack para evitar da pagina ficar com inputs quebrados a cada registro de presença
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnShellNavigated(object sender, ShellNavigatedEventArgs e)
    {

        // Verifique se a página "RegistroPresenca" foi navegada
        if (e.Current.Location.OriginalString.Contains("RegistroPresenca"))
        {
            // Remova a página da pilha de navegação
            var page = Shell.Current.Navigation.NavigationStack.FirstOrDefault(p => p is RegistroPresenca);
            if (page != null)
            {
                Shell.Current.Navigation.RemovePage(page);
            }

            // Navegue para a página novamente
            Shell.Current.GoToAsync("//RegistroPresenca");
        }
    }

    private void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        _loginController.OnLogout();
    }
}