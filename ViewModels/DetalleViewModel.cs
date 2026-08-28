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
        private readonly FavoritosViewModel _favoritosVm;

        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private Producto producto;

        public DetalleViewModel(ProductoRepository repo, FavoritosViewModel favoritosVm)
        {
            _repo = repo;
            _favoritosVm = favoritosVm;
        }

        // Se dispara automáticamente cuando Shell asigna el query property "id",
        // antes de que la página termine de aparecer (OnAppearing).
        partial void OnIdChanged(string value)
        {
            Producto = _repo.ObtenerPorId(value);
        }

        [RelayCommand]
        async Task AgregarAFavoritos()
        {
            if (Producto == null) return;

            bool agregado = _favoritosVm.Agregar(Producto);

            if (agregado)
            {
                await Shell.Current.DisplayAlertAsync("Favoritos", "Producto agregado a favoritos.", "Aceptar");
            }
            else
            {
                await Shell.Current.DisplayAlertAsync("Favoritos", "El producto ya está en favoritos.", "Aceptar");
            }
        }

        [RelayCommand]
        static async Task IrAFavoritos()
        {
            await Shell.Current.GoToAsync(nameof(FavoritosPage));
        }
    }
}
