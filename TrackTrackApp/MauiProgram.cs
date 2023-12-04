using Microsoft.Extensions.Logging;
using TrackTrackApp.ViewModels;
using TrackTrackApp.Views;
//using TrackTrackApp.Services;

namespace TrackTrackApp
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

#if DEBUG
		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<SignUp>();
            builder.Services.AddSingleton<SignUpViewModel>();
            builder.Services.AddSingleton<Login>();
            builder.Services.AddSingleton<LoginViewModel>();



            Routing.RegisterRoute("MainPage", typeof(MainPage));
            return builder.Build();
        }
    }
}