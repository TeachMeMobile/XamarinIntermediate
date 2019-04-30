using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Text;
using Xamarin.Forms;
using XamTutorial.Services.Dialog;
using XamTutorial.Services.Navigation;
using XamTutorial.Services.Settings;

namespace XamTutorial.ViewModels.Base
{
    public static class ViewModelLocator
    {
        private static TinyIoC.TinyIoCContainer _container;

        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.Create("AutoWireViewModel", typeof(bool), typeof(ViewModelLocator), default(bool), propertyChanged: OnAutoWireViewModelChanged);

        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(ViewModelLocator.AutoWireViewModelProperty);
        }

        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(ViewModelLocator.AutoWireViewModelProperty, value);
        }
        
        public static bool UseMockServices { get; set; }

        static ViewModelLocator()
        {
            _container = new TinyIoC.TinyIoCContainer();

            // View models - by default, TinyIoC will register concrete classes as multi-instance
            _container.Register<HomePageViewModel>();

            // Services - by default, TinyIoC will regerter interface registrations as singletons.
            // Register services that do not depend on a mock service counterpart
            _container.Register<IDialogService, DialogService>();
            _container.Register<INavigationService, NavigationService>();
            // place any service registrations that have a mock counterpart in the UpdateDependencies method
            UpdateDependencies(false);
        }

        public static void UpdateDependencies(bool useMocks)
        {
            // Change the injected dependencies as required
            if (useMocks)
            {
                // register mock services
            } else
            {
                // register actual services
                _container.Register<ISettingsService, SettingsService>();
            }
            UseMockServices = useMocks;
        }

        public static void RegisterSingleton<TInterface, T>() where TInterface : class where T: class, TInterface
        {
            _container.Register<TInterface, T>().AsSingleton();
        }

        public static T Resolve<T>() where T : class
        {
            return _container.Resolve<T>();
        }

        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var view = bindable as Element;
            if (view == null) return;

            var viewType = view.GetType();
            var viewName = viewType.FullName.Replace(".Pages.", ".ViewModels.");
            var viewAssemblyName = viewType.GetTypeInfo().Assembly.FullName;
            var viewModelName = string.Format(CultureInfo.InvariantCulture, "{0}ViewModel, {1}", viewName, viewAssemblyName);

            var viewModelType = Type.GetType(viewModelName);
            if (viewModelType == null) return;
            var viewModel = _container.Resolve(viewModelType);
            view.BindingContext = viewModel;
        }
    }
}
