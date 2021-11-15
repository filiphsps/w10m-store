using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class RepositoryModel
    {
        public RepositoryModel(String url) {
            this.URL = url;
        }
        public String URL { get; }
    }
}
