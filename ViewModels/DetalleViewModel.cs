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

        [ObservableProperty]
        private bool hasProducto;

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
            HasProducto = false;
            ErrorMessage = string.Empty;

            try
            {
                var (resultado, error) = await _api.ObtenerProductoAsync(id);

                if (error is not null)
                {
                    HasError = true;
                    ErrorMessage = error;
                }
                else
                {
                    Producto = resultado;
                    HasProducto = true;
                }
            }
            catch (TaskCanceledException)
            {
                HasError = true;
                ErrorMessage = "La petición tardó demasiado. Verifica tu conexión e intenta de nuevo.";
            }
            catch (HttpRequestException)
            {
                HasError = true;
                ErrorMessage = "No se pudo conectar al servidor o la ruta no existe.";
            }
            catch (System.Text.Json.JsonException)
            {
                HasError = true;
                ErrorMessage = "La respuesta del servidor no se pudo interpretar como JSON.";
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = $"Ocurrió un error inesperado: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}