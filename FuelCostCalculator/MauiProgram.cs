using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace FuelCostCalculator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Roboto-VariableFont_wght.ttf", "RobotoVariableFont");
                    fonts.AddFont("Roboto-Italic-VariableFont_wght.ttf", "RobotoItalicVariableFont");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<HistoryItemDb>();

            return builder.Build();
        }
    }
}
