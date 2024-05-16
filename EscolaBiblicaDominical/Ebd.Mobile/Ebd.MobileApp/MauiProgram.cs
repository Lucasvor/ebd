using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace Ebd.Mobile;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSansRegular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSansMedium.ttf", "OpenSansMedium");
                fonts.AddFont("OpenSansBold.ttf", "OpenSansBold");
                fonts.AddFont("fa-solid-900.ttf", "FontAwesome");
            })
            .UseMauiCommunityToolkit();

        builder.Services
            .ConfigureAndHandleHttpClient()
            .ConfigureServices()
            .ConfigureRepositories()
            .ConfigureViewModels()
            .ConfigurePages()
            .BuildServiceProvider();
#if DEBUG
        builder.Logging.AddDebug();
#endif
        var app = builder.Build();
        DependencyInjection.Initialize(app.Services);

        return app;
    }
}
