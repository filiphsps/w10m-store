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
        // TODO: Load repository list from disk.
        public async Task Initialize() {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri("https://raw.githubusercontent.com/w10m-research/StoreRepository/master/packages.json"));
            JArray packages = JArray.Parse(await response.Content.ReadAsStringAsync());

            // TODO: do this properly
            for (int i = 0; i < packages.Count; i++) {
                this.packages.Add(new AppModel(JObject.Parse(packages[i].ToString())));
            }
        }

        public List<AppModel> packages { get; } = new List<AppModel>();
    }
}
