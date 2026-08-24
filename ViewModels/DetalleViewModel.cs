using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Data;
using RepasoMAUI.Models;
using RepasoMAUI.Views;

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

        public DetalleViewModel(ProductoRepository repo, FavoritosViewModel favoritosViewModel)
        {
            _repo = repo;
            _favoritosViewModel = favoritosViewModel;
        }

        // Se dispara automáticamente cuando Shell asigna el query property "id",
        // antes de que la página termine de aparecer (OnAppearing).
        partial void OnIdChanged(string value)
        {
            Producto = _repo.ObtenerPorId(value);
        }

        [RelayCommand]
        private async Task AgregarAFavoritos()
        {
            if (Producto is null) return;
            
            _favoritosViewModel.AgregarFavorito(Producto);

            await Shell.Current.DisplayAlert("Favoritos", $"{Producto.Nombre} ha sido agregado a favoritos.", "OK");
        }
        [RelayCommand]
        private async Task IrAFavoritos()
        {
            await Shell.Current.GoToAsync(nameof(FavoritosPage));
        }
    }
}
