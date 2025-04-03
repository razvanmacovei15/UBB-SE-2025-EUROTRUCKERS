using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using UBB_SE_2025_EUROTRUCKERS.Models;
using UBB_SE_2025_EUROTRUCKERS.Services;

namespace UBB_SE_2025_EUROTRUCKERS.ViewModels
{
    public partial class DeliveriesViewModel : ViewModelBase
    {
        private readonly IDeliveryService _deliveryService;

        [ObservableProperty]
        private ObservableCollection<Delivery> _deliveries = new();

        public DeliveriesViewModel(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
            Title = "Deliveries";
            LoadDeliveriesCommand = new AsyncRelayCommand(LoadDeliveriesAsync);
        }

        public IAsyncRelayCommand LoadDeliveriesCommand { get; }

        private async Task LoadDeliveriesAsync()
        {
            IsBusy = true;
            try
            {
                var deliveries = await _deliveryService.GetActiveDeliveriesAsync();
                Deliveries = new ObservableCollection<Delivery>(deliveries);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading deliveries: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
