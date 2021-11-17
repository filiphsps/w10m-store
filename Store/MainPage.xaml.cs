using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Store
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(Object sender, RoutedEventArgs e)
        {
            try {
                await App.Repository.Initialize();

                try { await App.Repository.Settings.Save(); } catch { }

                // TODO: use the json data
                this.Frame.Navigate(typeof(Pages.PackagesPage), null, new DrillInNavigationTransitionInfo());
                this.Frame.BackStack.Remove(this.Frame.BackStack.Last());
            } catch (Exception ex) {
                // TODO: Show offline view
                var alert = new Windows.UI.Popups.MessageDialog(ex.Message, "Something went wrong :(");
                await alert.ShowAsync();
            }
        }
    }
}
