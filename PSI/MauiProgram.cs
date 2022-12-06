using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using PSI.Views;
using PSI.UserAuthentication;
using PSI.ViewModels;
using Microsoft.Maui.Controls.Hosting;
using PSI.Services;
using PSI.Views.ManageLocation;

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
        builder.Services.AddTransient<ReportView>();
        builder.Services.AddTransient<AddLocationView>();
        builder.Services.AddTransient<DeleteLocationView>();
        builder.Services.AddSingleton<LocationsView>();

        builder.Services.AddSingleton<ReportViewModel>();
        builder.Services.AddTransient<ReportDetailPage>();
        builder.Services.AddTransient<DetailViewModel>();

        builder.Services.AddSingleton<SignInPage>();
        builder.Services.AddSingleton<SignUpPage>();

        builder.Services.AddHttpClient<ILocationService, LocationService>();
        builder.Services.AddHttpClient<ILogService, LogService>();
        builder.Services.AddHttpClient<ReportRestService>();


        return builder.Build();
    }
}
