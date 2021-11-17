using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Store.Models;

namespace Store.Controllers {
    internal sealed class StoreController {
        public StoreController() {
            this.Settings = new SettingsController();
            this.Downloader = new DownloadController();
            this.Installer = new InstallController();
        }

        private async Task UpdatePackages() {
            this.Packages.Clear();

            foreach (RepositoryModel repo in this.Repositories) {
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(new Uri(String.Concat(repo.Url, "packages.json")));
                var packages = JArray.Parse(await response.Content.ReadAsStringAsync());
                foreach (JObject obj in packages.Select(package => JObject.Parse(package.ToString()))) {
                    // TODO: validate ID, can't be null
                    this.Packages.Add((String)obj["id"] ?? String.Empty, new AppModel(obj));
                }
            }
        }

        private async Task UpdateDependencies() {
            this.Dependencies.Clear();

            foreach (RepositoryModel repo in this.Repositories) {
                var client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync(new Uri(String.Concat(repo.Url, "dependencies.json")));
                var dependencies = JArray.Parse(await response.Content.ReadAsStringAsync());
                foreach (JObject obj in dependencies.Select(dependency => JObject.Parse(dependency.ToString()))) {
                    // TODO: validate ID, can't be null
                    this.Dependencies.Add((String)obj["id"] ?? String.Empty, obj.ToObject<AppDependency>());
                }
            }
        }

        public async Task Initialize() {
            await this.Settings.Initialize();
            await this.Downloader.Initialize();
            await this.Installer.Initialize();

            this.Repositories = this.Settings.Config.Repositories;

            // Fetch packages.
            await this.UpdatePackages();

            // TODO: Fetch dependencies.
            await this.UpdateDependencies();

            // Sort packages based on title.
            // TODO: do this properly. Perhaps even in the view(s)?
            this.Packages = this.Packages.OrderBy(i => i.Value.Title).ToDictionary(x => x.Key, x => x.Value);
        }

        public SettingsController Settings { get; }
        public DownloadController Downloader { get; }
        public InstallController Installer { get; }
        public List<RepositoryModel> Repositories { get; set; } // TODO: this should only be get.
        public Dictionary<String, AppModel> Packages { get; set; } = new Dictionary<String, AppModel>();
        public Dictionary<String, AppDependency> Dependencies { get; set; } = new Dictionary<String, AppDependency>();
    }
}
