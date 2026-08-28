using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Models;
using System.Collections.ObjectModel;

namespace RepasoMAUI.ViewModels
{
    public partial class FavoritosViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Producto> productosFavoritos = new();

        // Agrega un producto a la lista si no está ya agregado y retorna si se pudo agregar
        public bool Agregar(Producto producto)
        {
            if (producto == null) return false;

            if (ProductosFavoritos.Any(p => p.Id == producto.Id))
            {
                return false;
            }

            ProductosFavoritos.Add(producto);
            return true;
        }

        // Elimina un producto de la lista de favoritos
        [RelayCommand]
        void Eliminar(Producto producto)
        {
            if (producto == null) return;
            ProductosFavoritos.Remove(producto);
        }
    }
}
