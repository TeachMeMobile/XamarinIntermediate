using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamTutorial.Services.Navigation;
using XamTutorial.Services.Settings;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamTutorial
{
    public partial class App : Application
    {
        ISettingsService _settingsService;
        public App()
        {
            InitializeComponent();
            InitApp();
            if (Device.RuntimePlatform == Device.UWP)
            {
                InitNavigation();
            }
        }

        private void InitApp()
        {
            _settingsService = ViewModels.Base.ViewModelLocator.Resolve<ISettingsService>();
            if (_settingsService.UseMocks)
            {
                ViewModels.Base.ViewModelLocator.UpdateDependencies(_settingsService.UseMocks);
            }
        }

        private Task InitNavigation()
        {
            var navService = ViewModels.Base.ViewModelLocator.Resolve<INavigationService>();
            return navService.InitializeAsync();
        }

        protected override async void OnStart()
        {
            base.OnStart();

            if (Device.RuntimePlatform != Device.UWP)
            {
                await InitNavigation();
            }

            base.OnResume();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
    }
}
