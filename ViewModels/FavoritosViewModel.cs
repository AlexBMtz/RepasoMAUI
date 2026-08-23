using CommunityToolkit.Mvvm.ComponentModel;
using RepasoMAUI.Data;
using RepasoMAUI.Models;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using System.Linq;

namespace RepasoMAUI.ViewModels
{
    public partial class FavoritosViewModel : ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<Producto> favoritos = new();

        
        public void AgregarFavorito(Producto producto)
        {
            if (producto is null) return;
            if (!Favoritos.Any(p => p.Id == producto.Id))
                Favoritos.Add(producto);
        }

        [RelayCommand]
        private void EliminarFavorito(Producto producto)
        {
            if (producto is null) return;
            Favoritos.Remove(producto);
        }

    }
}
