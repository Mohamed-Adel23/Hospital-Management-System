using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HMSproject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace HMSproject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HmsContext _context;
    private readonly UserManager<Patient> _userManager;
    private readonly IEmailSender _emailSender;

    public HomeController(ILogger<HomeController> logger,UserManager<Patient> userManager,IEmailSender emailSender,HmsContext context)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _emailSender = emailSender;
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
            var nurses=_context.Nurses.FromSql($"SELECT * FROM nurses where email={model.Email} and password={model.Password}").ToList();
            if (nurses.Count()>0)
            {
                return RedirectToAction("Index", "Nurse_ayman",new { id =  nurses[0].Id});
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
                return RedirectToAction("Index", "HomeDoctor");
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
    public IActionResult Confirm()
    {
        return View();
    }
    [Authorize]
    public async Task<IActionResult> Contact()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(string message)
    {
        var user = await _userManager.GetUserAsync(User);
        _emailSender.SendEmailAsync("gguuhhidj@gmail.com", "Contact message From HMS Clients", $"From : {user.Email}<br/>{message}");
        return RedirectToAction("Confirm");
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    
}