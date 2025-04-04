using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using UBB_SE_2025_EUROTRUCKERS.Models;
using Microsoft.UI.Xaml;
using System;
using Microsoft.Extensions.DependencyInjection;
using UBB_SE_2025_EUROTRUCKERS.ViewModels;

namespace UBB_SE_2025_EUROTRUCKERS.Views
{
    public sealed partial class DetailsView : Page
    {
        public Delivery SelectedDelivery { get; set; }
        public DetailsViewModel ViewModel { get; }

        public DetailsView()
        {
            this.InitializeComponent();
            ViewModel = App.Services.GetRequiredService<DetailsViewModel>();
            this.DataContext = ViewModel;
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is Delivery delivery)
            {
                SelectedDelivery = delivery;
                this.DataContext = SelectedDelivery;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

    }
}

