using CommunityToolkit.Mvvm.ComponentModel;
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
    }
}
