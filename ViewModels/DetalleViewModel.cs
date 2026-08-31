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
        private string id = string.Empty;

        [ObservableProperty]
        private Producto producto = new();

        // Mensaje opcional para dar feedback visual (p. ej. "Ya está en favoritos")
        [ObservableProperty]
        private string mensajeFavoritos;

        // DetalleViewModel es Transient, pero FavoritosViewModel es Singleton:
        // el contenedor de DI inyecta siempre la MISMA instancia de FavoritosViewModel,
        // así que cualquier producto agregado aquí es visible en FavoritosPage.
        public DetalleViewModel(ProductoRepository repo, FavoritosViewModel favoritosVm)
        {
            _repo = repo;
            _favoritosVm = favoritosVm;
        }

        partial void OnIdChanged(string value)
        {
            Producto = _repo.ObtenerPorId(value);
            MensajeFavoritos = string.Empty;
        }

        [RelayCommand]
        private void AgregarAFavoritos()
        {
            if (Producto is null)
                return;

            bool agregado = _favoritosVm.AgregarProducto(Producto);
            MensajeFavoritos = agregado
                ? "Agregado a favoritos ✔"
                : "Este producto ya está en favoritos";
        }

        [RelayCommand]
        private async Task VerFavoritos()
        {
            await Shell.Current.GoToAsync(nameof(FavoritosPage));
        }
    }
}