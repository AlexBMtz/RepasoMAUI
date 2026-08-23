using RepasoMAUI.ViewModels;

namespace RepasoMAUI.Views
{
    public partial class ListaPage : ContentPage
    {
        public ListaPage(ListaViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
