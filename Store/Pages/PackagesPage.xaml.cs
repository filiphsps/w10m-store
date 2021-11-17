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
using System.Diagnostics;
using Windows.UI.Xaml.Media.Animation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Store.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PackagesPage : Page
    {
        //private Models.AppModel lastItem;

        public PackagesPage()
        {
            this.InitializeComponent();

            this.AllPkgsList.ItemsSource = App.Repository.Packages.Values;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // Remove selection (hacky)
            this.AllPkgsList.SelectedItem = null;

            /* var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("appLogoOut");
            if (animation != null)
            {
                await AllPkgsList.TryStartConnectedAnimationAsync(animation, this.lastItem, "LogoImgWrapper");
            } */
        }

        private void SettingsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.SettingsPage), null, new EntranceNavigationTransitionInfo());
        }

        private void AllPkgsList_ItemClick(object sender, ItemClickEventArgs e)
        {
            //this.lastItem = (Models.AppModel)e.ClickedItem;
            this.AllPkgsList.PrepareConnectedAnimation("appLogoIn", e.ClickedItem, "LogoImgWrapper");
            this.Frame.Navigate(typeof(Pages.AppPage), (Models.AppModel)e.ClickedItem, new SuppressNavigationTransitionInfo());
        }
    }
}
