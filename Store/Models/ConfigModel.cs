using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store.Models;

namespace Store.Models
{
    public class ConfigModel
    {
        public List<RepositoryModel> Repositories = new List<RepositoryModel>() {
            new RepositoryModel("w10m-research", "https://raw.githubusercontent.com/w10m-research/StoreRepository/master/packages.json")
        };
    }
}
