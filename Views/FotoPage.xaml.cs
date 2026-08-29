using RepasoMAUI.ViewModels;

namespace RepasoMAUI.Views;

public partial class FotoPage : ContentPage
{
    public FotoPage(FotoViewModel fotoViewModel)
    {
        InitializeComponent();
        BindingContext = fotoViewModel;
    }
}