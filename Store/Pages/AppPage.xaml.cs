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
using Windows.UI.Xaml.Media.Imaging;
using Store.Models;
using Windows.UI.Xaml.Media.Animation;
using System.Threading.Tasks;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Store.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppPage : Page
    {
        private AppModel app;

        public AppPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("appLogoIn");
            animation?.TryStart(this.AppImgWrapper);

            this.app = (AppModel)e.Parameter;

            if (this.app == null) {
                // TODO: show error
                return;
            }

            this.PrimaryPivot.Title = this.app.Title;
            this.AppNsStr.Text = this.app.Namespace;
            this.AppNameStr.Text = this.app.Title;
            this.AppAuthorStr.Text = this.app.Author;
            this.AppVersionStr.Text = this.app.Version.ToString();
            this.AppDescStr.Text = this.app.Description;
            this.AppImg.Source = new BitmapImage(new Uri(this.app.LogoUrl));

            if (this.app.Timestamp != null)
                this.AppTimestampStr.Text = this.app.Timestamp.ToLocalTime().ToString("dd MMMM yyyy");

            this.AppSizeStr.Text = this.app.Size.ToString();

            this.DependencyList.ItemsSource = this.app.Dependencies;
            this.ContributorsList.ItemsSource = this.app.Contributors;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            // ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("appLogoOut", this.AppImgWrapper);
        }

        private void InstallBtn_Click(object sender, RoutedEventArgs e)
        {
            // TODO
        }

        private void AppAuthorStr_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // TODO: Navigate to AuthorView
        }
    }
}
