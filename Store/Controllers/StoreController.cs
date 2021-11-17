using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Store.Models;

namespace Store.Controllers
{
    public class StoreController
    {
        public StoreController() {
            this.Settings = new SettingsController();
            this.Downloads = new DownloadController();
        }

        private async Task updatePackages() {
            this.Packages.Clear();

            for (Int32 n = 0; n < this.Repositories.Count; n++) {
                var repo = this.Repositories[n];

                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(new Uri(String.Concat(repo.URL, "packages.json")));
                var packages = JArray.Parse(await response.Content.ReadAsStringAsync());

                for (Int32 i = 0; i < packages.Count; i++) {
                    // TODO: do this properly
                    var obj = JObject.Parse(packages[i].ToString());
                    this.Packages.Add((String)obj["id"], new AppModel(obj));
                }
            }
        }

        private async Task updateDependencies() {
            this.Dependencies.Clear();

            for (Int32 n = 0; n < this.Repositories.Count; n++) {
                var repo = this.Repositories[n];

                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(new Uri(String.Concat(repo.URL, "dependencies.json")));
                var dependencies = JArray.Parse(await response.Content.ReadAsStringAsync());

                for (Int32 i = 0; i < dependencies.Count; i++) {
                    // TODO: do this properly
                    var obj = JObject.Parse(dependencies[i].ToString());
                    this.Dependencies.Add((String)obj["id"], obj.ToObject<AppDependency>());
                }
            }
        }

        public async Task Initialize() {
            await this.Settings.Initialize();

            this.Repositories = this.Settings.Config.Repositories;

            // Fetch packages.
            await this.updatePackages();

            // TODO: Fetch dependencies.
            await this.updateDependencies();

            // Sort packages based on title.
            // TODO: do this properly.
            this.Packages = this.Packages.OrderBy(i => i.Value.Title).ToDictionary(x => x.Key, x => x.Value);
        }

        public SettingsController Settings { get; }
        public DownloadController Downloads { get; }
        public List<RepositoryModel> Repositories { get; set; } // TODO: this should only be get.
        public Dictionary<String, AppModel> Packages { get; set; } = new Dictionary<String, AppModel>();
        public Dictionary<String, AppDependency> Dependencies { get; set; } = new Dictionary<String, AppDependency>();
    }
}
