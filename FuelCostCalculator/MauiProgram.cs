using CommunityToolkit.Maui;

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

            builder.Services.AddSingleton<HistoryItemDb>();
            builder.Services.AddTransient<MainPage>();

            return builder.Build();
        }
    }
}