using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HMSproject.Models;
using Microsoft.EntityFrameworkCore;

namespace HMSproject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HmsContext _context;

    public HomeController(ILogger<HomeController> logger,HmsContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult SystemLogin()
    {
        return View();
    }
    

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult SystemLogin(FormSystemLogin model)
    {
        if (model.Type=="admin")
        {
            if (model.Email=="admin@mail.com" && model.Password=="password")
            {
                return RedirectToAction("Index", "HomeAdmin");
            }
            else
            {
                TempData["message"] = "invalid login";
                return View();
            }
        }
        else if (model.Type == "nurse")
        {
            var nurse=_context.Nurses.FromSql($"SELECT * FROM nurses where email={model.Email} and password={model.Password}");
            if (nurse.Count()>0)
            {
                return RedirectToAction("Index", "Nurse");
            }
            else
            {
                TempData["message"] = "invalid login";
                return View();
            }
        }
        else if (model.Type == "doctor")
        {
            var doctor=_context.Doctors.FromSql($"SELECT * FROM doctors where email={model.Email} and password={model.Password}");
            if (doctor.Count()>0)
            {
                return RedirectToAction("Index", "Nurse");
            }
            else
            {
                TempData["message"] = "invalid login";
                return View();
            }
        }
        return View();
    }
    
    

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> Services()
    {
        return View();
    }
    
    public async Task<IActionResult> Department()
    {
        return View();
    }
    public async Task<IActionResult> Doctors()
    {
        return View();
    }

    public async Task<IActionResult> Contact()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}