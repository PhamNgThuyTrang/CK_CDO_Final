using CK_CDO_Final.Entities;
using CK_CDO_Final.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CK_CDO_Final.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly OracleDbContext _context;

        public HomeController(ILogger<HomeController> logger, OracleDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["User Account"] = _context.Users.Count();
            ViewData["Total Hose"] = _context.Hose.Count() + _context.Upcom.Count() + _context.Upcom.Count();
            ViewData["Total Companies"] = _context.CompanyDetails.Count();
            ViewData["Total Index"] = _context.Index.Count();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
