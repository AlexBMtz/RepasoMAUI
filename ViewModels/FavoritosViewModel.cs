using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace RepasoMAUI.ViewModels
{
   public partial class FavoritosViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Producto> favoritos;

        public FavoritosViewModel()
        {
            favoritos = new ObservableCollection<Producto>();
        }

        [RelayCommand]
        private void EliminarFavorito(Producto producto)
        {
            if (producto is null) return;
            Favoritos.Remove(producto);
        }

        public void AgregarFavorito(Producto producto)
        {
            if (producto is null) return;
            
            if (!favoritos.Any(p => p.Id == producto.Id))
            {
                Favoritos.Add(producto);
            }
        }
    }
}
