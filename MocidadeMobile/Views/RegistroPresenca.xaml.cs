using MocidadeMobile.Models;
using MocidadeMobile.Services;
using Microsoft.Maui.Controls;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using MocidadeMobile.Controllers;


namespace MocidadeMobile.Views;

public partial class RegistroPresenca : ContentPage
{
    private readonly SessionService _sessionService;
    private readonly RegistroPresencaController _registroPresencaController;

    public RegistroPresenca()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _sessionService = new SessionService();
        _registroPresencaController = new RegistroPresencaController();

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormat.QrCode
        };

        cameraBarcodeReaderView.BarcodesDetected += OnBarcodeDetected;
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

    private void OnBarcodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        // Processar o c�digo QR detectado
        var barcode = e.Results.FirstOrDefault();
        if (barcode != null)
        {
            // Exibir o valor do c�digo QR
            Dispatcher.Dispatch(() =>
            {

                txtEventoEmAndamento.Text = $"QR Code detectado: {barcode.Value}";

                // Parar a captura da c�mera
                cameraBarcodeReaderView.IsDetecting = false;
                cameraBarcodeReaderView.IsEnabled = false;
                cameraBarcodeReaderView.IsVisible = false;


                lblAponteACamera.IsVisible = false;

            });
        }
    }

    private async Task RegistraPresencaAsync()
    {
        try
        {
            var evento = 1;
            var usuario = 1;

            await _registroPresencaController.RegistraPresenca(evento, usuario);

        }
        catch(Exception e)
        {
            await MessageService.SendAlertAsync("Erro", "Falha ao registrar presen�a: " + e.Message);
        }
    }
}