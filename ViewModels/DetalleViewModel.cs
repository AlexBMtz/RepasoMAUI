using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Data;
using RepasoMAUI.Models;
using RepasoMAUI.Views;
using System.Collections.ObjectModel;
using RepasoMAUI.Services;

namespace RepasoMAUI.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public partial class DetalleViewModel : ObservableObject
    {
        private readonly ProductoRepository _repo;
        private readonly FavoritosService _favoritosService;

        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private Producto producto;

        public ObservableCollection<Producto> Favoritos => _favoritosService.Favoritos;

        public DetalleViewModel(ProductoRepository repo, FavoritosService favoritosService)
        {
            _repo = repo;
            _favoritosService = favoritosService;
        }

        // Se dispara automáticamente cuando Shell asigna el query property "id",
        // antes de que la página termine de aparecer (OnAppearing).
        partial void OnIdChanged(string value)
        {
            Producto = _repo.ObtenerPorId(value);
        }

        [RelayCommand]
        private async Task AgregarProducto()
        {
            bool agregado = _favoritosService.Agregar(Producto);

            if (agregado)
            {
                await Shell.Current.DisplayAlert("Exito", "Agregado a favoritos.", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Información", "El producto ya está en favoritos.", "OK");
            }
        }

        [RelayCommand]
        private async Task VerFavoritos()
        {
            await Shell.Current.GoToAsync(nameof(FavoritosPage));
        }


        [RelayCommand]
        private async Task EliminarProducto()
        {
            if (Producto == null)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha seleccionado ningún producto.", "OK");
                return;
            }
            if (Favoritos.Contains(Producto))
            {
                _favoritosService.Eliminar(Producto);
                await Shell.Current.DisplayAlert("Exito", $"Producto '{Producto.Nombre}' eliminado de favoritos.", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Información", "El producto no está en favoritos.", "OK");
                return;
            }
        }
    }
}
