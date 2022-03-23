using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Soutez.Models
{
    public class Polozka
    {
        public int ID { get; set; }

        public int Code { get; set; }

        public string Jmeno { get; set; }

        public int Cena { get; set; }

        public int Dostupnost { get; set; }

        public bool Akce { get; set; }

        public string Poznamka { get; set; }

        public string Kategorie { get; set; }

        public string Popis { get; set; }

        public string UrlImg { get; set; }

        public int MaxKeKoupi { get; set; }
    }
}
