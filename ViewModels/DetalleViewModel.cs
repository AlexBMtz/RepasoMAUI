using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Models;
using RepasoMAUI.Services;

namespace RepasoMAUI.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public partial class DetalleViewModel : ObservableObject
    {
        private readonly ProductoApiService _apiService;

        [ObservableProperty]
        private string id = string.Empty;

        [ObservableProperty]
        private Producto? producto;

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool hasError;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        public DetalleViewModel(ProductoApiService apiService)
        {
            _apiService = apiService;
        }

        // Se dispara automáticamente cuando Shell asigna el query property "id",
        // antes de que la página termine de aparecer (OnAppearing).
        partial void OnIdChanged(string value)
        {
            if (int.TryParse(value, out int productoId))
            {
                _ = CargarProductoAsync(productoId);
            }
        }

        [RelayCommand]
        private async Task CargarProductoAsync(int productoId)
        {
            IsLoading = true;
            HasError = false;
            ErrorMessage = string.Empty;

            try
            {
                var dto = await _apiService.ObtenerProductoPorIdAsync(productoId);

                if (dto != null)
                {
                    Producto = new Producto
                    {
                        Id = dto.Id.ToString(),
                        Nombre = dto.Title,
                        Precio = dto.Price,
                        Descripcion = dto.Description,
                        Categoria = dto.Category,
                        ImagenUrl = dto.Image
                    };
                }
            }
            catch (Exception ex)
            {
                HasError = true;
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        private async Task ReintentarAsync()
        {
            if (int.TryParse(Id, out int productoId))
            {
                await CargarProductoAsync(productoId);
            }
        }
    }
}