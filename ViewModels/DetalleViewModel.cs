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
        private readonly FavoritosViewModel _favoritosVM;

        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private Producto producto;

        public DetalleViewModel(ProductoRepository repo, FavoritosViewModel favoritosVM)
        {
            _repo = repo;
            _favoritosVM = favoritosVM;
        }

        // Se dispara automáticamente cuando Shell asigna el query property "id",
        // antes de que la página termine de aparecer (OnAppearing).
        partial void OnIdChanged(string value)
        {
            Producto = _repo.ObtenerPorId(value);
        }

        [RelayCommand]
        private void AgregarFavorito()
        {
            if (Producto != null)
                _favoritosVM.AgregarFavorito(Producto);
        }

    }
}
