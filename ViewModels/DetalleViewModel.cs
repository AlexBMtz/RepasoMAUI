using CommunityToolkit.Mvvm.ComponentModel;
using RepasoMAUI.Data;
using RepasoMAUI.Models;
using RepasoMAUI.Views;
using System.Windows.Input;

namespace RepasoMAUI.ViewModels
{
    [QueryProperty(nameof(Id), "id")]
    public class DetalleViewModel : ObservableObject
    {
        private readonly ProductoRepository _repo;
        private readonly FavoritosViewModel _favoritos;

        private string _id;
        private Producto _producto;

        public string Id
        {
            get => _id;
            set
            {
                if (SetProperty(ref _id, value))
                {
                    Producto = _repo.ObtenerPorId(value);
                }
            }
        }

        public Producto Producto
        {
            get => _producto;
            set => SetProperty(ref _producto, value);
        }

        public ICommand AgregarFavoritoCommand { get; }

        public ICommand VerFavoritosCommand { get; }

        public DetalleViewModel(
            ProductoRepository repo,
            FavoritosViewModel favoritos)
        {
            _repo = repo;
            _favoritos = favoritos;

            AgregarFavoritoCommand =
                new Command(AgregarFavorito);

            VerFavoritosCommand =
                new Command(async () => await VerFavoritos());
        }

        private void AgregarFavorito()
        {
            if (Producto == null)
                return;

            _favoritos.AgregarFavorito(Producto);
        }

        private async Task VerFavoritos()
        {
            await Shell.Current.GoToAsync(nameof(FavoritosPage));
        }
    }
}