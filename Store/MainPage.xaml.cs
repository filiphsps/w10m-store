using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;

namespace Store {
    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();
        }

        private async void Page_Loaded(Object sender, RoutedEventArgs e) {
            await App.StoreManager.Initialize();
            await App.StoreManager.Settings.Save();

            this.Frame.Navigate(typeof(Pages.PackagesPage), null, new DrillInNavigationTransitionInfo());
            this.Frame.BackStack.Remove(this.Frame.BackStack.Last());
        }
    }
}
    
