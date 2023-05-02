using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using HMSproject.Models;

namespace HMSproject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
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