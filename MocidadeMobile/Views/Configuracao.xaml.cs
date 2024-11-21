using MocidadeMobile.Models;
using Microsoft.Maui.Controls;
namespace MocidadeMobile.Views;

public partial class Configuracao : ContentPage
{
	public Configuracao()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);
        BindingContext = new ConfiguracaoViewModel();
    }


    private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
    {
        var picker = sender as Picker;
        var selectedEvento = picker.SelectedItem as EventoViewModel;
        var viewModel = BindingContext as ConfiguracaoViewModel;
        if (viewModel != null && selectedEvento != null)
        {
            viewModel.EventoSelecionado = selectedEvento;
        }
    }

    private async void OnSalvarConfiguracaoClicked(object sender, EventArgs e)
    {
        var viewModel = BindingContext as ConfiguracaoViewModel;
        if (viewModel != null)
        {
            viewModel.OnSalvarConfiguracaoClicked();
        }
    }

}