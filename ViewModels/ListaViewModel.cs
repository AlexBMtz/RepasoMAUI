
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Models;
using RepasoMAUI.Services;
using System.Collections.ObjectModel;

namespace RepasoMAUI.ViewModels
{
    public partial class ListaViewModel : ObservableObject
    {
        private readonly ProductoApiService _api;

        [ObservableProperty]
        private ObservableCollection<Producto> productos = [];

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private bool hasError;

        [ObservableProperty]
        private string? errorMessage;

        public ListaViewModel(ProductoApiService api)
        {
            _api = api;
            _ = CargarProductos();
        }

        async Task CargarProductos()
        {
            IsLoading = true;
            HasError = false;

            var (resultado, error) = await _api.ObtenerProductosAsync();

            if (error is not null)
            {
                HasError = true;
                ErrorMessage = error;
            }
            else
            {
                Productos = new ObservableCollection<Producto>(resultado);
            }

            IsLoading = false;
        }

        /// <summary>
        /// Navega a la pantalla de detalle enviando el identificador del producto seleccionado.
        /// </summary>
        /// <param name="producto">Producto seleccionado de la lista.</param>
        [RelayCommand]
        private async Task VerDetalle(Producto? producto)
        {
            if (producto is null)
                return;

            // Navega a DetallePage pasando el id del producto como parámetro de consulta
            await Shell.Current.GoToAsync($"{nameof(Views.DetallePage)}?id={producto.Id}");
        }
    }
}