using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BugProjektV1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugProjektV1.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
    }
}