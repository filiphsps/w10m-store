using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Store.Models {
    internal sealed class RepositoryModel {
        public RepositoryModel(String url) {
            this.Url = url;

            // Make sure we end with a "/"
            if (!this.Url.EndsWith("/")) this.Url = String.Concat(this.Url, '/');
        }

        public async Task Initialize() {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(new Uri(String.Concat(this.Url, "index.json")));
            var info = JObject.Parse(await response.Content.ReadAsStringAsync());

            this.Name = (String)info["name"];
            this.Description = (String)info["description"];
            this.Author = (String)info["author"];

            // TODO: paths
        }

        public String Name { get; private set; }
        public String Description { get; private set; }
        public String Author { get; private set; }
        public String Url { get; }
    }
}
