using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Store.Controllers
{
    public class SettingsController
    {
        public async Task Initialize() {
            var appData = Windows.Storage.ApplicationData.Current.LocalFolder;
            /*try
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

        public async Task Save() {
            var appData = Windows.Storage.ApplicationData.Current.LocalFolder;
            var configFile = await appData.GetFileAsync("config.json");
            await Windows.Storage.FileIO.WriteTextAsync(configFile, JsonConvert.SerializeObject(this.Config));
        }

        public Models.ConfigModel Config { get; set; }
    }
}
