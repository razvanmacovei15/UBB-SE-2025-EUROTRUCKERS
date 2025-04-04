using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UBB_SE_2025_EUROTRUCKERS.Models;
using UBB_SE_2025_EUROTRUCKERS.Services;

namespace UBB_SE_2025_EUROTRUCKERS.ViewModels
{
    public partial class DetailsViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<Delivery> _deliverieDetails = new();
        private void NavigateToDetails(Delivery delivery)
        {
            if (delivery != null)
            {
                _navigationService.NavigateToWithParameter<DetailsViewModel>(delivery);
            }
        }
    }
}
