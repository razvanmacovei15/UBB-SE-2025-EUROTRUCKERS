using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using UBB_SE_2025_EUROTRUCKERS.Models;
using UBB_SE_2025_EUROTRUCKERS.Services;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace UBB_SE_2025_EUROTRUCKERS.ViewModels
{
    public partial class DeliveriesViewModel : ViewModelBase
    {
        private readonly IDeliveryService _deliveryService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private ObservableCollection<Delivery> _deliveries = new();

        public DeliveriesViewModel(
            IDeliveryService deliveryService,
            INavigationService navigationService)
        {
            _deliveryService = deliveryService;
            _navigationService = navigationService;
            Title = "Deliveries";
            LoadDeliveriesCommand = new AsyncRelayCommand(LoadDeliveriesAsync);
            NavigateToDetailsCommand = new RelayCommand<Delivery>(NavigateToDetails);

            _ = LoadDeliveriesCommand.ExecuteAsync(null);
        }

        public IAsyncRelayCommand LoadDeliveriesCommand { get; }
        public IRelayCommand<Delivery> NavigateToDetailsCommand { get; }

        private async Task LoadDeliveriesAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {
                var deliveries = await _deliveryService.GetAllDeliveriesAsync();

                if (deliveries == null || !deliveries.Any())
                {
                    Console.WriteLine("No deliveries found.");
                }
                else
                {
                    Console.WriteLine($"Found {deliveries.Count()} deliveries.");
                }

                Deliveries.Clear();
                foreach (var delivery in deliveries)
                {
                    Deliveries.Add(delivery);
                }
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




        private void NavigateToDetails(Delivery delivery)
        {
            if (delivery != null)
            {
                _navigationService.NavigateToWithParameter<DetailsViewModel>(delivery);
            }
        }
    }
}