using CommunityToolkit.Maui;
using Microsoft.Extensions.DependencyInjection;
using PSI.Views;
using PSI.UserAuthentication;
using PSI.ViewModels;

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
        builder.Services.AddSingleton<ArticleViewModel>();
        builder.Services.AddTransient<AddLocationView>();
        builder.Services.AddTransient<SelectionView>();
        builder.Services.AddTransient<ReportView>();
        builder.Services.AddTransient<SignInPage>();
        builder.Services.AddTransient<SignUpPage>();

        return builder.Build();
    }
}
