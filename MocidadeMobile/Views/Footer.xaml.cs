namespace MocidadeMobile.Views;

public partial class Footer : ContentView
{
    public Footer()
    {
        InitializeComponent();
    }

    private void OnLabelTapped(object sender, EventArgs e)
    {
        var uri = new Uri("https://dgsystems.com.br/");
        Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
    }
}