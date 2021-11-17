
using System.Threading.Tasks;
using Store.Models;

namespace Store.Controllers {
    internal sealed class InstallController {
        public async Task Initialize() {
            
        }

        public async Task Install(AppModel app) {
            await Task.Delay(1500);
        }
    }
}
