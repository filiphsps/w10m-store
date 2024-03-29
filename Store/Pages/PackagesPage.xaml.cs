﻿using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Animation;
using StoreManager.Models;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Store.Pages {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    internal sealed partial class PackagesPage {
        //private Models.AppModel _lastItem;

        public PackagesPage() {
            this.InitializeComponent();
            this.AllPkgsList.ItemsSource = App.StoreManager.Packages.Values;
        }

        private async Task Reload() {
            await App.StoreManager.Initialize();
            this.AllPkgsList.ItemsSource = App.StoreManager.Packages.Values;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);

            // Remove selection (hacky)
            this.AllPkgsList.SelectedItem = null;

            /* ConnectedAnimation animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("appLogoOut");
            if (animation != null)
            {
                await this.AllPkgsList.TryStartConnectedAnimationAsync(animation, this._lastItem, "LogoImgWrapper");
            } */
        }

        private void SettingsBtn_Click(Object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(SettingsPage), null, new EntranceNavigationTransitionInfo());
        }

        private void AllPkgsList_ItemClick(Object sender, ItemClickEventArgs e) {
            //this.lastItem = (Models.AppModel)e.ClickedItem;
            this.AllPkgsList.PrepareConnectedAnimation("appLogoIn", e.ClickedItem, "AppWrapper");
            this.Frame.Navigate(typeof(AppPage), (AppModel)e.ClickedItem, new SuppressNavigationTransitionInfo());
        }

        private async void DonateBtn_OnClick(Object sender, RoutedEventArgs e) {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("https://github.com/sponsors/filiphsandstrom"));
        }

        private async void ReloadBtn_OnClick(Object sender, RoutedEventArgs e) {
            await this.Reload();
        }
    }
}
