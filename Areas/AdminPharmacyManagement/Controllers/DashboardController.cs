using Microsoft.AspNetCore.Mvc;

namespace PharmacyManagement.Areas.AdminPharmacyManagement.Controllers
{
    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
