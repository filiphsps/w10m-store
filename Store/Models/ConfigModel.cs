using System.Collections.Generic;

namespace Store.Models {
    internal sealed class ConfigModel {
        internal readonly List<RepositoryModel> Repositories = new List<RepositoryModel>() {
            new RepositoryModel("https://w10m-research.github.io/StoreRepository")
        };
    }
}
