using AndroidX.Lifecycle;
using RepasoMAUI.ViewModels;

namespace RepasoMAUI.Views
{
    public partial class FavoritosPage : ContentPage
    {
        private readonly FavoritosViewModel _viewModel;

        public FavoritosPage(FavoritosViewModel viewModel)
        {
            InitializeComponent();

            BindingContext = _viewModel = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _viewModel.CargarFavoritos();
        }
}
