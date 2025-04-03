using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Extensions.DependencyInjection;
using UBB_SE_2025_EUROTRUCKERS.ViewModels;
using UBB_SE_2025_EUROTRUCKERS.Views;
using UBB_SE_2025_EUROTRUCKERS.ViewModels;
using UBB_SE_2025_EUROTRUCKERS;

namespace UBB_SE_2025_EUROTRUCKERS.Views
{
    public sealed partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; }

        public MainWindow()
        {
            this.InitializeComponent();

            // Obtener ViewModel del contenedor DI
            ViewModel = App.Services.GetRequiredService<MainViewModel>();

        }
    }
}