using Microsoft.Extensions.Logging;

using Microsoft.Extensions.DependencyInjection;

namespace People;

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

        // Configurar la ruta de la base de datos
        string dbPath = FileAccessHelper.GetLocalFilePath("people_scabrera.db3");

        // Registrar PersonRepository como un servicio
        builder.Services.AddSingleton<PersonRepository>(s =>
            ActivatorUtilities.CreateInstance<PersonRepository>(s, dbPath));

        return builder.Build();
    }
}

