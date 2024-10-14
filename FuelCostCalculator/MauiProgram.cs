using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace FuelCostCalculator
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var getAssembly = Assembly.GetExecutingAssembly();
            using var stream = getAssembly.GetManifestResourceStream("FuelCostCalculator.appsettings.json");

            var config = new ConfigurationBuilder()
                .AddJsonStream(stream)
                .Build();

            builder.Configuration.AddConfiguration(config);

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
