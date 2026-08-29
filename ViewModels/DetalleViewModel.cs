using CommunityToolkit.Mvvm.ComponentModel;
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

        partial void OnIdChanged(string value)
        {
            _ = CargarProducto(value);
        }

        async Task CargarProducto(string id)
        {
            if (string.IsNullOrEmpty(id))
                return;

            IsLoading = true;
            HasError = false;

            var (resultado, error) = await _api.ObtenerProductoAsync(id);

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