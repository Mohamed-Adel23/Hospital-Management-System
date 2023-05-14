
using HMSproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace HMSproject.Controllers


{
    public class SearchController : Controller
    {


        public SearchController(HmsContext db)
        {
            _db = db;
        }

        private readonly HmsContext _db;




        /*-------------------------------------------------------------------------*/
        public IActionResult Spatient(string? nid)
        {
            if (nid == null)
            {
                var patients = _db.AspNetUsers
                    .Include(p => p.Appointments)
                    .ToList();

                return View(patients);
            }
            else
            {
                var patient = _db.AspNetUsers
                    .Include(p => p.Appointments)
                    .FirstOrDefault(p => p.SSN == nid);

                if (patient == null)
                {
                    return NotFound("No Patient");
                }

                return View(new List<Patient> { patient });
            }
        }









        /*-------------------------------------------------------------------------*/

        public IActionResult Shictory(string? Id)
        {
            if (Id == null)
            {
                return NotFound("Not Found");
            }


            var patient = _db.AspNetUsers.Find(Id);
            var appointment = _db.Appointments.FirstOrDefault(a => a.PatientID == patient.Id);

      

            var diagnosis = _db.Diagnose.Where(d => d.fk_app == appointment.Id).ToList();

        
           
            return View(diagnosis);
        }








        /*-------------------------------------------------------------------------*/




        //public IActionResult edit(string? Id)
        //{
        //    // Get the current patient
        //    var patient = _db.AspNetUsers.Find(Id);

        //    // Get the current prescription for the patient
        //    var appointment = _db.Appointments.FirstOrDefault(g => g.PatientID==  patient.Id);
        //    var diagnosis = _db.Diagnoses.Where(d => d.FkApp == appointment.Id).ToList();

        //    // Pass the prescription data to the view
        //    return View(diagnosis);
        //}



        ////Post
        //[HttpPost]
        //[ValidateAntiForgeryToken]


        //public IActionResult edit(Diagnose model)
        //{
        //    // Get the diagnosis from the database
        //    var diagnosis = _db.Diagnoses.Find(model.Id);
        //    if (diagnosis != null)
        //    {

        //        // Update the diagnosis with the new data
        //        diagnosis.Prescription = model.Prescription;
        //        diagnosis.Description = model.Description;
        //        diagnosis.Analysis = model.Analysis;
        //        diagnosis.FkApp = model.FkApp;

        //        _db.SaveChanges();

        //        // Redirect to the Hictory page
        //        var appointment = _db.Appointments.FirstOrDefault(a => a.Id == diagnosis.FkApp);
        //        return RedirectToAction("Hictory", new { id = appointment.PatientID });
        //    }
        //    else { return NotFound("Not found"); }

        //}

        public IActionResult Edit(int? id)
        {
            var diagnosis = _db.Diagnose.Find(id);
            if (diagnosis == null)
            {
                return NotFound();
            }

            return View(diagnosis);
        }

        [HttpPost]
        public IActionResult Edit(int id, Diagnose model)
        {
            var diagnosis = _db.Diagnose.Find(id);
            if (diagnosis == null)
            {
                return NotFound();
            }

            diagnosis.Prescription = model.Prescription;
            diagnosis.Description = model.Description;
            diagnosis.Analysis = model.Analysis;

            _db.SaveChanges();
            TempData["sure"] = "The Data has been updated successfully";

            return RedirectToAction("Spatient");
        }



        /*-------------------------------------------------------------------------*/


        public IActionResult Delete(int? Id)
        {
            if (Id == null )
            {
                return NotFound();
            }
            var del = _db.Diagnose.Find(Id);
            if (del == null)
            {
                return NotFound();
            }
            return View(del);
        }



        //Post
        [HttpPost]

        public IActionResult Delete(Diagnose k,string? Id)
        {

            _db.Diagnose.Remove(k);
            _db.SaveChanges();
            TempData["sure"] = "The Data has been delete successfully";
            return RedirectToAction("Spatient");
            
          
        }






    }
}
