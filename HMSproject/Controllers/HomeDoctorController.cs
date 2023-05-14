using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HMSproject.Models;

namespace HMSproject.Controllers
{
    public class HomeDoctorController : Controller
    {
        




		/*------------------------------------------------------------------------------*/



		public IActionResult Indexx()
        {
            return View();
        }

		/*------------------------------------------------------------------------------*/
		public IActionResult Doctor()
		{
			return View();
		}
		

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel{ RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

	
}