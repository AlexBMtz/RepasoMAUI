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
        [NotifyPropertyChangedFor(nameof(MostrarContenido))]
        private bool isLoading;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(MostrarContenido))]
        private bool hasError;

        [ObservableProperty]
        private string errorMessage;

        // true solo cuando ya terminó de cargar y no hubo error:
        // así el contenido normal solo se muestra en el happy path.
        public bool MostrarContenido => !IsLoading && !HasError;

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
        private async Task Reintentar()
        {
            await CargarProducto();
        }

        private async Task CargarProducto()
        {
            IsLoading = true;
            HasError = false;
            ErrorMessage = string.Empty;

            var (resultado, error) = await _api.ObtenerProductoPorIdAsync(Id);

            if (error is not null)
            {
                HasError = true;
                ErrorMessage = error;
                Producto = null;
            }
            else
            {
                Producto = resultado;
            }

            IsLoading = false;
        }
    }
}