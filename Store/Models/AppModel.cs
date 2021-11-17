using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Store.Models {
    internal sealed class AppAuthor {
        public AppAuthor(String name, String role = "Contributor") {
            this.Name = name;
            this.Role = role;
        }

        public String Name { get; }
        public String Role { get; }
    }

    public sealed class AppDependency {
        public AppDependency(String title, String ns, Version version) {
            this.Title = title;
            this.Namespace = ns;
            this.Version = version;
        }

        public String Title { get; }
        public String Namespace { get; }
        public Version Version { get; }
    }

    internal sealed class AppModel {
        public AppModel(JObject data) {
            this.Id = (String)data["id"];
            // TODO: string can't be empty... sanity checks
            this.Version = new Version((String)data["version"] ?? String.Empty);

            if (data.ContainsKey("date")) {
                try {
                    this.Timestamp = DateTime.Parse((String)data["date"]);
                } catch { }
            }

            this.Title = (String)data["title"];
            this.Author = (String)data["author"];
            this.Description = (String)data["description"];
            this.LogoUrl = (String)data["logo_url"];
            this.Size = (Double)data["size"];
            this.Contributors = data["contributors"].ToObject<List<AppAuthor>>();
            this.Dependencies = data["dependencies"].ToObject<List<String>>();
        }

        public String Id { get; }
        public Version Version { get; }
        public DateTime? Timestamp { get; }
        public String Title { get; }
        public String Author { get; }
        public List<AppAuthor> Contributors { get; }
        public String Description { get; }
        public String LogoUrl { get; }
        public Double Size { get; }
        public List<String> Dependencies { get; }
    }
}
