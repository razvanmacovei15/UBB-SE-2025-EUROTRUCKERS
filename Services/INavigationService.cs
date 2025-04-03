using Microsoft.Extensions.DependencyInjection;
using System;
using UBB_SE_2025_EUROTRUCKERS.ViewModels;

namespace UBB_SE_2025_EUROTRUCKERS.Services
{
    public interface INavigationService
    {
        void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;
        void GoBack();
    }

    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            var viewModel = _serviceProvider.GetService<TViewModel>();
            // Logic for changing actual view model
        }

        public void GoBack()
        {
            // TODO: Implement logic to go back to the previous view model
        }
    }
}
