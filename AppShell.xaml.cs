using RepasoMAUI.Views;

namespace RepasoMAUI;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("DetallePage", typeof(DetallePage));
        Routing.RegisterRoute("favoritos", typeof(FavoritosPage));
    }
}