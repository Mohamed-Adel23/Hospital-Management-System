﻿using Microsoft.AspNetCore.Mvc;

namespace hmsAdmin.Controllers
{
    public class DeptController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
    }
}