using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    class AppAuthor
    {
        public AppAuthor(String name, String role = "Contributor") {
            this.Name = name;
            this.Role = role;
        }

        public String Name { get; }
        public String Role { get; }
    }

    class AppDependency
    {
        public AppDependency(String title, String ns, Version version)
        {
            this.Title = title;
            this.Namespace = ns;
            this.Version = version;
        }

        public String Title { get; }
        public String Namespace { get; }
        public Version Version { get; }
    }

    class AppModel
    {
        public AppModel() {
            // TODO
            this.Title = "Store";
            this.Author = "Filiph Sandström";
            this.Contributors = new List<AppAuthor>()
            {
                new AppAuthor("Filiph Sandström", "Lead developer"),
                new AppAuthor("HerryYT", "Secondary developer")
            };
            this.Description = "The best third-party/alternative store for Windows 10 Mobile.";
            this.LogoUrl = "https://www.shareicon.net/data/2015/07/26/75452_windows_512x512.png";
            this.Version = new Version(1, 0, 0, 0);
            this.Dependencies = new List<AppDependency>()
            {
                new AppDependency("DepNr1", "com.dev.depnr1", new Version(2, 41, 0, 3))
            };
            this.Timestamp = new DateTime();
            this.Size = 15.75;
        }

        public Version Version { get; }
        public DateTime Timestamp { get; }
        public String Title { get; }
        public String Author { get; }
        public List<AppAuthor> Contributors { get; }
        public String Description { get; }
        public String LogoUrl { get; }
        public Double Size { get; }
        public List<AppDependency> Dependencies { get; }
    }
}
