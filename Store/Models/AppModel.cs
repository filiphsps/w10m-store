using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Management.Deployment;

namespace Store.Models
{
    class AppModel
    {
        public AppModel() {
            // TODO
            this.Title = "Store";
            this.Author = "Filiph Sandström";
            this.Description = "The best third-party/alternative store for Windows 10 Mobile.";
            this.LogoUrl = "https://www.shareicon.net/data/2015/07/26/75452_windows_512x512.png";
            this.Version = new Version(1, 0, 0, 0);
            this.Dependencies = new List<String>();
        }

        public Version Version { get; }
        public String Title { get; }
        public String Author { get; }
        public String Description { get; }
        public String LogoUrl { get; }
        public List<String> Dependencies { get; }

        public async void Install() {
            var installer = new PackageManager();
            var options = new DeploymentOptions();

            await installer.AddPackageAsync(new Uri(""), null, options);
        }
    }
}
