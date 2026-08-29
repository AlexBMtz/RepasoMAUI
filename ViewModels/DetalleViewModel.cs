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

        [ObservableProperty]
        private string id;

        [ObservableProperty]
        private Producto producto;

        public DetalleViewModel(ProductoRepository repo)
        {
            _repo = repo;
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
            if (ProductoSeleccionado == null)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha seleccionado ningún producto.", "OK");
                return;
            }
            if (!Favoritos.Contains(ProductoSeleccionado))
            {
                Favoritos.Add(ProductoSeleccionado);
                await Shell.Current.DisplayAlert("Exito", $"Producto '{ProductoSeleccionado.Nombre}' agregado a favoritos.", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Información", "El producto ya está en favoritos.", "OK");
                return;
            }
        }

        [RelayCommand]
        private async Task VerFavoritos()
        {
            if (Favoritos == null || Favoritos.Count == 0)
            {
                await Shell.Current.DisplayAlert("Información", "No hay productos en favoritos.", "OK");
                return;
            }

            var parametros = new Dictionary<string, object>
            {
                { "Favoritos", Favoritos }
            };

            await Shell.Current.GoToAsync(nameof(FavoritosPage), parametros);
        }


        [RelayCommand]
        private async Task EliminarProducto()
        {
            if (ProductoSeleccionado == null)
            {
                await Shell.Current.DisplayAlert("Error", "No se ha seleccionado ningún producto.", "OK");
                return;
            }
            if (Favoritos.Contains(ProductoSeleccionado))
            {
                Favoritos.Remove(ProductoSeleccionado);
                await Shell.Current.DisplayAlert("Exito", $"Producto '{ProductoSeleccionado.Nombre}' eliminado de favoritos.", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Información", "El producto no está en favoritos.", "OK");
                return;
            }
        }
    }
}
