using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using RepasoMAUI.Models;

namespace RepasoMAUI.Services
{
    public class FavoritosService
    {
        public ObservableCollection<Producto> Favoritos { get; } = new ObservableCollection<Producto>();

        public bool Agregar(Producto producto)
        {
            if (producto == null || Favoritos.Contains(producto))
                return false;

            Favoritos.Add(producto);
            return true;
        }

        public bool Eliminar(Producto producto)
        {
            if (producto == null || !Favoritos.Contains(producto))
                return false;

            Favoritos.Remove(producto);
            return false;
        }
    }
}
