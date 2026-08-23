using RepasoMAUI.Views;

namespace RepasoMAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(DetallePage), typeof(DetallePage));
        }
    }
}
