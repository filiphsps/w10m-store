using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using StoreManager.Controllers;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Media.Animation;

namespace Store {
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App {
        private Frame _rootFrame;
        internal static StoreController StoreManager { get; set; } = new StoreController();

        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        internal App() {
            this.InitializeComponent();

            this.Suspending += this.OnSuspending;
            this.UnhandledException += (sender, ex) => {
                ex.Handled = true;
                this._rootFrame.Navigate(typeof(Pages.ErrorPage), ex.Exception, new DrillInNavigationTransitionInfo());
            };
            TaskScheduler.UnobservedTaskException += (sender, ex) => {
                ex.SetObserved();
                this._rootFrame.Navigate(typeof(Pages.ErrorPage), ex.Exception, new DrillInNavigationTransitionInfo());
            };
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e) {
            // TODO: Handle titlebar.
            //CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            //coreTitleBar.ExtendViewIntoTitleBar = true;

            this._rootFrame = (Frame)Window.Current.Content;
            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (this._rootFrame == null) {
                // Create a Frame to act as the navigation context and navigate to the first page
                this._rootFrame = new Frame();

                this._rootFrame.NavigationFailed += this.OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated) {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = this._rootFrame;
            }

            if (e.PrelaunchActivated) return;
            if (this._rootFrame.Content == null) {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                this._rootFrame.Navigate(typeof(MainPage), e.Arguments);
            }

            SystemNavigationManager.GetForCurrentView().BackRequested += this.OnBackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility =
                AppViewBackButtonVisibility.Visible;

            // Ensure the current window is active
            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(Object sender, NavigationFailedEventArgs e) {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(Object sender, SuspendingEventArgs e) {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        protected override async void OnActivated(IActivatedEventArgs args) {
            this._rootFrame = (Frame)Window.Current.Content;
            if (this._rootFrame == null) {
                this._rootFrame = new Frame();
                Window.Current.Content = this._rootFrame;
            }

            if (args.Kind == ActivationKind.Protocol) {
                var eventArgs = args as ProtocolActivatedEventArgs;
                // TODO: error if app doesnt exist
                // TODO: somehow handle outside repo
                await App.StoreManager.Initialize();
                this._rootFrame.Navigate(typeof(Pages.AppPage), App.StoreManager.Packages[eventArgs.Uri.Host]);
            }

            SystemNavigationManager.GetForCurrentView().BackRequested += this.OnBackRequested;
            Window.Current.Activate();
        }

        private void OnBackRequested(Object sender, BackRequestedEventArgs e) {
            var rootFrame = Window.Current.Content as Frame;
            if (!rootFrame.CanGoBack) {
                Application.Current.Exit();
                return;
            }

            rootFrame.GoBack(new EntranceNavigationTransitionInfo());
            e.Handled = true;
        }

        public static BackgroundTaskRegistration RegisterBackgroundTask(
            String taskEntryPoint,
            String name,
            IBackgroundTrigger trigger,
            IBackgroundCondition condition) {

            return null;
        }
    }
}