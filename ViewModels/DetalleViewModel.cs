using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Data;
using RepasoMAUI.Models;

namespace RepasoMAUI.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public partial class DetalleViewModel : ObservableObject
    {
        private readonly ProductoRepository _repo;
        private readonly FavoritosViewModel _favoritosViewModel;

        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private Producto producto;

        public DetalleViewModel(
            ProductoRepository repo,
            FavoritosViewModel favoritosViewModel)
        {
            _repo = repo;
            _favoritosViewModel = favoritosViewModel;
        }

        partial void OnIdChanged(string value)
        {
            Producto = _repo.ObtenerPorId(value);
        }

        [RelayCommand]
        private void AgregarFavorito()
        {
            if (Producto == null)
                return;

            _favoritosViewModel.AgregarFavorito(Producto);
        }

        [RelayCommand]
        private async Task VerFavoritos()
        {
            await Shell.Current.GoToAsync("favoritos");
        }
    }
}

