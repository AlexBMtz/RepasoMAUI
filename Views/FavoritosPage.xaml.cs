using AndroidX.Lifecycle;
using RepasoMAUI.ViewModels;

namespace RepasoMAUI.Views
{
    public partial class FavoritosPage : ContentPage
    {
        private readonly FavoritosViewModel _viewModel;
        public FavoritosPage(FavoritosViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.CargarFavoritos();
        }
    }
}
