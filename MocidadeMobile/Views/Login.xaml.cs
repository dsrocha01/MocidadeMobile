using Microsoft.Maui.Controls;
using MocidadeMobile.Controllers;
using MocidadeMobile.Models;
using MocidadeMobile.Services;
using MocidadeMobile.ViewModels;
using Org.BouncyCastle.Asn1.X509;

namespace MocidadeMobile.Views;

public partial class Login : ContentPage
{
    private readonly LoginController _loginController;
    private readonly SessionService _sessionService;
    public Login()
    {
        InitializeComponent();
        _loginController = new LoginController(this);
        _sessionService = new SessionService();
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string cpf = txtCpf.Text;
        string senha = txtSenha.Text;

        UsuarioViewModel loggedInUser = await _loginController.Logar(cpf, senha);

        if (loggedInUser != null && loggedInUser.Id > 0 && Application.Current != null)
        {
            // Salva a sessão do usuário
            _sessionService.SaveUserSession(loggedInUser);

            // Navegar para a página principal
            Application.Current.MainPage = new Menu(loggedInUser);
        }
        else
        {
            await DisplayAlert("Erro", "CPF ou senha incorretos.", "OK");
        }
    }

    
}