using StoreManager.Models;
using StoreManager.Controllers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Popups;
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

        private async Task reload() {
            await App.StoreManager.Settings.Save();
            await App.StoreManager.Initialize();
            this.Frame.GoBack();
            this.Frame.GoForward();
        }

        private void ReposList_OnSelectionChanged(Object sender, SelectionChangedEventArgs e) {
            if (this.ReposList.SelectedItems.Count <= 0) {
                this.EditRepoBtn.Visibility = Visibility.Collapsed;
                this.AddRepoBtn.Visibility = Visibility.Visible;
                this.RemoveRepoBtn.Visibility = Visibility.Collapsed;
                this.EditRepoBtn.IsEnabled = false;
                this.AddRepoBtn.IsEnabled = true;
                this.RemoveRepoBtn.IsEnabled = true;
                return;
            }

            this.EditRepoBtn.Visibility = Visibility.Visible;
            this.AddRepoBtn.Visibility = Visibility.Collapsed;
            this.RemoveRepoBtn.Visibility = Visibility.Visible;
            this.EditRepoBtn.IsEnabled = true;
            this.AddRepoBtn.IsEnabled = false;
            this.RemoveRepoBtn.IsEnabled = true;
        }

        private async void RemoveRepoBtn_OnClick(Object sender, RoutedEventArgs e) {
            // ReSharper disable once InvertIf
            if ((this.ReposList.Items?.Count ?? 0) - this.ReposList.SelectedItems.Count <= 0) {
                var msg = new MessageDialog("You can't remove all repositories!");
                await msg.ShowAsync();
                return;
            }

            // FIXME: there's probably a better way to do this?
            foreach (RepositoryModel repo in this.ReposList.SelectedItems) {
                App.StoreManager.Settings.Config.Repositories.Remove(repo);
            }

            await this.reload();
        }

        private void EditRepoBtn_OnClick(Object sender, RoutedEventArgs e) {
            throw new NotImplementedException();
        }

        private async void AddRepoBtn_OnClick(Object sender, RoutedEventArgs e) {
            var input = new TextBox() {
                Height = (Double)App.Current.Resources["TextControlThemeMinHeight"],
                PlaceholderText = "https://w10m-research.github.io/StoreRepository/"
            };

            var dialog = new ContentDialog() {
                Title = "Add repository",
                MaxWidth = this.ActualWidth,
                PrimaryButtonText = "Add",
                SecondaryButtonText = "Cancel",
                Content = input
            };

            ContentDialogResult result = await dialog.ShowAsync();
            if (result != ContentDialogResult.Primary)
                return;

            // FIXME: validate input.
            App.StoreManager.Settings.Config.Repositories.Add(new RepositoryModel(((TextBox)dialog.Content).Text));
            await this.reload();
        }

        private void RootPivot_OnSelectionChanged(Object sender, SelectionChangedEventArgs e) {
            // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
            if (this.RootPivot.SelectedIndex == 0)
                this.AppBar.ClosedDisplayMode = AppBarClosedDisplayMode.Compact;
            else
                this.AppBar.ClosedDisplayMode = AppBarClosedDisplayMode.Minimal;
        }
    }
}
