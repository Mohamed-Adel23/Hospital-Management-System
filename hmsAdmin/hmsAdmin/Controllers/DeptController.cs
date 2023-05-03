using hmsAdmin.Data;
using hmsAdmin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace hmsAdmin.Controllers
{
    public class DeptController : Controller
    {
        // Make a refrence from HmsContext
        private readonly HmsContext _db;

        //Make a constructor to connect with HmsContext
        public DeptController(HmsContext db)
        {
            _db = db; // have Full Access To DB 
        }

        public IActionResult Index()
        {
            int drCnt = 0, nsCnt = 0;
            var depts = _db.Departments.FromSql($"SELECT * FROM departments");
            Dictionary<int, int> dr = new Dictionary<int, int>();
            Dictionary<int, int> ns = new Dictionary<int, int>();
            foreach (var d in depts)
            {
                drCnt = 0;
                nsCnt = 0;
                var drQ = _db.Doctors.FromSql($"SELECT * FROM doctors WHERE fk_dept = {d.Id}");
                foreach(var c in drQ)
                {
                    drCnt++;
                }
                dr.Add(d.Id, drCnt);
                var nsQ = _db.Nurses.FromSql($"SELECT * FROM nurses WHERE fk_dept = {d.Id}");
                foreach (var c in nsQ)
                {
                    nsCnt++;
                }
                ns.Add(d.Id, nsCnt);
            }

            ViewBag.Doc = dr;
            ViewBag.Nur = ns;

            IEnumerable<Department> deptData = _db.Departments.ToList();
            return View(deptData);
        }


        // -- Create --
        // GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Department obj)
        {
            // Check The Name
            var name = _db.Departments.FromSql($"SELECT * FROM departments WHERE name = {obj.Name}");
            foreach (var p in name)
            {
                if (p.Name == null)
                    break;
                else
                {
                    ModelState.AddModelError("Name", "This Name is already exist!!");
                    break;
                }
            }

            if (ModelState.IsValid)
            {
                _db.Departments.Add(obj);
                _db.SaveChanges();

                TempData["success"] = "Department Created Successfully!";
                return RedirectToAction("Index");
            }
            TempData["error"] = "Oops, Errors Occured!";
            return View(obj);
        }

        // -- Update --
        // GET
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var obj = _db.Departments.Find(id);
            if (obj == null)
                return NotFound();

            return View(obj);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department obj)
        {
			// Check The Name
			var name = _db.Departments.FromSql($"SELECT * FROM departments WHERE name = {obj.Name} AND id != {obj.Id}");
			foreach (var p in name)
			{
				if (p.Name == null)
					break;
				else
				{
					ModelState.AddModelError("Name", "This Name is already exist!!");
					break;
				}
			}

			if (ModelState.IsValid)
            {
                _db.Departments.Update(obj);
                _db.SaveChanges();

				TempData["success"] = "Department Updated Successfully!";
				return RedirectToAction("Index");
            }
			TempData["error"] = "Oops, Errors Occured!";
			return View(obj);
        }

        // -- Delete --
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var obj = _db.Departments.Find(id);
            if (obj == null)
                return NotFound();

            // FIRST, Delete All Children of The Depatment 
            var drIds = _db.Doctors.FromSql($"SELECT * FROM doctors WHERE fk_dept = {id}");
            foreach (var dr in drIds)
            {
                _db.Database.ExecuteSqlInterpolated($"DELETE FROM doctors WHERE id = {dr.Id}");
                _db.SaveChanges();
            }
            var nsIds = _db.Nurses.FromSql($"SELECT * FROM nurses WHERE fk_dept = {id}");
            foreach (var ns in nsIds)
            {
                _db.Database.ExecuteSqlInterpolated($"DELETE FROM nurses WHERE id = {ns.Id}");
                _db.SaveChanges();
            }
            var appIds = _db.Appointments.FromSql($"SELECT * FROM appointments WHERE fk_dept = {id}");
            foreach (var app in appIds)
            {
                _db.Database.ExecuteSqlInterpolated($"DELETE FROM appointments WHERE id = {app.Id}");
                _db.SaveChanges();
            }


            // DELETE The Parent
            _db.Database.ExecuteSqlInterpolated($"DELETE FROM departments WHERE id = {id}");
            _db.SaveChanges();

            TempData["success"] = "Department Deleted Successfully!";
			return RedirectToAction("Index");
        }
    }
}

