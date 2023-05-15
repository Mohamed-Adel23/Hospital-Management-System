using HMSproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Dynamic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace HMSproject.Controllers
{
    public class DoctorController : Controller
    {
        // Make a refrence from HmsContext
        private readonly HmsContext _db;

        //Make a constructor to connect with HmsContext
        public DoctorController(HmsContext db)
        {
            _db = db; // have Full Access To DB 
        }

        public IActionResult Index()
        {
            IEnumerable<Doctor> drData = _db.Doctors.Include(dept => dept.FkDeptNavigation).ToList();
            return View(drData);
        }

        // -- Create --
        // GET
        [HttpGet]
        public IActionResult Create()
        {
            var depts = _db.Departments.FromSql($"SELECT * FROM departments").ToList();
            Dictionary<int, string> data = new Dictionary<int, string>();
            foreach (var dept in depts)
            {
                data.Add(dept.Id, dept.Name);
            }
            ViewBag.Depts = data;
            return View();
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Doctor obj, IFormFile drImage)
        {
            // Check Image
            if(drImage == null)
				ModelState.AddModelError("Image", "Please, Select an Image!");

            // Hashing The Password
            var passwordHasher = new PasswordHasher<string>();
            string password = obj.Password;
            string hashedPassword = passwordHasher.HashPassword(null, password);
            // Check Password
            var pass = _db.Doctors.FromSql($"SELECT * FROM doctors WHERE password = {hashedPassword}");
			foreach (var p in pass)
			{
				if (p.Password == null)
					break;
				else
				{
					ModelState.AddModelError("Password", "This Password is already exist!!");
					break;
				}
			}
			// Check Email
			var em = _db.Doctors.FromSql($"SELECT * FROM doctors WHERE email = {obj.Email}");
			foreach (var p in em)
			{
				if (p.Email == null)
					break;
				else
				{
					ModelState.AddModelError("Email", "This Email is already exist!!");
					break;
				}
			}
			// Check Phone
			var ph = _db.Doctors.FromSql($"SELECT * FROM doctors WHERE phone = {obj.Phone}");
			foreach (var p in ph)
			{
				if (p.Phone == null)
					break;
				else
				{
					ModelState.AddModelError("Phone", "This Phone is already exist!!");
					break;
				}
			}

            if (ModelState.IsValid)
            {
                _db.Doctors.Add(obj);
                _db.SaveChanges();

                // Update DataBase
                _db.Database.ExecuteSqlInterpolated($"UPDATE doctors SET password = {hashedPassword} WHERE id = {obj.Id}");
                _db.SaveChanges();

                // Upload The Image
                try
                {
                    // Upload The Image To wwwroot Folder
                    string imageName = drImage.FileName;
                    imageName = $"{obj.Id}-" + Path.GetFileName(imageName);
                    string uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/dr", imageName);
                    var stream = new FileStream(uploadFilePath, FileMode.Create);
                    drImage.CopyToAsync(stream);
                    // Using SQL Queries To Update The Image 
                    using (var trans = _db.Database.BeginTransaction())
                    {
                        _db.Database.ExecuteSqlInterpolated($"UPDATE doctors SET image = {imageName} WHERE id = {obj.Id}");
                        _db.SaveChanges();
                        trans.Commit();
                    }
                }
                catch (Exception e)
                {
                    TempData["error"] = "Oops, Errors Occured!";
                    return View(obj);
                }

                TempData["success"] = "Doctor Created Successfully";

                return RedirectToAction("Index");
            }
            // If there are unknown errors 
            var depts = _db.Departments.FromSql($"SELECT * FROM departments").ToList();
            Dictionary<int, string> data = new Dictionary<int, string>();
            foreach (var dept in depts)
            {
                data.Add(dept.Id, dept.Name);
            }
            ViewBag.Depts = data;

			TempData["error"] = "Oops, Errors Occured!";
			return View(obj);
        }

        // -- Update --
        // GET
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var obj = _db.Doctors.Find(id);
            if (obj == null)
                return NotFound();

            var depts = _db.Departments.FromSql($"SELECT * FROM departments").ToList();
            Dictionary<int, string> data = new Dictionary<int, string>();
            foreach (var dept in depts)
            {
                data.Add(dept.Id, dept.Name);
            }
            ViewBag.Depts = data;

            return View(obj);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Doctor obj)
        {
			// Check Email
			var em = _db.Doctors.FromSql($"SELECT * FROM doctors WHERE email = {obj.Email} AND id != {obj.Id}");
			foreach (var p in em)
			{
				if (p.Email == null)
					break;
				else
				{
					ModelState.AddModelError("Email", "This Email is already exist!!");
					break;
				}
			}
			// Check Phone
			var ph = _db.Doctors.FromSql($"SELECT * FROM doctors WHERE phone = {obj.Phone} AND id != {obj.Id}");
			foreach (var p in ph)
			{
				if (p.Phone == null)
					break;
				else
				{
					ModelState.AddModelError("Phone", "This Phone is already exist!!");
					break;
				}
			}

			if (ModelState.IsValid)
            {
                // Update Changed Date
                _db.Doctors.Update(obj);
                _db.SaveChanges();
				TempData["success"] = "Doctor Updated Successfully";
				return RedirectToAction("Index");
            }

			// If there are unknown errors 
			var depts = _db.Departments.FromSql($"SELECT * FROM departments").ToList();
			Dictionary<int, string> data = new Dictionary<int, string>();
			foreach (var dept in depts)
			{
				data.Add(dept.Id, dept.Name);
			}
			ViewBag.Depts = data;

			TempData["error"] = "Oops, Errors Occured!";
			return View(obj);
        }

        // -- Update Password --
        // GET
        public IActionResult EditPass(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var obj = _db.Doctors.Find(id);
            if (obj == null)
                return NotFound();

            var depts = _db.Departments.FromSql($"SELECT * FROM departments").ToList();
            Dictionary<int, string> data = new Dictionary<int, string>();
            foreach (var dept in depts)
            {
                data.Add(dept.Id, dept.Name);
            }
            ViewBag.Depts = data;

            return View(obj);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditPass(Doctor obj)
        {
            // Hashing The Password
            var passwordHasher = new PasswordHasher<string>();
            string password = obj.Password;
            string hashedPassword = passwordHasher.HashPassword(null, password);
            
            // Check Password
            var pass = _db.Doctors.FromSql($"SELECT * FROM doctors WHERE password = {hashedPassword} AND id != {obj.Id}");
			foreach (var p in pass)
			{
				if (p.Password == null)
					break;
				else
				{
					ModelState.AddModelError("Password", "This Password is already exist!!");
					break;
				}
			}

			if (ModelState.IsValid)
            {
                _db.Doctors.Update(obj);
                _db.SaveChanges();

                // Update Hashing Password
                _db.Database.ExecuteSqlInterpolated($"UPDATE doctors SET password = {hashedPassword} WHERE id = {obj.Id}");
                _db.SaveChanges();

                TempData["success"] = "Password Updated Successfully";
                return RedirectToAction("Index");
            }

			// If there are unknown errors 
			var depts = _db.Departments.FromSql($"SELECT * FROM departments").ToList();
			Dictionary<int, string> data = new Dictionary<int, string>();
			foreach (var dept in depts)
			{
				data.Add(dept.Id, dept.Name);
			}
			ViewBag.Depts = data;

			TempData["error"] = "Oops, Errors Occured!";
			return View(obj);
        }

        // -- Update Image --
        // GET
        public IActionResult EditImage(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var obj = _db.Doctors.Find(id);
            if (obj == null)
                return NotFound();

            var depts = _db.Departments.FromSql($"SELECT * FROM departments").ToList();
            Dictionary<int, string> data = new Dictionary<int, string>();
            foreach (var dept in depts)
            {
                data.Add(dept.Id, dept.Name);
            }
            ViewBag.Depts = data;

            return View(obj);
        }
        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditImage(Doctor obj, IFormFile drImage)
        {
			// Check Image
			if (drImage == null)
				ModelState.AddModelError("Image", "Please, Select an Image!");

			if (ModelState.IsValid)
            {
                _db.Doctors.Update(obj);
                _db.SaveChanges();

                // Update The Image if the user choose to change his image
                try
                {
                    // Upload The Image To wwwroot Folder
                    string imageName = drImage.FileName;
                    imageName = $"{obj.Id}-" + Path.GetFileName(imageName);
                    string uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/dr", imageName);
                    var stream = new FileStream(uploadFilePath, FileMode.Create);
                    drImage.CopyToAsync(stream);
                    // Using SQL Queries To Update The Image 
                    using (var trans = _db.Database.BeginTransaction())
                    {
                        _db.Database.ExecuteSqlInterpolated($"UPDATE doctors SET image = {imageName} WHERE id = {obj.Id}");
                        _db.SaveChanges();
                        trans.Commit();
                    }
                }
                catch (Exception e)
                {

                }

                TempData["success"] = "Image Updated Successfully";
                return RedirectToAction("Index");
            }

			// If there are unknown errors 
			var depts = _db.Departments.FromSql($"SELECT * FROM departments").ToList();
			Dictionary<int, string> data = new Dictionary<int, string>();
			foreach (var dept in depts)
			{
				data.Add(dept.Id, dept.Name);
			}
			ViewBag.Depts = data;

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

            var obj = _db.Doctors.Find(id);
            if (obj == null)
                return NotFound();

            _db.Doctors.Remove(obj);
            _db.SaveChanges();

            TempData["success"] = "Doctor Deleted Successfully";
            return RedirectToAction("Index");
        }

        // Show our Doctors to Public 
        public IActionResult Doctors()
        {
            IEnumerable<Doctor> drData = _db.Doctors.Include(dept => dept.FkDeptNavigation).ToList();
            return View(drData);
        }

        // Show Doctor Profile
        public IActionResult SingleDoctor(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var obj = _db.Doctors.Find(id);
            if (obj == null)
                return NotFound();

            var drData = _db.Doctors.FromSql($"SELECT * FROM doctors WHERE id = {id}");
            drData.ToList();
            ViewBag.Doctor = drData;
            foreach (var d in drData)
            {
                ViewBag.DrName = d.Name;
                break;
            }

            foreach(var dr in drData)
            {
                var deptData = _db.Departments.FromSql($"SELECT * FROM departments WHERE id = {dr.FkDept}");
                foreach(var dept in deptData)
                {
                    ViewBag.DeptName = dept.Name;
                    break;
                }
                break;
            }
            return View();
        }
    }
}
