using Newtonsoft.Json;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;

namespace Store.Controllers {
    internal sealed class SettingsController {
        // TODO: implementation cache

        internal async Task Initialize() {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

            if (!localSettings.Values.ContainsKey("config")) {
                this.Config = new Models.ConfigModel {
                    Repositories = new List<RepositoryModel> {
                        new RepositoryModel("https://w10m-research.github.io/StoreRepository")
                    }
                };
                return;
            }

            this.Config = JsonConvert.DeserializeObject<Models.ConfigModel>((String)localSettings.Values["config"]);
        }

        internal async Task Save() {
            ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["config"] = JsonConvert.SerializeObject(this.Config);
        }

        internal Models.ConfigModel Config { get; private set; }
    }
}
