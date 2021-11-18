using System;
using System.Collections.ObjectModel;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Store.Pages {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    internal sealed partial class SettingsPage {
        public SettingsPage() {
            this.InitializeComponent();

            PackageVersion version = Package.Current.Id.Version;
            this.AppVersionStr.Text = String.Format($"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}");

            this.AppPkgsStr.Text = App.StoreManager.Packages.Count.ToString();
            this.AppDepsStr.Text = App.StoreManager.Dependencies.Count.ToString();
            this.AppResCntStr.Text = App.StoreManager.Repositories.Count.ToString();
            this.ReposList.ItemsSource = App.StoreManager.Repositories;
        }

        private void ReposList_OnSelectionChanged(Object sender, SelectionChangedEventArgs e) {
            if (this.ReposList.SelectedItems.Count <= 0) {
                this.EditRepoBtn.Visibility = Visibility.Collapsed;
                this.AddRepoBtn.Visibility = Visibility.Visible;
                this.RemoveRepoBtn.IsEnabled = false;
                return;
            }

            this.EditRepoBtn.Visibility = Visibility.Visible;
            this.AddRepoBtn.Visibility = Visibility.Collapsed;
            this.RemoveRepoBtn.IsEnabled = true;
        }

        private void RemoveRepoBtn_OnClick(Object sender, RoutedEventArgs e) {
            throw new NotImplementedException();
        }
    }
}
