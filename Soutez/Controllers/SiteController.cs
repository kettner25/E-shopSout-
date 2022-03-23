using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Soutez.Models;
using Microsoft.AspNetCore.Http;

namespace Soutez.Controllers
{
    public class SiteController : Controller
    {
        public int[] PolozkyKosik()
        {
            int[] polozkyCena = new int[2];

            string[] polozky = HttpContext.Session.GetString("Polozky").Split(";");

            foreach (string polozka in polozky)
            {
                if(Data.Polozky.Where(p => p.ID + "" == polozka).FirstOrDefault() != null)
                {
                    polozkyCena[0]++;
                    polozkyCena[1] += Data.Polozky.Where(p => p.ID + "" == polozka).First().Cena;
                }
            }

            return polozkyCena;
        }

        public IActionResult Kategorie(string kategorie)
        {
            List<Polozka> polozky = Data.Polozky.Where(p => p.Kategorie == kategorie).ToList();

            ViewBag.Data = polozky;

            ViewBag.Kosik = PolozkyKosik();

            ViewBag.Loc = new string[] { kategorie };

            return View();
        }

        public IActionResult PolozkaDetail(string kategorie, int polozka)
        {
            Polozka _polozka = Data.Polozky.Where(p => p.ID == polozka).FirstOrDefault();

            if (_polozka == null)
                return Redirect("/Site/Kategorie?kategorie="+ kategorie);

            ViewBag.Kosik = PolozkyKosik();

            ViewBag.Data = _polozka;

            ViewBag.Loc = new string[] { kategorie, _polozka.Jmeno };

            return View();
        }


        public IActionResult Kosik()
        {
            ViewBag.Kosik = PolozkyKosik();

            ViewBag.Loc = new string[] { "Košík" };

            ViewBag.Data = HttpContext.Session.GetString("Polozky").Split(";");
            ViewBag.Polozky = Data.Polozky;

            return View();
        }


        public void PridejDoKosiku(int id)
        {
            if (Data.Polozky.Where(p => p.ID == id).FirstOrDefault() == null)
                return;

            Polozka polozka = Data.Polozky.Where(p => p.ID == id).First();

            string[] koupene = HttpContext.Session.GetString("Polozky").Split(";");
            int pocet = 0;

            foreach(string i in koupene)
            {
                if (Data.Polozky.Where(p => p.ID + "" == i).FirstOrDefault() != null)
                    pocet++;
            }

            if (polozka.Dostupnost - pocet <= 0 || pocet + 1 == polozka.MaxKeKoupi)
                return;

            HttpContext.Session.SetString("Polozky", HttpContext.Session.GetString("Polozky") + ";"+id );
        }
    }
}
