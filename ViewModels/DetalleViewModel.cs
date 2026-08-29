using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Models;
using RepasoMAUI.Services;

namespace RepasoMAUI.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public partial class DetalleViewModel : ObservableObject
    {
        private readonly ProductoApiService _api;

        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private Producto producto;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool hasError;

        [ObservableProperty]
        private string errorMessage;

        public DetalleViewModel(ProductoApiService api)
        {
            _api = api;
        }

        // Se dispara automáticamente cuando Shell asigna el query property "id",
        // antes de que la página termine de aparecer (OnAppearing).
        partial void OnIdChanged(string value)
        {
            _ = CargarProducto();
        }

        [RelayCommand]
        async Task CargarProducto()
        {
            IsLoading = true;
            HasError = false;

            var (resultado, error) = await _api.ObtenerProductoPorIdAsync(Id);

            if (error is not null)
            {
                HasError = true;
                ErrorMessage = error;
            }
            else
            {
                Producto = resultado;
            }

            IsLoading = false;
        }
    }
}