using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Store {
    public sealed partial class MainPage : Page {
        public MainPage() {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            this.RegisterBackgroundTask();
        }

        private async void Page_Loaded(Object sender, RoutedEventArgs e) {
            await App.StoreManager.Initialize();
            await App.StoreManager.Settings.Save();

            // Update tile
            PackagesBackgroundTask.PackagesBackgroundTask.UpdateTile(App.StoreManager.Packages.Values.ToList());

            this.Frame.Navigate(typeof(Pages.PackagesPage), null, new DrillInNavigationTransitionInfo());
            this.Frame.BackStack.Remove(this.Frame.BackStack.Last());
        }

        private async void RegisterBackgroundTask() {
            BackgroundAccessStatus backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
            // ReSharper disable once InvertIf
            if (backgroundAccessStatus == BackgroundAccessStatus.AllowedSubjectToSystemPolicy ||
                backgroundAccessStatus == BackgroundAccessStatus.AlwaysAllowed) {
                foreach (KeyValuePair<Guid, IBackgroundTaskRegistration> task in BackgroundTaskRegistration.AllTasks) {
                    if (task.Value.Name == "PackagesBackgroundTask") {
                        task.Value.Unregister(true);
                    }
                }

                // ReSharper disable Once
                var taskBuilder = new BackgroundTaskBuilder {
                    TaskEntryPoint = typeof(PackagesBackgroundTask.PackagesBackgroundTask).FullName,
                    Name = "PackagesBackgroundTask"
                };
                taskBuilder.SetTrigger(new TimeTrigger(15, false));
                BackgroundTaskRegistration registration = taskBuilder.Register();
            }
        }
    }
}
    
