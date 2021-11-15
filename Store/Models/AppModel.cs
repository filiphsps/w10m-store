using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Store.Models
{
    public class AppAuthor
    {
        public AppAuthor(String name, String role = "Contributor") {
            this.Name = name;
            this.Role = role;
        }

        public String Name { get; }
        public String Role { get; }
    }

    public class AppDependency
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

    public class AppModel
    {
        public AppModel(JObject data) {
            this.Namespace = (String)data["namespace"];
            this.Title = (String)data["title"];
            this.Author = (String)data["author"];
            this.Version = new Version((String)data["version"]);
            this.Description = (String)data["description"];
            this.LogoUrl = (String)data["logo_url"];
            this.Size = (Double)data["size"];
            // TODO: Contributors.
            // TODO: Dependencies.
        }

        public String Namespace { get; }
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
