using MocidadeMobile.Models;
using MocidadeMobile.Services;

namespace MocidadeMobile.Views;

public partial class RegistroPresenca : ContentPage
{
    private readonly SessionService _sessionService;

    public RegistroPresenca()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _sessionService = new SessionService();
        LoadData();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // C�digo a ser executado quando a p�gina for carregada
        LoadData();
    }

    private async void LoadData()
    {
        // Simula o carregamento de dados com um atraso
        await Task.Delay(1000);
        EventoViewModel evento = _sessionService.GetEventoEmAndamento();

        if(evento != null)
        {
            txtEventoEmAndamento.Text = evento.Nome;
        }

        // Ap�s o carregamento dos dados, exibe o conte�do e oculta o indicador de carregamento
        LoadingIndicatorRegistroPresenca.IsRunning = false;
        LoadingIndicatorRegistroPresenca.IsVisible = false;
        ContentLayoutRegistroPresenca.IsVisible = true;
    }
}