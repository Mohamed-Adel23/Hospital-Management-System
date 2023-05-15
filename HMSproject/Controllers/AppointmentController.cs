using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HMSproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace HMSproject.Controllers
{
    [Authorize]
    public class AppointmentController : Controller
    {
        private readonly HmsContext _context;
        private readonly UserManager<Patient> _userManager;
        private readonly IEmailSender _emailSender;

        public AppointmentController(HmsContext context,UserManager<Patient>userManager,IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
        }
        
        public async Task<IActionResult> Confirm()
        {
            var user = await _userManager.GetUserAsync(User);
            _emailSender.SendEmailAsync(user.Email, "Confirming Your Appointment", $"Thank you for making your appointment.<br/>" +
                $" Please make sure that you should come \"{TempData["Day"]}\" To the department of \"{TempData["Department"]}\" <br/>" +
                $"The Price of the appointment will be \"{TempData["Price"]}$\" if you chose to pay cash.<br/>Timing schedule.<br/><strong>Working Hours</strong><br/>" +
                $"<strong>Sun - Wed :&nbsp&nbsp&nbsp8:00 - 17:00<br/>Thu - Fri :&nbsp&nbsp&nbsp9:00 - 17:00<br/>Sat - sun :&nbsp&nbsp&nbsp10:00 - 17:00</strong>");
            return View();
        }
        
        // GET: Appointment
        public async Task<IActionResult> Index()
        {
              return _context.Appointments != null ? 
                          View(await _context.Appointments
                              .Include(p=>p.Patient)
                              .Include(d=>d.Department)
                              .ToListAsync()) :
                          Problem("Entity set 'HmsContext.Appointments'  is null.");
        }

        
        private string getDay(string id)
        {
            if (id == "1") return "Next Saturday";
            if (id == "2") return "Next Sunday";
            if (id == "3") return "Next Monday";
            if (id == "4") return "Next Tuesday";
            if (id == "5") return "Next Wednesday";
            if (id == "6") return "Next Thursday";
            return "Next Friday";
        }
        public void create_days_status(int selected=0)
        {
            List<Day> days = new List<Day>() 
                {   new Day(){id=1,name= "Next Saturday" },
                    new Day(){id=2,name= "Next Sunday" },
                    new Day(){id=3,name= "Next Monday" },
                    new Day(){id=4,name= "Next Tuesday"},
                    new Day(){id=5,name= "Next Wednesday"},
                    new Day(){id=6,name= "Next Thursday"},
                    new Day(){id=7,name= "Next Friday"}
                };
            SelectList MyListDay = new SelectList(days,"id","name",selected);
            List<PaymentMethod> paymentMethods = new List<PaymentMethod>()
            {
                new PaymentMethod() { id = 0, name = "Cash" },
                new PaymentMethod() { id = 1, name = "Online" }
            };
            SelectList MyListPay = new SelectList(paymentMethods,"id","name",selected);
            ViewBag.dayList = MyListDay;
            ViewBag.payList = MyListPay;
        }
        
        // GET: Appointment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(p=>p.Patient)
                .Include(d=>d.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointment/Create
        public IActionResult Create()
        {
            create_days_status();
            List<Department> departments =_context.Departments.ToList();
            SelectList MyList = new SelectList(departments, "Id", "Name",1);
            ViewBag.deptList = MyList;
            return View();
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientID,DepartmentID,Day,Status")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                appointment.PatientID = _userManager.GetUserId(User);
                appointment.Day = getDay(appointment.Day);
                // appointment.Id = _context.Appointments.Max(i => i.Id) + 1;
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                TempData["Day"] = appointment.Day;
                TempData["Department"] = _context.Departments.Find(appointment.DepartmentID).Name;
                TempData["Price"] = _context.Departments.Find(appointment.DepartmentID).app_price;
                return RedirectToAction("Confirm");
            }
            return View(appointment);
        }

        // GET: Appointment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            create_days_status();
            List<Department> departments =_context.Departments.ToList();
            SelectList MyList = new SelectList(departments, "Id", "Name",1);
            ViewBag.deptList = MyList;

            return View(appointment);
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientID,DepartmentID,Day,Status")] Appointment appointment)
        {
            if (id != appointment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    appointment.PatientID = _userManager.GetUserId(User);
                    appointment.Day = getDay(appointment.Day);
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(appointment);
        }

        // GET: Appointment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(p=>p.Patient)
                .Include(d=>d.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'HmsContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                var diagnoses = _context.Diagnose.Where(d => d.fk_app == appointment.Id).ToList();
                foreach (var diagnose in diagnoses)
                {
                    _context.Remove(diagnose);
                    await _context.SaveChangesAsync();
                }
                _context.Appointments.Remove(appointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
          return (_context.Appointments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
