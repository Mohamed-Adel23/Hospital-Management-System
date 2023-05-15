using HMSproject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace HMSproject.Controllers

{
    public class WorkController : Controller
    {

        public WorkController(HmsContext db)
        {
            _db = db;
        }

        private readonly HmsContext _db;

        /*-----------------------------------------------------------------------------------------*/





        public IActionResult Hictory(string? Id)
        {
            if ( Id == null)
            {
                return NotFound("Not Found Patient");
            }

       
            var patient = _db.AspNetUsers.Find(Id);
            var appointment = _db.Appointments.FirstOrDefault(a => a.PatientID == patient.Id);
            
            var AllDiagnosis = _db.Diagnose.ToList();
            
            var diagnosis=new List<Diagnose>(){};
            foreach (var d in AllDiagnosis)
            {
                if (d.fk_app==appointment.Id)
                {
                    diagnosis.Add(d);
                }
            }
            if (diagnosis.Count==0)
            {
                TempData["NoDiagnoses"] = "Oh oops! The patient hase no disgnoses";
            }
            return View(diagnosis);

        }
        

        //GEt
        public IActionResult Click() 
        {
            return View("prescription");
        }
        [HttpPost]
        public IActionResult Save(Diagnose model )
        {
            // Get the current patient
            var patient = _db.AspNetUsers.FirstOrDefault(p => p.Condition == 0);
            if (patient != null)
            {
                var appointment = _db.Appointments.FirstOrDefault(a => a.PatientID == patient.Id);
                if (appointment != null)
                {
                    // Create a new diagnosis
                    var diagnosis = new Diagnose
                    {
                        Prescription = model.Prescription,
                        Description = model.Description,
                        Analysis = model.Analysis,
                        fk_app = appointment.Id
                        //AppointmentId = patient.AppointmentId
                    };

                    // Add the diagnosis to the database
                    _db.Diagnose.Add(diagnosis);
                    _db.SaveChanges();

                    // Update the patient's condition
                    patient.Condition = 1;
                    _db.SaveChanges();


                    // Redirect to the Diagnoses page
                    return RedirectToAction("Hictory", new { id = patient.Id});
                }
                else { return NotFound("The Appointments is Finished"); }
            }
            else { return NotFound("The Patients is Finished"); } 

                

        }


            /*------------------------------------------------------------------------------*/



            public async Task<IActionResult> Patient()
            {
               var nextPatient = await _db.AspNetUsers
                   .Include(p => p.Appointments)
                   .Where(p => p.Condition == 0 && p.Appointments.Any())
                   .FirstOrDefaultAsync();

               if (nextPatient == null)
               {
                    ViewBag.NoUser="There are no patients";
                    return RedirectToAction("Work");
                }
                return View(nextPatient);
            }
       



        /*------------------------------------------------------------------------------*/
        public IActionResult Appointement()
		{

            var patients = _db.AspNetUsers
               
               .Include(p => p.Appointments)
               .Where(p => p.Condition == 0 && p.Appointments.Any())
               .ToList();

            ViewBag.Pat = patients;

            return View(patients);
        }



/*-------------------------------------------------------------------------------------------------------------------------------------------------------------------*/


        


        /*------------------------------------------------------------------------------*/


        public IActionResult Work()
        {
            return View(ViewBag.NoUser);
        }



    }
}
