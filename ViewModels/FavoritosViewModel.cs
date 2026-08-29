using CommunityToolkit.Mvvm.ComponentModel;
using RepasoMAUI.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RepasoMAUI.ViewModels
{
    public class FavoritosViewModel : ObservableObject
    {
        public ObservableCollection<Producto> Favoritos { get; }
            = new ObservableCollection<Producto>();

        public ICommand EliminarFavoritoCommand { get; }

        public FavoritosViewModel()
        {
            EliminarFavoritoCommand =
                new Command<Producto>(EliminarFavorito);
        }

        public void AgregarFavorito(Producto producto)
        {
            if (producto == null)
                return;

            // No permitir productos duplicados
            if (!Favoritos.Any(p => p.Id == producto.Id))
            {
                Favoritos.Add(producto);
            }
        }

        private void EliminarFavorito(Producto producto)
        {
            if (producto == null)
                return;

            Favoritos.Remove(producto);
        }
    }
}