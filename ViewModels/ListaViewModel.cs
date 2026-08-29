using CommunityToolkit.Mvvm.ComponentModel;
using RepasoMAUI.Data;
using RepasoMAUI.Models;
using RepasoMAUI.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RepasoMAUI.ViewModels
{
    public partial class ListaViewModel : ObservableObject
    {
        private readonly ProductoRepository _repo;

        public ObservableCollection<Producto> Productos { get; }

        public ICommand VerDetalleCommand { get; }

        public ListaViewModel(ProductoRepository repo)
        {
            _repo = repo;

            Productos = new ObservableCollection<Producto>(
                _repo.ObtenerTodos()
            );

            VerDetalleCommand =
                new Command<Producto>(async producto =>
                {
                    if (producto == null)
                        return;

                    await Shell.Current.GoToAsync(
                        $"/{nameof(DetallePage)}?id={producto.Id}"
                    );
                });
        }
    }
}