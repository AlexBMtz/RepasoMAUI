using RepasoMAUI.Models;
using RepasoMAUI.ViewModels;

namespace RepasoMAUI.Views;

public partial class FavoritosPage : ContentPage
{
    private readonly FavoritosViewModel _viewModel;

    public FavoritosPage(FavoritosViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _viewModel.CargarFavoritos();
    }

    private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (sender is CheckBox checkBox &&
            checkBox.BindingContext is Producto producto)
        {
            _viewModel.SeleccionarProductoCommand.Execute(producto);
        }
    }
}
