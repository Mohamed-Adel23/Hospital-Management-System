using HMSproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMSproject.Controllers
{
    public class HomeAdminController : Controller
    {
		// Make a refrence from HmsContext
		private readonly HmsContext _db;

		//Make a constructor to connect with HmsContext
		public HomeAdminController(HmsContext db)
		{
			_db = db; // have Full Access To DB 
		}

		public IActionResult Index()
        {
            var dr = _db.Doctors.FromSql($"SELECT * FROM doctors");
            var dp = _db.Departments.FromSql($"SELECT * FROM departments");

            Dictionary<int, string> names = new Dictionary<int, string>();
            foreach (var d in dr)
            {
                if (d.FkDept == null) continue;
                foreach (var d2 in dp)
                {
                    if (d2.Id == d.FkDept)
                    {
                        if (!names.ContainsKey(d2.Id))
                        {
                            names.Add(d2.Id, d.Name);

                            break;
                        }
                    }
                }
            }
            ViewBag.Names = names;
            var appData = _db.Appointments.Include(pat => pat.Patient).Include(dept => dept.Department).ToList();
            ViewBag.App = appData;

            var drData = _db.Doctors.Include(dept => dept.FkDeptNavigation).ToList();
            ViewBag.DrData = drData;

            return View();
        }

        public IActionResult ShowApp()
        {
            //var drDept = _db.Doctors.Join(
            //    _db.Departments,
            //    dr => dr.Id, dp => dp.Id,
            //    (dr, dp) => new
            //    {
            //        id = dp.Id,
            //        name = dr.Name
            //    });
            var dr = _db.Doctors.FromSql($"SELECT * FROM Doctors");
            var dp = _db.Departments.FromSql($"SELECT * FROM Departments");

            Dictionary<int, string> names = new Dictionary<int, string>();
            foreach ( var d in dr )
            {
                foreach ( var d2 in dp )
                {
                    if(d2.Id == d.FkDept)
                    {
                        if (!names.ContainsKey(d2.Id))
                        {
                            names.Add(d2.Id, d.Name);
                            break;
                        }
                    }
                }
            }
            ViewBag.Names = names;

            IEnumerable<Appointment> appData = _db.Appointments.Include(pat => pat.Patient).Include(dept => dept.Department).ToList();
            return View(appData);
        }
    }
}