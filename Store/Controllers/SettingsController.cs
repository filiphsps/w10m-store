using System.Threading.Tasks;

namespace Store.Controllers {
    internal sealed class SettingsController {
        internal async Task Initialize() {
            /*
            TODO: implementation cache loading
            var appData = Windows.Storage.ApplicationData.Current.LocalFolder;
            try
            {
                var configFile = await appData.GetFileAsync("config.json");
                this.Config = JsonConvert.DeserializeObject<Models.ConfigModel>(await Windows.Storage.FileIO.ReadTextAsync(configFile));
            }
            catch (FileNotFoundException) {
                var configFile = await appData.CreateFileAsync("config.json"); */
                this.Config = new Models.ConfigModel();
                /*await Windows.Storage.FileIO.WriteTextAsync(configFile, JsonConvert.SerializeObject(this.Config));
            }*/
        }

        internal async Task Save() {
            // TODO: implementation cache saving
            // var appData = Windows.Storage.ApplicationData.Current.LocalFolder;
            // var configFile = await appData.GetFileAsync("config.json");
            // await Windows.Storage.FileIO.WriteTextAsync(configFile, JsonConvert.SerializeObject(this.Config));
        }

        internal Models.ConfigModel Config { get; private set; }
    }
}
