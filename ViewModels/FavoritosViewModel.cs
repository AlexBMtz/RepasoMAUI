using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Collections.Specialized;

namespace RepasoMAUI.ViewModels
{
    public partial class FavoritosViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Producto> favoritos = new();

        public FavoritosViewModel()
        {
            // Update Count when collection changes so UI can reflect the number of favorites
            Favoritos.CollectionChanged += Favoritos_CollectionChanged;
        }

        private void Favoritos_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Count));
        }

        public int Count => Favoritos?.Count ?? 0;

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
