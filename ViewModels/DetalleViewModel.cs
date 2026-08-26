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

        public DetalleViewModel(ProductoApiService api)
        {
            _api = api;
        }

        // Se dispara automáticamente cuando Shell asigna el query property "id",
        // antes de que la página termine de aparecer (OnAppearing).
        partial void OnIdChanged(string value)
        {
            if(!string.IsNullOrWhiteSpace(value))
            {
                _ = CargarDetalleProducto(value);
            }
        }

        async Task CargarDetalleProducto(string id)
        {
            isLoading = true;

            var (resultado, error) = await _api.ObtenerProductoPorIdAsync(id);
            if (error is not null)
            {
                // Manejar el error, por ejemplo, mostrar un mensaje al usuario.
                Producto = null;
            }
            else
            {
                Producto = resultado;
            }
            isLoading = false;
        }

    }
}
