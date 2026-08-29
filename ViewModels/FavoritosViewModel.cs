using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Models;
using RepasoMAUI.Services;
using System.Collections.ObjectModel;

namespace RepasoMAUI.ViewModels
{
    [QueryProperty(nameof(Favoritos), "Favoritos")]
    public partial class FavoritosViewModel : ObservableObject
    {
        private readonly FavoritosService _favoritosService;

        public ObservableCollection<Producto> Favoritos => _favoritosService.Favoritos;

        public FavoritosViewModel(FavoritosService favoritosService)
        {
            _favoritosService = favoritosService;
        }

        [RelayCommand]
        private async Task EliminarFavoritos(Producto producto)
        {
            if (producto == null) return;

            bool eliminado = _favoritosService.Eliminar(producto);

            if (eliminado)
            {
                await Shell.Current.DisplayAlert("Exito", $"'{producto.Nombre}' eliminado de favoritos", "Ok");
            }

        }
    }
}