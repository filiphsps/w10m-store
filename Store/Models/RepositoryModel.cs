using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class RepositoryModel
    {
        public RepositoryModel(String title, String url) {
            this.Title = title;
            this.URL = url;
        }
        public String Title { get; }
        public String URL { get; }
}
}
