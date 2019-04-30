using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamTutorial.ViewModels
{
    public class HomePageViewModel : Base.ViewModelBase
    {
        private List<string> _menuItems;

        public List<string> MenuItems
        {
            get => _menuItems;
            set { _menuItems = value; RaisePropertyChanged(nameof(MenuItems)); }
        }

        public HomePageViewModel()
        {
            MenuItems = new List<string>
            {
                "Custom Views",
                "API Calls"
            };
        }

        public override Task InitializeAsync(object navigationData)
        {
            return base.InitializeAsync(navigationData);
        }
    }
}
