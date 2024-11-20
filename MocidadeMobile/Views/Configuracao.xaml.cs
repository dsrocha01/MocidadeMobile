using MocidadeMobile.Models;

namespace MocidadeMobile.Views;

public partial class Configuracao : ContentPage
{
	public Configuracao()
	{
		InitializeComponent();
		BindingContext = new ConfiguracaoViewModel();
    }
}