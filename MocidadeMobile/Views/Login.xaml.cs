using Microsoft.Maui.Controls;
using MocidadeMobile.Controllers;
using MocidadeMobile.Models;
using MocidadeMobile.ViewModels;
using Org.BouncyCastle.Asn1.X509;

namespace MocidadeMobile.Views;

public partial class Login : ContentPage
{
    private readonly LoginController _loginController;
    public Login()
    {
        InitializeComponent();
        _loginController = new LoginController(this);
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        string cpf = txtCpf.Text;
        string senha = txtSenha.Text;

        Usuario loggedInUser = await _loginController.Logar(cpf, senha);

        if (loggedInUser != null && Application.Current != null)
        {
            // Navegar para a página principal
            Application.Current.MainPage = new Menu(loggedInUser);
        }
        else
        {
            await DisplayAlert("Erro", "CPF ou senha incorretos.", "OK");
        }
    }

    
}