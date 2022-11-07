using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using PSI.Views;
using PSI.UserAuthentication;
using PSI.ViewModels;
using PSI.Database;
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
            });

        builder.UseMauiCommunityToolkit();

        builder.Services.AddSingleton<MainView>();
        builder.Services.AddSingleton<ReportViewModel>();
        builder.Services.AddTransient<AddLocationView>();
        builder.Services.AddTransient<SelectionView>();
        builder.Services.AddTransient<ReportView>();

        builder.Services.AddSingleton<SignInPage>();
        builder.Services.AddSingleton<SignUpPage>();
        builder.Services.AddSingleton<UserDataBase>();

        builder.Services.AddTransient<ReportDetailPage>();
        builder.Services.AddTransient<DetailViewModel>();

        builder.Services.AddSingleton<IRestService, RestService>();
        builder.Services.AddSingleton<ITodoService, TodoService>();


        return builder.Build();
    }
}
