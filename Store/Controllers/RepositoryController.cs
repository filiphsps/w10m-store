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
        public async Task Initialize() {
            // TODO: Load repository list from disk.
            this.repositories = new List<RepositoryModel>() { new RepositoryModel("Builtin", "https://raw.githubusercontent.com/w10m-research/StoreRepository/master/packages.json") };

            for (int n = 0; n < this.repositories.Count; n++) {
                var repo = this.repositories[n];

                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(new Uri(repo.URL));
                JArray packages = JArray.Parse(await response.Content.ReadAsStringAsync());

                for (int i = 0; i < packages.Count; i++) {
                    // TODO: do this properly
                    var obj = JObject.Parse(packages[i].ToString());
                    this.packages.Add((String)obj["namespace"], new AppModel(obj));
                }
            }
        }

        public List<RepositoryModel> repositories { get; set; } // TODO: this should only be get.
        public Dictionary<String, AppModel> packages { get; } = new Dictionary<String, AppModel>();
    }
}
