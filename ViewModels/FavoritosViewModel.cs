using CommunityToolkit.Mvvm.ComponentModel;
using RepasoMAUI.Models;
using RepasoMAUI.Services;
using System.Collections.ObjectModel;

namespace RepasoMAUI.ViewModels
{
    public partial class FavoritosViewModel : ObservableObject
    {
        private readonly FavoritosService _favoritosService;

        public ObservableCollection<Producto> Favoritos => _favoritosService.Favoritos;

        public FavoritosViewModel(FavoritosService favoritosService)
        {
            _favoritosService = favoritosService;
        }
    }
}
