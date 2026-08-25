using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Data;
using RepasoMAUI.Models;
using RepasoMAUI.Services;
using RepasoMAUI.Views;

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
        async Task AgregarAFavoritos()
        {
            if (Producto is null) return;

            bool agregado = _favoritosService.Agregar(Producto);

            await Shell.Current.DisplayAlertAsync(
                "Favoritos",
                agregado
                    ? $"{Producto.Nombre} se agregó a favoritos."
                    : $"{Producto.Nombre} ya está en favoritos.",
                "OK");
        }

        [RelayCommand]
        static async Task VerFavoritos()
        {
            await Shell.Current.GoToAsync(nameof(FavoritosPage));
        }
    }
}
