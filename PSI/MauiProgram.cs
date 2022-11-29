using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using PSI.Views;
using PSI.UserAuthentication;
using PSI.ViewModels;
using Microsoft.Maui.Controls.Hosting;
using PSI.Services;

namespace PSI;

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
            })
            .UseMauiMaps();
            

        builder.UseMauiCommunityToolkit();

        builder.Services.AddSingleton<MainView>();
        builder.Services.AddSingleton<ReportViewModel>();
        builder.Services.AddTransient<AddLocationView>();
        builder.Services.AddTransient<ReportView>();

        builder.Services.AddSingleton<SignInPage>();
        builder.Services.AddSingleton<SignUpPage>();
        builder.Services.AddSingleton<LocationsView>();

        builder.Services.AddTransient<ReportDetailPage>();

        builder.Services.AddTransient<DetailViewModel>();

        builder.Services.AddHttpClient<ILocationService, LocationService>();
        builder.Services.AddHttpClient<ILogService, LogService>();


        return builder.Build();
    }
}
