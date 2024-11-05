using Microsoft.Maui.Controls;
using MocidadeMobile.ViewModels;

namespace MocidadeMobile.Views;

public partial class Login : ContentPage
{
    public Login()
    {
        InitializeComponent();
        BindingContext = new LoginViewModel();
    }
}