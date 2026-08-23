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

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
