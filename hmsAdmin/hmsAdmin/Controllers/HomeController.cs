using hmsAdmin.Data;
using hmsAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace hmsAdmin.Controllers
{
    public class HomeController : Controller
    {
		// Make a refrence from HmsContext
		private readonly HmsContext _db;

		//Make a constructor to connect with HmsContext
		public HomeController(HmsContext db)
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
                if(d.FkDept == null) continue;
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
            var appData = _db.Appointments.Include(pat => pat.FkPatNavigation).Include(dept => dept.FkDeptNavigation).ToList();
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
            var dr = _db.Doctors.FromSql($"SELECT * FROM doctors");
            var dp = _db.Departments.FromSql($"SELECT * FROM departments");

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

            IEnumerable<Appointment> appData = _db.Appointments.Include(pat => pat.FkPatNavigation).Include(dept => dept.FkDeptNavigation).ToList();
            return View(appData);
        }
    }
}