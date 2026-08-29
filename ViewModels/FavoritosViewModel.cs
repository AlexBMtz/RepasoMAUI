using CommunityToolkit.Mvvm.ComponentModel;
using RepasoMAUI.Models;
using System.Collections.ObjectModel;

namespace RepasoMAUI.ViewModels
{
    [QueryProperty(nameof(Favoritos), "Favoritos")]
    public partial class FavoritosViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Producto> favoritos;

        public FavoritosViewModel()
        {
        }
    }
}