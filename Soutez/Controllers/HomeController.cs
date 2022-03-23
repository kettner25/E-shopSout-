using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Soutez.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Soutez.Controllers
{
    public class HomeController : Controller
    {
        public int[] PolozkyKosik()
        {
            int[] polozkyCena = new int[2];

            string[] polozky = HttpContext.Session.GetString("Polozky").Split(";");

            foreach (string polozka in polozky)
            {
                if (Data.Polozky.Where(p => p.ID + "" == polozka).FirstOrDefault() != null)
                {
                    polozkyCena[0]++;
                    polozkyCena[1] += Data.Polozky.Where(p => p.ID + "" == polozka).First().Cena;
                }
            }

            return polozkyCena;
        }

        public IActionResult Index()
        {
            List<Polozka> polozky = Data.Polozky.Where(p => p.Akce).ToList();

            if (HttpContext.Session.GetString("Polozky") == null)
                HttpContext.Session.SetString("Polozky", "");

            ViewBag.Kosik = PolozkyKosik();

            return View(polozky);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
