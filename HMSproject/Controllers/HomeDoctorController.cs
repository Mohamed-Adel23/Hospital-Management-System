using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using HMSproject.Models;

namespace HMSproject.Controllers
{
    public class HomeDoctorController : Controller
    {
	    public HomeDoctorController(HmsContext db)
	    {
		    _db = db;
	    }

	    private readonly HmsContext _db;

		/*------------------------------------------------------------------------------*/



		public IActionResult Index()
        {
            return View();
        }

		/*------------------------------------------------------------------------------*/
		public IActionResult Doctor()
		{
			return View();
		}

		public IActionResult Close()
		{
			var patients = _db.AspNetUsers.ToList();
			foreach(var patient in patients) 
			{
				patient.Condition = 0;
				_db.Update(patient);
			}

			_db.SaveChanges();

			return RedirectToAction("Index","Home");
		}


		

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel{ RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

	
}