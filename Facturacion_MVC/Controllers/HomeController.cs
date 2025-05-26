using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Facturacion_MVC.Models;

namespace Facturacion_MVC.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Ayuda()
    {
        return View();
    }

    public IActionResult Acerca()
    {
        return View();
    }

    public IActionResult Contacto()
    {
        return View();
    }
}
