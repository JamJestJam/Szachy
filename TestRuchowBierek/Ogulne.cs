using LogikaSzachy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestRuchowBierek
{
    public partial class TestRuchow
    {
        [DataTestMethod]
        [DataRow(5, 5)]
        [DataRow(3, 3)]
        [DataRow(5, 3)]
        [DataRow(3, 5)]
        public void TestZbijaniaBierek(int x, int y)
        {
            //tworzenie danych
            List<Bierka> listaBierek = new List<Bierka>();
            Plansza plansza = new Plansza(null, null, listaBierek);
            //dodawanie bierek na stol
            listaBierek.Add(new Krol(new Punkt(0, 0), Strona.Biała, plansza));
            listaBierek.Add(new Krol(new Punkt(7, 7), Strona.Czarna, plansza));
            listaBierek.Add(new Hetman(new Punkt(4, 4), Strona.Biała, plansza));

            listaBierek.Add(new Pionek(new Punkt(x, y), Strona.Czarna, plansza));

            //sprawdzanie poprawnosci danych
            Assert.IsTrue(plansza.SprobujWykonacRuch(new Punkt(4, 4), new Punkt(x, y)));
            Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(x, y), new Punkt(x, y) - new Punkt(x, y + 1)));
        }
    }
}
