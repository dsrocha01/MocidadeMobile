using MocidadeMobile.Models;
using MocidadeMobile.Services;
using Microsoft.Maui.Controls;
using ZXing.Net.Maui;
using ZXing.Net.Maui.Controls;
using MocidadeMobile.Controllers;
using ZXing.QrCode.Internal;


namespace MocidadeMobile.Views;

public partial class RegistroPresenca : ContentPage
{
    private readonly SessionService _sessionService;
    private readonly RegistroPresencaController _registroPresencaController;
    private EventoViewModel evento;
    private UsuarioViewModel usuario;

    public RegistroPresenca()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        _sessionService = new SessionService();
        _registroPresencaController = new RegistroPresencaController();
        evento = new EventoViewModel();
        usuario = new UsuarioViewModel();

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormat.QrCode
        };

        cameraBarcodeReaderView.BarcodesDetected +=OnBarcodeDetected;
        btnRegistrarPresenca.Clicked += async (sender, e) => await RegistraPresenca();
        btnRegistroManual.Clicked +=CarregaRegistroManual;
        btnRegistroCamera.Clicked +=CarregaRegistroCamera;
        txtCpf.TextChanged +=VerificaCpf;
        btnBuscaManual.Clicked += async (sender, e) => await RealizaBuscaManual();
        LoadData();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Código a ser executado quando a página for carregada

        LoadData();

        ClearScreen();

        // Reiniciar a câmera
        RestartCamera();
    }

    private async void LoadData()
    {
        // Simula o carregamento de dados com um atraso
        await Task.Delay(1000);
        evento = _sessionService.GetEventoEmAndamento();

        if(evento != null)
        {
            txtEventoEmAndamento.Text = evento.Nome;
        }

        // Após o carregamento dos dados, exibe o conteúdo e oculta o indicador de carregamento
        LoadingIndicatorRegistroPresenca.IsRunning = false;
        LoadingIndicatorRegistroPresenca.IsVisible = false;
        ContentLayoutRegistroPresenca.IsVisible = true;
    }

    private void OnBarcodeDetected(object sender, BarcodeDetectionEventArgs e)
    {
        // Processar o código QR detectado
        var barcode = e.Results.FirstOrDefault();
        if (barcode != null)
        {
            // Exibir o valor do código QR
            Dispatcher.Dispatch(async () =>
            {

                // Mostrar o indicador de carregamento
                frameCamera.IsVisible = false;
                LoadingIndicatorCamera.IsVisible = true;
                LoadingIndicatorCamera.IsRunning = true;

                //cameraBarcodeReaderView.IsVisible = false;

                usuario = await _registroPresencaController.RetornaUsuario(barcode.Value);

                if(usuario.Nome !=  null)
                    txtNomeEncontrado.Text = "Usuário: "+usuario.Nome + " \n CPF: " + usuario.CPF;
                    txtNomeEncontrado.LineBreakMode = LineBreakMode.WordWrap;


                // Oculta o indicador de carregamento
                LoadingIndicatorCamera.IsVisible = false;
                LoadingIndicatorCamera.IsRunning = false;

                //lblAponteACamera.IsVisible = false;
                btnRegistrarPresenca.IsVisible = true;

                

            });
        }
    }

    private async Task RegistraPresenca()
    {
        try
        {
            // Mostrar o indicador de carregamento
            frameCamera.IsVisible = false;
            LoadingIndicatorCamera.IsVisible = true;
            LoadingIndicatorCamera.IsRunning = true;

            var eventoId = evento.CodigoEvento;

            if (usuario !=null && usuario.Id >0)
            {
                bool registrou = await _registroPresencaController.RegistraPresenca(eventoId, usuario);

                if (registrou)
                {
                    await MessageService.SendAlertAsync("Pronto", "Presença registrada com sucesso.");

                    await ReloadPage();
                }
                
            }
            else
            {
                await MessageService.SendAlertAsync("Atenção", "Usuario não localizado.");
            }
        }
        catch(Exception e)
        {
            //await MessageService.SendAlertAsync("Erro", "Falha ao registrar presença: " + e.Message);
        }
        finally
        {
            //// Ocultar o indicador de carregamento
            //frameCamera.IsVisible = true;
            //LoadingIndicatorCamera.IsVisible = false;
            //LoadingIndicatorCamera.IsRunning = false;
            //btnRegistrarPresenca.IsVisible = false;
        }
    }

    private async Task RealizaBuscaManual()
    {
        if (txtCpf != null && txtCpf.IsVisible)
        {
            if (txtCpf.Text != "" && txtCpf.Text.Length == 11)
            {
                usuario = await _registroPresencaController.RetornaUsuario(txtCpf.Text);

                if (usuario.Nome != null)
                {
                    txtNomeEncontrado.Text = "Usuário: " + usuario.Nome + " \n CPF: " + usuario.CPF;
                    txtNomeEncontrado.LineBreakMode = LineBreakMode.WordWrap;

                    btnRegistrarPresenca.IsVisible = true;
                }
                else
                {
                    btnRegistrarPresenca.IsVisible = false;

                    await MessageService.SendAlertAsync("Atenção", "Usuario não localizado.");
                }  
            }
        }
    }

    private void CarregaRegistroManual(object sender, EventArgs e)
    {

        //cameraBarcodeReaderView.IsDetecting = false;
        //cameraBarcodeReaderView.IsEnabled = false;
        //frameCamera.IsVisible = false;
        btnRegistroManual.IsVisible = false;
        txtCpf.IsVisible = true;
        btnRegistroCamera.IsVisible = true;
    }

    private void CarregaRegistroCamera(object sender, EventArgs e)
    {
        //cameraBarcodeReaderView.IsDetecting = true;
        //cameraBarcodeReaderView.IsEnabled = true;
        //frameCamera.IsVisible = true;
        btnRegistroManual.IsVisible = true;
        txtCpf.Text = "";
        txtCpf.IsVisible = false;
        btnRegistrarPresenca.IsVisible = false;
        btnRegistroCamera.IsVisible = false;
    }

    private void VerificaCpf(object sender, EventArgs e)
    {
        if (txtCpf != null && txtCpf.IsVisible)
        {
            if (txtCpf.Text.Length == 11)
            {
                btnBuscaManual.IsVisible = true;
            }
            else
            {
                btnBuscaManual.IsVisible = false;
            }
        }
    }

    private async Task ReloadPage()
    {
        // Remove a página atual da pilha de navegação
        await Navigation.PopToRootAsync();
        await Navigation.PopToRootAsync();
        await Navigation.PopToRootAsync();

        Thread.Sleep(1000);

        // Navega para a mesma página novamente
        await Navigation.PushAsync(new RegistroPresenca());
    }

    private void ClearScreen()
    {
        Thread.Sleep(500);
        txtEventoEmAndamento.Text = string.Empty;
        txtNomeEncontrado.Text = string.Empty;
        txtCpf.Text = string.Empty;
        btnRegistrarPresenca.IsVisible = false;
        btnBuscaManual.IsVisible = false;
        cameraBarcodeReaderView.IsDetecting = true;
        cameraBarcodeReaderView.IsEnabled = true;
        frameCamera.IsVisible = true;

        Thread.Sleep(500);

        LoadingIndicatorCamera.IsVisible = false;
        LoadingIndicatorCamera.IsRunning = false;
        btnRegistroManual.IsVisible = true;
        txtCpf.IsVisible = false;
        btnRegistroCamera.IsVisible = false;
        ContentLayoutRegistroPresenca.IsVisible = false;

        Thread.Sleep(500);

        LoadingIndicatorRegistroPresenca.IsRunning = false;
        LoadingIndicatorRegistroPresenca.IsVisible = false;
        RestartCamera();
    }

    private void RestartCamera()
    {
        cameraBarcodeReaderView.IsDetecting = false;
        cameraBarcodeReaderView.IsEnabled = false;
        frameCamera.IsVisible = false;

        // Aguarde um curto período para garantir que a câmera seja desativada corretamente
        Task.Delay(1000).ContinueWith(_ =>
        {
            Dispatcher.Dispatch(() =>
            {
                cameraBarcodeReaderView.IsDetecting = true;
                cameraBarcodeReaderView.IsEnabled = true;
                frameCamera.IsVisible = true;
            });
        });
    }
}