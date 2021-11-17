using System.Collections.Generic;

namespace Store.Models {
    internal sealed class ConfigModel {
        internal readonly List<RepositoryModel> Repositories = new List<RepositoryModel>() {
            new RepositoryModel("w10m-research", "https://raw.githubusercontent.com/w10m-research/StoreRepository/master/")
        };
    }
}
