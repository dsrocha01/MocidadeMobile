namespace MocidadeMobile.Views;
using Microsoft.Maui.Controls;
using MocidadeMobile.Controllers;
using MocidadeMobile.Models;
using MocidadeMobile.Services;

public partial class Carteirinha : ContentPage
{
    private readonly CarteirinhaController _carteirinha;
    private readonly SessionService _sessionService;
    private readonly QrCodeService _qrCodeService;

    public Carteirinha()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _carteirinha = new CarteirinhaController(this);
        _sessionService = new SessionService();
        _qrCodeService = new QrCodeService();
        LoadData();
    }


    private async void LoadData()
    {
        // Simula o carregamento de dados com um atraso
        await Task.Delay(2000);
        UsuarioViewModel usuario = _sessionService.GetUserSession();

        CarteirinhaViewModel carteirinha = await _carteirinha.ObtemCarteirinha(usuario.CPF);

        if(usuario != null)
        {
            // Atualiza as Labels com os dados obtidos
            txtNome.Text = carteirinha.Usuario.Nome;
            txtCPF.Text = carteirinha.Usuario.CPF;
            txtAla.Text = carteirinha.Ala.Nome;

            // Gera o QR code com o CPF do usuário
            QrCodeImage.Source = _qrCodeService.GenerateQrCode(usuario.CPF);
        }
        

        // Após o carregamento dos dados, exibe o conteúdo e oculta o indicador de carregamento
        LoadingIndicator.IsRunning = false;
        LoadingIndicator.IsVisible = false;
        ContentLayout.IsVisible = true;
    }
}