using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Licznik.Models
{
    public class Licznik
    {
        public string Nazwa { get; set; } = "";
        public int Wartosc { get; set; } = 0;

        public Licznik()
        {
        }
        public Licznik(string nazwa)
        {
            Nazwa = nazwa;
            Wartosc = 0;
        }
        public void Zwieksz()
        {
            Wartosc++;
        }
        public void Zmniejsz()
        {
            Wartosc--;
        }
    }
}
