using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RepasoMAUI.Data;
using RepasoMAUI.Models;
using RepasoMAUI.Views;
using System.Collections.ObjectModel;

namespace RepasoMAUI.ViewModels
{
    public partial class ListaViewModel : ObservableObject
    {
        private readonly ProductoRepository _repo;

        [ObservableProperty]
        private ObservableCollection<Producto> productos;

        public ListaViewModel(ProductoRepository repo)
        {
            _repo = repo;
            Productos = new ObservableCollection<Producto>(_repo.ObtenerTodos());
        }

        [RelayCommand]
        static async Task VerDetalle(Producto producto)
        {
            if (producto is null) return;

            await Shell.Current.GoToAsync($"/{nameof(DetallePage)}?id={producto.Id}");
        }
    }
}
