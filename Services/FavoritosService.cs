using RepasoMAUI.Models;
using System.Collections.ObjectModel;

namespace RepasoMAUI.Services
{
    /// <summary>
    /// Servicio Singleton que mantiene la lista de favoritos en memoria
    /// durante todo el ciclo de vida de la app.
    /// </summary>
    public class FavoritosService
    {
        private readonly HashSet<string> _ids = [];

        public ObservableCollection<Producto> Favoritos { get; } = [];

        /// <summary>
        /// Agrega un producto si no existe ya en la lista.
        /// Retorna true si se agregó, false si ya existía.
        /// </summary>
        public bool Agregar(Producto producto)
        {
            if (producto is null || !_ids.Add(producto.Id))
                return false;

            Favoritos.Add(producto);
            return true;
        }
    }
}
