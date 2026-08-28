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
        private string? id;

        [ObservableProperty]
        private Producto? producto;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool hasError;

        [ObservableProperty]
        private string? errorMessage;

        public DetalleViewModel(ProductoApiService api)
        {
            _api = api;
        }

        // Se dispara automáticamente cuando Shell asigna el query property "id",
        // antes de que la página termine de aparecer (OnAppearing).
        partial void OnIdChanged(string? value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                // Carga la información del producto al recibir el Id de navegación
                _ = CargarProductoDetalleAsync(value);
            }
        }

        /// <summary>
        /// Obtiene los detalles del producto desde la API y actualiza las propiedades del ViewModel.
        /// </summary>
        /// <param name="productoId">Identificador del producto a cargar.</param>
        private async Task CargarProductoDetalleAsync(string productoId)
        {
            IsLoading = true;
            HasError = false;

            // Llama al servicio para obtener la información del producto por su ID
            var (resultado, error) = await _api.ObtenerProductoPorIdAsync(productoId);

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
