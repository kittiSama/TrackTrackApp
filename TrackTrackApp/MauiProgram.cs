using Microsoft.Extensions.Logging;
using TrackTrackApp.Services;
using TrackTrackApp.ViewModels;
using TrackTrackApp.Views;
using Microcharts.Maui;
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
                .UseMicrocharts()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "RealOpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                    fonts.AddFont("ConcertOne-Regular.ttf", "concertOne");
                    fonts.AddFont("Assistant-VariableFont_wght.ttf", "OpenSansRegular");
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
            builder.Services.AddSingleton<UserMainPage>();
            builder.Services.AddSingleton<UserMainPageViewModel>();
            builder.Services.AddSingleton<SearchPage>();
            builder.Services.AddSingleton<SearchPageViewModel>();
            builder.Services.AddSingleton<DataPage>();
            builder.Services.AddSingleton<DataPageViewModel>();

            builder.Services.AddSingleton<TrackTrackServices>();

            Routing.RegisterRoute("MainPage", typeof(MainPage));
            return builder.Build();
        }
    }
}