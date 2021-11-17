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
    public class RepositoryController
    {
        public RepositoryController() {
            this.Settings = new SettingsController();
        }

        public async Task Initialize() {
            await this.Settings.Initialize();

            this.Repositories = this.Settings.Config.Repositories; 

            // Fetch packages
            for (int n = 0; n < this.Repositories.Count; n++) {
                var repo = this.Repositories[n];

                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(new Uri(repo.URL));
                JArray packages = JArray.Parse(await response.Content.ReadAsStringAsync());

                for (int i = 0; i < packages.Count; i++) {
                    // TODO: do this properly
                    var obj = JObject.Parse(packages[i].ToString());
                    this.Packages.Add((String)obj["namespace"], new AppModel(obj));
                }
            }

            // Sort packages based on title
            // TODO: do this properly
            this.Packages = this.Packages.OrderBy(i => i.Value.Title).ToDictionary(x => x.Key, x => x.Value);
        }

        public SettingsController Settings { get; }
        public List<RepositoryModel> Repositories { get; set; } // TODO: this should only be get.
        public Dictionary<String, AppModel> Packages { get; set; } = new Dictionary<String, AppModel>();
    }
}
