using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using UBB_SE_2025_EUROTRUCKERS.Models;
using Microsoft.UI.Xaml;
using System;

namespace Delivery_Details_Part.Views
{
    public sealed partial class DetailsPage : Page
    {
        public Delivery SelectedDelivery { get; set; }

        public DetailsPage()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            global::System.Uri resourceLocator = new global::System.Uri("ms-appx:///Views/DetailsPage.xaml");
            global::Microsoft.UI.Xaml.Application.LoadComponent(this, resourceLocator, global::Microsoft.UI.Xaml.Controls.Primitives.ComponentResourceLocation.Application);
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

