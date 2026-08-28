using CommunityToolkit.Mvvm.ComponentModel;
using RepasoMAUI.Models;
using System.Collections.ObjectModel;

namespace RepasoMAUI.ViewModels
{
    public partial class FavoritosViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Producto> productosFavoritos = new();

        // Agrega un producto a la lista si no está ya agregado
        public void Agregar(Producto producto)
        {
            if (producto == null) return;

            if (!ProductosFavoritos.Any(p => p.Id == producto.Id))
            {
                ProductosFavoritos.Add(producto);
            }
        }
    }
}
