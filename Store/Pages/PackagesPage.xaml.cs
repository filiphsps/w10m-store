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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Store.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PackagesPage : Page
    {
        public PackagesPage()
        {
            this.InitializeComponent();
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.SettingsPage));
        }

        private void DebugBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.AppPage));
        }
    }
}
