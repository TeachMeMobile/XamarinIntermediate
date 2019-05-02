using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace XamTutorial.ViewModels
{
    public class HomePageViewModel : Base.ViewModelBase
    {
        private string _city;
        private string _temp;
        private string _condition;
        private string _conditionIcon;

        public string City
        {
            get => _city;
            set { _city = value; RaisePropertyChanged(nameof(City)); }
        }

        public string Temperature
        {
            get => _temp;
            set { _temp = value; RaisePropertyChanged(nameof(Temperature)); }
        }

        public string Condition
        {
            get => _condition;
            set { _condition = value; RaisePropertyChanged(nameof(Condition)); }
        }

        public string ConditionIcon
        {
            get => _conditionIcon;
            set { _conditionIcon = value; RaisePropertyChanged(nameof(ConditionIcon)); }
        }

        public ICommand SettingsCommand => new Command(async() =>
        {
            // when settings icon is tapped, go to settings page
            await NavigateService.NavigateToAsync<SettingsPageViewModel>();
        });
        

        public HomePageViewModel()
        {
           
        }

        public override Task InitializeAsync(object navigationData)
        {
            // Temporary default information, to be replace by services (mock service at first);
            City = "Chicago, IL";
            Temperature = "50.5";
            Condition = "Partly Cloudy";
            ConditionIcon = "cloud.png";

            return base.InitializeAsync(navigationData);
        }
    }
}
