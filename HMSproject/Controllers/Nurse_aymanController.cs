

using HMSproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace HMS.Controllers;

public class Nurse_aymanController : Controller
{
    public Nurse_aymanController(HmsContext db)
    {
        _db = db;
    }
        
    private readonly HmsContext _db;
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Nurse_profile()
    {
        return View();
    }

    
    public IActionResult Nurse_search()
    {
        
        IEnumerable<Appointment> patientList = _db.Appointments
            .Include(n=>n.Patient)
            .Include(d=>d.Department)
            .ToList();
        // .Include(a => a.FkPat).ToList()
        return View(patientList);
    }
    
    [HttpPost]
    public IActionResult Nurse_search(int id)
        {
            var update = _db.Appointments
                .Include(n=>n.Patient)
                .Include(d=>d.Department)
                .FirstOrDefault(x => x.Id == id);
            if (update == null) return NotFound();
            update.Status = 1;
            
            var cash = _db.Cash_Flows.FirstOrDefault();
            cash.appointments_cash += 50;
            _db.Cash_Flows.Update(cash);
            _db.Update(update);
            _db.SaveChanges();
    
            return RedirectToAction();
        }
    
    [HttpPost]
    public IActionResult Search(string SSN )
    {
        var patient = _db.AspNetUsers.FirstOrDefault(p => p.SSN == SSN);
        if (patient == null)
        {
            var model = new Appointment()
                { PatientID = "No Result" };
            return View(model);
        }

        var appointment = _db.Appointments
            .Include(n=>n.Patient)
            .Include(d=>d.Department)
            .FirstOrDefault(a => a.PatientID == patient.Id);
        var _model = appointment;
        if (_model == null)
        {
            var model = new Appointment()
                { PatientID = "No Result" };
            return View(model);
        }
        
        var viewModel = new Appointment()
        {
            PatientID = patient.Name,
            Department = appointment.Department ,
            Day = appointment.Day,
            Status = appointment.Status,
            Id = appointment.Id
        };
         

        return View("Search", viewModel );
    }
    
    
    public IActionResult Nurse_Pharmacy()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Nurse_Pharmacy(string SSN )
    {
        if (string.IsNullOrEmpty(SSN)){ return View();}
        var patient = _db.AspNetUsers.FirstOrDefault(p => p.SSN == SSN);
        if (patient == null) {return View();}
       
        var appointments = _db.Appointments.Where(a => a.PatientID == patient.Id).ToList();
        if (appointments == null || appointments.Count==0) {return View();}

        var diagnoses = new List<Diagnose>() { };

        foreach (var app in appointments)
          {
             diagnoses.AddRange(_db.Diagnose.
                  Include(d => d.Appointments).
                  ThenInclude(o => o.Patient).
                  Include(dep => dep.Appointments).
                  ThenInclude(n => n.Department).
                  Where(d => d.fk_app == app.Id).ToList());
          }
          if (diagnoses == null || diagnoses.Count==0) {return View();}
        return View(diagnoses);
    }
    
    [HttpPost]
    public IActionResult Pharmacy_update()
    {

        var cash = _db.Cash_Flows.FirstOrDefault();
        cash.Lab_cash += 19;
        _db.Cash_Flows.Update(cash);
        _db.SaveChanges();
        //TempData["Success"] = "Data updated successfully";

        return RedirectToAction("index");
    }
    
    public IActionResult Nurse_Lab()
    {
   
        return View();
    }
    
    [HttpPost]
    public IActionResult Nurse_Lab(string SSN )
    {
        if (string.IsNullOrEmpty(SSN)) return View();
        var patient = _db.AspNetUsers.FirstOrDefault(p => p.SSN == SSN);
        if (patient == null) {return View();}
        var appointment = _db.Appointments.FirstOrDefault(a => a.PatientID == patient.Id);
        if (appointment == null) {return View();}
        var analysis = _db.Labs.FirstOrDefault(L => L.FkApp == appointment.Id);
        if (analysis == null) {return View();}
        
        var analysis_ = _db.Labs.
            Include(d => d.FkAppNavigation).
            ThenInclude(z => z.Patient).
            Include(dep => dep.FkAppNavigation).
            ThenInclude(n => n.Department).
            FirstOrDefault(d => d.FkApp == appointment.Id);

        return View(analysis_);
    }
    
    [HttpPost]
    public IActionResult Lab_update(int? id, string result, int? SSN)
    {
        var update = _db.Labs.FirstOrDefault(x => x.Id == id);
        var cash = _db.Cash_Flows.FirstOrDefault();
        cash.Lab_cash += 50;
        if (update == null) return NotFound();
        update.Result = result ;
        _db.Labs.Update(update);
        _db.Cash_Flows.Update(cash);
        _db.SaveChanges();
        //TempData["Success"] = "Data updated successfully";
    
        return RedirectToAction("Nurse_Lab", new {SSN = SSN});
    }
    
    public IActionResult Nurse_Cash_Flow()
    {
        var money = _db.Cash_Flows.FirstOrDefault();
        return View(money);
    }
    
    public IActionResult Nurse_Book()
    {
        return View();
    }
    
    public IActionResult Nurse_add_new()
    {
        return View();
    }
    



}