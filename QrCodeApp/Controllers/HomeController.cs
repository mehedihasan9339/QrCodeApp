using Microsoft.AspNetCore.Mvc;
using QrCodeApp.Models;
using System.Diagnostics;

namespace QrCodeApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
