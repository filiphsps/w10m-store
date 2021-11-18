using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Store.Models;
using Windows.UI.Xaml.Media.Imaging;

namespace Store.Pages
{
    /// <summary>
    /// The "Install App" view
    /// </summary>
    public sealed partial class InstallerPage : Page
    {
        private AppModel _app;

        public InstallerPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            this._app = (AppModel)e.Parameter;

            this.AppNameStr.Text = this._app.Title;
            this.AppAuthorStr.Text = this._app.Author;
            this.AppImg.Source = new BitmapImage(new Uri(this._app.LogoUrl));
            this.ProgressStr.Text = "";
        }

        private async void ActionBtn_Click(Object sender, RoutedEventArgs e)
        {
            this.CancelBtn.Visibility = Visibility.Collapsed;
            this.ActionBtn.Visibility = Visibility.Collapsed;

            // TODO: Utility function?
            this.ProgressStr.Text += "Installing \"" + this._app.Title + "\"\n";

            // TODO: subscribe to progress updates
            this.ProgressStr.Text += "Staring download...\n";
            await App.StoreManager.Downloader.Download(this._app);

            // TODO: subscribe to progress updates
            this.ProgressStr.Text += "Starting installation...\n";
            await App.StoreManager.Installer.Install(this._app);

            this.CancelBtn.Content = "Done";
            this.CancelBtn.Visibility = Visibility.Visible;
        }

        private void CancelBtn_Click(Object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
