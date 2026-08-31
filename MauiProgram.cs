using Microsoft.Extensions.Logging;
using RepasoMAUI.Data;
using RepasoMAUI.ViewModels;
using RepasoMAUI.Views;

namespace RepasoMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Repositorio — Singleton: una sola instancia para toda la app
            builder.Services.AddSingleton<ProductoRepository>();

            // Lista
            builder.Services.AddTransient<ListaViewModel>();
            builder.Services.AddTransient<ListaPage>();

            // Detalle
            builder.Services.AddTransient<DetalleViewModel>();
            builder.Services.AddTransient<DetallePage>();

            // Favoritos
            // ViewModel como Singleton: es lo que permite que la lista de
            // favoritos persista en memoria durante toda la vida de la app,
            // sin importar cuántas veces se navegue entre Detalle y Favoritos.
            builder.Services.AddSingleton<FavoritosViewModel>();
            // La página puede seguir siendo Transient: cada vez que Shell
            // navega a "favoritos" crea una instancia nueva de la página,
            // pero DI le inyecta siempre la MISMA instancia del ViewModel.
            builder.Services.AddTransient<FavoritosPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}