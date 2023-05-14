using Microsoft.AspNetCore.Mvc;

namespace HMSproject.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Profile()
        {
            return View();
        }
    }
}
