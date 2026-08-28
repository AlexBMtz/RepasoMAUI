using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Models;
using System.Collections.ObjectModel;

namespace RepasoMAUI.ViewModels
{
    public partial class FavoritosViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Producto> productos = new();

        [RelayCommand]
        public void AgregarFavorito(Producto producto)
        {
            if (producto == null)
                return;


            if (!Productos.Any(p => p.Id == producto.Id))
            {
                Productos.Add(producto);
            }
        }

        [RelayCommand]
        public void EliminarFavorito(Producto producto)
        {
            if (producto == null)
                return;

            Productos.Remove(producto);
        }
    }
}

