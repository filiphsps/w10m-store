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
using Windows.ApplicationModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Store.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();

            var version = Package.Current.Id.Version;
            this.AppVersionStr.Text = String.Format("{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);

            this.AppPkgsStr.Text = App.Repository.Packages.Count.ToString();
            this.AppDepsStr.Text = "0"; // App.Repository.Packages.Count.ToString();
            this.AppResCntStr.Text = App.Repository.Repositories.Count.ToString();
            this.ReposList.ItemsSource = App.Repository.Repositories;
        }
    }
}
