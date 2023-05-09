using HMSproject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HMSproject.Controllers
{
    public class NurseController : Controller
    {
        // Make a refrence from HmsContext
        private readonly HmsContext _db;

        //Make a constructor to connect with HmsContext
        public NurseController(HmsContext db)
        {
            _db = db; // have Full Access To DB 
        }

        public IActionResult Index()
        {
            IEnumerable<Nurse> nurseData = _db.Nurses.Include(dept => dept.FkDeptNavigation).ToList();
            return View(nurseData);
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
        public IActionResult Create(Nurse obj, IFormFile nsImage)
        {
            // Check Image
            if (nsImage == null)
                ModelState.AddModelError("Image", "Please, Select an Image!");

            // Hashing The Password
            var passwordHasher = new PasswordHasher<string>();
            string password = obj.Password;
            string hashedPassword = passwordHasher.HashPassword(null, password);
            // Check Password
            var pass = _db.Nurses.FromSql($"SELECT * FROM nurses WHERE password = {hashedPassword}");
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
            var em = _db.Nurses.FromSql($"SELECT * FROM nurses WHERE email = {obj.Email}");
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
            var ph = _db.Nurses.FromSql($"SELECT * FROM nurses WHERE phone = {obj.Phone}");
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
                _db.Nurses.Add(obj);
                _db.SaveChanges();

                // Update DataBase
                _db.Database.ExecuteSqlInterpolated($"UPDATE nurses SET password = {hashedPassword} WHERE id = {obj.Id}");
                _db.SaveChanges();

                // Upload The Image
                try
                {
                    // Upload The Image To wwwroot Folder
                    string imageName = nsImage.FileName;
                    imageName = $"{obj.Id}-" + Path.GetFileName(imageName);
                    string uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ns", imageName);
                    var stream = new FileStream(uploadFilePath, FileMode.Create);
                    nsImage.CopyToAsync(stream);
                    // Using SQL Queries To Update The Image 
                    using (var trans = _db.Database.BeginTransaction())
                    {
                        _db.Database.ExecuteSqlInterpolated($"UPDATE nurses SET image = {imageName} WHERE id = {obj.Id}");
                        _db.SaveChanges();
                        trans.Commit();
                    }
                }
                catch (Exception e)
                {

                }
                TempData["success"] = "Nurse Created Successfully";
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
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var obj = _db.Nurses.Find(id);
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
        public IActionResult Edit(Nurse obj)
        {
            // Check Email
            var em = _db.Nurses.FromSql($"SELECT * FROM nurses WHERE email = {obj.Email} AND id != {obj.Id}");
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
            var ph = _db.Nurses.FromSql($"SELECT * FROM nurses WHERE phone = {obj.Phone} AND id != {obj.Id}");
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
                _db.Nurses.Update(obj);
                _db.SaveChanges();

                TempData["success"] = "Nurse Updated Successfully";
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
        [HttpGet]
        public IActionResult EditPass(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();

            var obj = _db.Nurses.Find(id);
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
        public IActionResult EditPass(Nurse obj)
        {
            // Hashing The Password
            var passwordHasher = new PasswordHasher<string>();
            string password = obj.Password;
            string hashedPassword = passwordHasher.HashPassword(null, password);

            // Check Password
            var pass = _db.Nurses.FromSql($"SELECT * FROM nurses WHERE password = {hashedPassword} AND id != {obj.Id}");
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
                _db.Nurses.Update(obj);
                _db.SaveChanges();

                // Update Hashing Password
                _db.Database.ExecuteSqlInterpolated($"UPDATE nurses SET password = {hashedPassword} WHERE id = {obj.Id}");
                _db.SaveChanges();

                TempData["success"] = "Password Updated Successfully";
                return RedirectToAction("Index");
            }

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

            var obj = _db.Nurses.Find(id);
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
        public IActionResult EditImage(Nurse obj, IFormFile nsImage)
        {
            // Check Image
            if (nsImage == null)
                ModelState.AddModelError("Image", "Please, Select an Image!");


            if (ModelState.IsValid)
            {
                _db.Nurses.Update(obj);
                _db.SaveChanges();

                // Update The Image if the user choose to change his image
                try
                {
                    // Upload The Image To wwwroot Folder
                    string imageName = nsImage.FileName;
                    imageName = $"{obj.Id}-" + Path.GetFileName(imageName);
                    string uploadFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/ns", imageName);
                    var stream = new FileStream(uploadFilePath, FileMode.Create);
                    nsImage.CopyToAsync(stream);
                    // Using SQL Queries To Update The Image 
                    using (var trans = _db.Database.BeginTransaction())
                    {
                        _db.Database.ExecuteSqlInterpolated($"UPDATE nurses SET image = {imageName} WHERE id = {obj.Id}");
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

            var obj = _db.Nurses.Find(id);
            if (obj == null)
                return NotFound();

            _db.Nurses.Remove(obj);
            _db.SaveChanges();

            TempData["success"] = "Nurse Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}
