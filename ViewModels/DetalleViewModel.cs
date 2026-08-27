using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Data;
using RepasoMAUI.Models;
using RepasoMAUI.Views;
using Microsoft.Maui.Controls;

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
        void AgregarAFavoritos()
        {
            _favoritosViewModel.Agregar(Producto);
        }

        [RelayCommand]
        static async Task VerFavoritos()
        {
            await Shell.Current.GoToAsync($"/{nameof(FavoritosPage)}");
        }

        [RelayCommand]
        async Task Close()
        {
            // Navigate back to the catalog (root list)
            await Shell.Current.GoToAsync("//lista");
        }
    }
}
