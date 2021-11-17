using System.Threading.Tasks;
using Store.Models;

namespace Store.Controllers {
    internal sealed class DownloadController {
        private async Task extractArchive() { }

        public async Task Initialize() {
            // TODO: Create "downloads", "dependencies" & "packages" folders if they don't already exist.
            // TODO: Populate hashmap with the files already downloaded.
        }

        public async Task Download(AppModel app) {
            await Task.Delay(1000);
        }
    }
}
