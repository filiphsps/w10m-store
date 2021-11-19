using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreManager.Models {
    public class RepositoryModel {
        public enum RepositoryStatus {
            UnLoaded,
            Loaded,
            Error
        }

        public RepositoryModel(String url) {
            this.Url = url;

            // Make sure we end with a "/"
            if (!this.Url.EndsWith("/")) this.Url = String.Concat(this.Url, '/');
        }

        public async Task Initialize() {
            try {
                var client = new HttpClient {
                    Timeout = TimeSpan.FromSeconds(15)
                };

                // TODO: handle without index.json too
                HttpResponseMessage response = await client.GetAsync(new Uri(String.Concat(this.Url, "index.json")));
                var info = JObject.Parse(await response.Content.ReadAsStringAsync());

                this.Name = (String)info["name"];
                this.Description = (String)info["description"];
                this.Author = (String)info["author"];

                this.Status = RepositoryStatus.Loaded;
            } catch (Exception ex) {
                Debug.WriteLine(ex);
                this.Status = RepositoryStatus.Error;
                // TODO: log error somewhere
            }

            // TODO: paths
        }

        public RepositoryStatus Status { get; private set; } = RepositoryStatus.UnLoaded;
        public String Name { get; private set; }
        public String Description { get; private set; }
        public String Author { get; private set; }
        public String Url { get; }
    }
}
