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


            builder.Services.AddSingleton<ProductoRepository>();


            builder.Services.AddTransient<ListaViewModel>();
            builder.Services.AddTransient<ListaPage>();


            builder.Services.AddTransient<DetalleViewModel>();
            builder.Services.AddTransient<DetallePage>();


            builder.Services.AddSingleton<FavoritosViewModel>();
            builder.Services.AddTransient<FavoritosPage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

