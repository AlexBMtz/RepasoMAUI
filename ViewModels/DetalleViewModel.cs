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
        private string id = string.Empty;

        [ObservableProperty]
        private Producto producto = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool hasError;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        public DetalleViewModel(ProductoApiService api)
        {
            _api = api;
        }

        [RelayCommand]
        private async Task RetryAsync()
        {
            if (string.IsNullOrWhiteSpace(Id))
            {
                return;
            }

            await CargarProductoAsync();
        }

        partial void OnIdChanged(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return;
            }

            _ = CargarProductoAsync();
        }

        private async Task CargarProductoAsync()
        {
            IsLoading = true;
            HasError = false;
            ErrorMessage = string.Empty;

            var (producto, error) = await _api.ObtenerProductoAsync(Id);

            if (error is not null)
            {
                HasError = true;
                ErrorMessage = error;
                Producto = new Producto();
            }
            else if (producto is not null)
            {
                Producto = producto;
            }

            IsLoading = false;
        }
    }
}
