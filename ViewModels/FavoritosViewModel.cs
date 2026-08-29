using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Data;
using RepasoMAUI.Models;
using System.Collections.ObjectModel;

namespace RepasoMAUI.ViewModels
{
    public partial class FavoritosViewModel : ObservableObject
    {
        private readonly ProductoRepository _repo;

        // Lista de productos favoritos
        [ObservableProperty]
        private ObservableCollection<Producto> favoritos;

        // Productos que el usuario seleccionó para eliminar
        private List<Producto> seleccionados = new();

        // Indica si estamos en modo eliminar
        [ObservableProperty]
        private bool modoEliminar;

        public FavoritosViewModel(ProductoRepository repo)
        {
            _repo = repo;

            Favoritos = new ObservableCollection<Producto>();
        }

        // Carga los favoritos desde el Repository
        public void CargarFavoritos()
        {
            Favoritos.Clear();

            foreach (var producto in _repo.ObtenerFavoritos())
            {
                Favoritos.Add(producto);
            }

            seleccionados.Clear();
            ModoEliminar = false;
        }

        // Activa o desactiva el modo eliminar
        [RelayCommand]
        private void ActivarEliminar()
        {
            if (ModoEliminar)
            {
                // Si ya estamos en modo eliminar,
                // eliminamos los seleccionados
                EliminarSeleccionados();
            }
            else
            {
                // Entramos en modo eliminar
                ModoEliminar = true;
            }
        }

        // Selecciona o deselecciona un producto
        [RelayCommand]
        private void SeleccionarProducto(Producto producto)
        {
            if (producto == null)
                return;

            // Si ya está seleccionado, lo quitamos
            if (seleccionados.Any(p => p.Id == producto.Id))
            {
                seleccionados.RemoveAll(p => p.Id == producto.Id);
            }
            else
            {
                // Si no está seleccionado, lo agregamos
                seleccionados.Add(producto);
            }
        }

        // Elimina todos los productos seleccionados
        private void EliminarSeleccionados()
        {
            if (seleccionados.Count == 0)
            {
                ModoEliminar = false;
                return;
            }

            // Eliminamos todos del Repository
            _repo.EliminarFavoritos(seleccionados);

            // Eliminamos todos de la colección que se muestra
            foreach (var producto in seleccionados.ToList())
            {
                Favoritos.Remove(producto);
            }

            // Limpiamos la lista de seleccionados
            seleccionados.Clear();

            // Salimos del modo eliminar
            ModoEliminar = false;
        }
    }
}
