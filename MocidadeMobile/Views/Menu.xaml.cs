using MocidadeMobile.Controllers;
using MocidadeMobile.Models;
using MocidadeMobile.ViewModels;
namespace MocidadeMobile.Views;

public partial class Menu : Shell
{
    private readonly LoginController _loginController;

    public Menu(Usuario loggedInUser)
	{
		InitializeComponent();
        _loginController = new LoginController(this);
        BindingContext = new MenuViewModel(loggedInUser);
    }

    private void OnLogoutButtonClicked(object sender, EventArgs e)
    {
        _loginController.OnLogout();
    }
}