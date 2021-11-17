using System;

namespace Store.Models {
    internal sealed class RepositoryModel {
        public RepositoryModel(String title, String url) {
            this.Title = title;
            this.Url = url;
        }

        public String Title { get; }
        public String Url { get; }
    }
}
