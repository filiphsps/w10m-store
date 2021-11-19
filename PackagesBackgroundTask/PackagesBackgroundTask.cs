using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using StoreManager.Controllers;
using StoreManager.Models;
using System.Linq;

namespace PackagesBackgroundTask {
    public sealed class PackagesBackgroundTask : IBackgroundTask {
        private BackgroundTaskDeferral _deferral;
        public async void Run(IBackgroundTaskInstance taskInstance) {
            // Get a deferral, to prevent the task from closing prematurely
            // while asynchronous code is still running.
            this._deferral = taskInstance.GetDeferral();

            // Download packages
            var store = new StoreController();
            await store.Initialize();

            // Update the live tile with the feed items.
            UpdateTile(store.Packages.Values.ToList());

            // Inform the system that the task is finished.
            this._deferral.Complete();
        }

        public static void UpdateTile(Object packages) {
            // Create tile updater
            TileUpdater updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();

            // Keep track of the number feed items that get tile notifications.

            Int32 itemCount = 0;
            foreach (AppModel item in (List<AppModel>)packages) {
                // TODO: proper xml here, so we can handle all sizes
                XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150SmallImageAndText04);

                tileXml.GetElementsByTagName("text")[0].InnerText = item.Title;
                tileXml.GetElementsByTagName("text")[1].InnerText = item.Description.Replace("\n\n", "\n").Replace("\n", " ");
                ((XmlElement)tileXml.GetElementsByTagName("image")[0]).SetAttribute("src", item.LogoUrl);

                // Create a new tile notification.
                updater.Update(new TileNotification(tileXml));

                // Don't create more than 5 notifications.
                if (itemCount++ > 5) break;
            }

            // Set badge
            {
                // Get the blank badge XML payload for a badge number
                XmlDocument badgeXml =
                    BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeNumber);

                // Set the value of the badge in the XML to our number
                var badgeElement = badgeXml.SelectSingleNode("/badge") as XmlElement;
                badgeElement?.SetAttribute("value", "0"); // TODO: get updateable packages count

                // Create the badge notification
                var badge = new BadgeNotification(badgeXml);

                // Create the badge updater for the application
                BadgeUpdater badgeUpdater =
                    BadgeUpdateManager.CreateBadgeUpdaterForApplication();

                // And update the badge
                badgeUpdater.Update(badge);
            }
        }
    }
}