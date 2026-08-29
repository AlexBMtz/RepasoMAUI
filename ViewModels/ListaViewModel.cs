
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Models;
using RepasoMAUI.Services;
using RepasoMAUI.Views;
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
        private string errorMessage;

        public ListaViewModel(ProductoApiService api)
        {
            _api = api;
            _ = CargarProductos();
        }


        [RelayCommand]
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

        [RelayCommand]
        async Task VerDetalle(Producto producto)
        {
            if (producto is null) return;
            var route = $"{nameof(DetallePage)}?id={producto.Id}";
            await Shell.Current.GoToAsync(route);
        }
    }
}