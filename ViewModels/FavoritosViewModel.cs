using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace RepasoMAUI.ViewModels
{
    public partial class FavoritosViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Producto> favoritos = new();

        public void Agregar(Producto producto)
        {
            if (producto is null) return;

            if (Favoritos.Any(p => p.Id == producto.Id)) return;

            Favoritos.Add(producto);
        }

        [RelayCommand]
        void Eliminar(Producto producto)
        {
            if (producto is null) return;

            Favoritos.Remove(producto);
        }
    }
}
