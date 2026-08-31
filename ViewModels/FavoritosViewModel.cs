using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Models;

namespace RepasoMAUI.ViewModels
{
    // Se registra como Singleton en el contenedor de DI.
    // Al ser una única instancia compartida durante toda la vida de la app,
    // la ObservableCollection interna conserva su estado aunque naveguemos
    // entre páginas (Detalle <-> Favoritos) múltiples veces.
    public partial class FavoritosViewModel : ObservableObject
    {
        public ObservableCollection<Producto> Favoritos { get; } = new();

        [ObservableProperty]
        private bool hayFavoritos;

        [ObservableProperty]
        private bool noHayFavoritos;

        public FavoritosViewModel()
        {
            ActualizarEstado();
        }

        // Llamado desde DetalleViewModel al presionar "Agregar a Favoritos".
        // Devuelve true si se agregó, false si ya existía (evita duplicados).
        public bool AgregarProducto(Producto producto)
        {
            if (producto is null)
                return false;

            bool yaExiste = Favoritos.Any(p => p.Id == producto.Id);
            if (yaExiste)
                return false;

            Favoritos.Add(producto);
            ActualizarEstado();
            return true;
        }

        [RelayCommand]
        private void Eliminar(Producto producto)
        {
            if (producto is null)
                return;

            var existente = Favoritos.FirstOrDefault(p => p.Id == producto.Id);
            if (existente is not null)
            {
                Favoritos.Remove(existente);
                ActualizarEstado();
            }
        }

        private void ActualizarEstado()
        {
            HayFavoritos = Favoritos.Count > 0;
            NoHayFavoritos = !HayFavoritos;
        }
    }
}