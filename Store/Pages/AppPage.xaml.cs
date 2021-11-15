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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Store.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppPage : Page
    {
        public AppPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var anim = ConnectedAnimationService.GetForCurrentView().GetAnimation("ForwardConnectedAnimation");
            if (anim != null)
            {
                anim.TryStart(this.AppImg);
            }

            AppModel app = (AppModel)e.Parameter;

            if (app == null) {
                // TODO: show error
                return;
            }

            PrimaryPivot.Title = app.Title;
            AppNameStr.Text = app.Title;
            AppAuthorStr.Text = app.Author;
            AppVersionStr.Text = app.Version.ToString();
            AppDescStr.Text = app.Description;
            AppImg.Source = new BitmapImage(new Uri(app.LogoUrl));

            if (app.Timestamp != null)
                AppTimestampStr.Text = app.Timestamp.ToLocalTime().ToString("dd MMMM yyyy");
            AppSizeStr.Text = app.Size.ToString();

            DependencyList.ItemsSource = app.Dependencies;
            ContributorsList.ItemsSource = app.Contributors;

            base.OnNavigatedTo(e);
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
