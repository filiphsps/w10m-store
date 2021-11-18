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

            throw new NotImplementedException();

            this.CancelBtn.Visibility = Visibility.Visible;
        }

        private void CancelBtn_Click(Object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
