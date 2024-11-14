namespace MocidadeMobile.Views;
using Microsoft.Maui.Controls;
using MocidadeMobile.Controllers;

public partial class Carteirinha : ContentPage
{
    //private readonly CarteirinhaController _carteirinha;

    public Carteirinha()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        //_carteirinha = new CarteirinhaController(Carteirinha);
        LoadData();
    }


    private async void LoadData()
    {
        // Simula o carregamento de dados com um atraso
        await Task.Delay(2000);

        //Carteirinha carteirinha = await CarteirinhaController();

        // Após o carregamento dos dados, exibe o conteúdo e oculta o indicador de carregamento
        LoadingIndicator.IsRunning = false;
        LoadingIndicator.IsVisible = false;
        ContentLayout.IsVisible = true;
    }
}