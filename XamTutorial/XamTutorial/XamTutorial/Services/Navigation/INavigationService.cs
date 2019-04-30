using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamTutorial.Services.Navigation
{
    public interface INavigationService
    {
        ViewModels.Base.ViewModelBase PreviousPageViewModel { get; }
        Task InitializeAsync();
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModels.Base.ViewModelBase;
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModels.Base.ViewModelBase;
        Task NavigateToAsync(Page page);
        Task RemoveLastFromBackStackAsync();
        Task RemoveBackStackAsync();
        Task<Page> GoBackAsync();
    }
}
