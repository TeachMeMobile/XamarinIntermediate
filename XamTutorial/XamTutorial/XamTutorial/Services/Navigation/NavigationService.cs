using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamTutorial.Pages;
using XamTutorial.ViewModels;
using XamTutorial.ViewModels.Base;

namespace XamTutorial.Services.Navigation
{
    public class NavigationService : INavigationService
    {
        public ViewModelBase PreviousPageViewModel
        {
            get
            {
                if (App.Current.MainPage is CustomNavigationPage navPage)
                {
                    if (navPage.Navigation.NavigationStack.Count >= 2)
                    {
                        var viewModel = navPage.Navigation.NavigationStack[navPage.Navigation.NavigationStack.Count - 2].BindingContext;
                        return viewModel as ViewModelBase;
                    }
                }
                return null;
            }
        }

        public Task<Page> GoBackAsync()
        {
            if (App.Current.MainPage is CustomNavigationPage navPage)
            {
                if (navPage.Navigation.NavigationStack.Count >= 2)
                {
                    return navPage.Navigation.PopAsync();
                }
            }
            return null;
        }

        public Task InitializeAsync()
        {
            // if you have a login page:
            // -- Check your current access token and validate it. If it is valid, go to Home Page
            // -- If it is not valid, go to the login page.
            return NavigateToAsync<HomePageViewModel>();
        }

        public Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), parameter);
        }

        public Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase
        {
            return InternalNavigateToAsync(typeof(TViewModel), null);
        }

        public Task NavigateToAsync(Page page)
        {
            if (App.Current.MainPage is CustomNavigationPage navPage)
            {
                return navPage.PushAsync(page);
            }
            return Task.FromResult(false);
        }

        public Task RemoveBackStackAsync()
        {
            if (App.Current.MainPage is CustomNavigationPage navPage)
            {
                for (int i = 0; i < navPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = navPage.Navigation.NavigationStack[i];
                    navPage.Navigation.RemovePage(page);
                }
            }
            return Task.FromResult(true);
        }

        public Task RemoveLastFromBackStackAsync()
        {
            if (App.Current.MainPage is CustomNavigationPage navPage)
            {
                var navStack = navPage.Navigation.NavigationStack;
                if (navStack.Count >= 2)
                {
                    navPage.Navigation.RemovePage(navStack[navStack.Count - 2]);
                }
            }
            return Task.FromResult(true);
        }

        private async Task InternalNavigateToAsync(Type viewModelType, object parameter)
        {
            Page page = CreatePage(viewModelType);

            if (page is HomePage)
            {
                App.Current.MainPage = new CustomNavigationPage(page);
            }
            else if (App.Current.MainPage is CustomNavigationPage navPage)
            {
                await navPage.Navigation.PushAsync(page);
            } else
            {
                App.Current.MainPage = new CustomNavigationPage(page);
            }

            if (page.BindingContext is ViewModelBase vm)
            {
                await vm.InitializeAsync(parameter);
            }
        }

        private Type GetPageTypeForViewModel(Type viewModelType)
        {
            var viewName = viewModelType.FullName.Replace("ViewModels", "Pages").Replace("ViewModel", string.Empty);
            var viewModelAssemblyName = viewModelType.GetTypeInfo().Assembly.FullName;
            var viewAssemblyName = string.Format(CultureInfo.InvariantCulture, "{0}, {1}", viewName, viewModelAssemblyName);
            var viewType = Type.GetType(viewAssemblyName);
            return viewType;
        }

        private Page CreatePage(Type viewModelType)
        {
            Type pageType = GetPageTypeForViewModel(viewModelType);
            if (pageType == null)
            {
                throw new Exception($"Cannot locate page type for {viewModelType}");
            }

            Page page = Activator.CreateInstance(pageType) as Page;
            return page;
        }
    }
}
