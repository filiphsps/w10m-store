using StoreManager.Models;
using System.Threading.Tasks;

namespace StoreManager.Controllers {
    public class InstallController {
        public async Task Initialize() {
            
        }

        public async Task Install(AppModel app) {
            await Task.Delay(1500);
        }
    }
}
