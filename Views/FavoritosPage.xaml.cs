using RepasoMAUI.ViewModels;

namespace RepasoMAUI.Views;

public partial class FavoritosPage : ContentPage
{
    public FavoritosPage(FavoritosViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}