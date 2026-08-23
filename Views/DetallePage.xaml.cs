using RepasoMAUI.ViewModels;

namespace RepasoMAUI.Views;

public partial class DetallePage : ContentPage
{
    public DetallePage(DetalleViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}