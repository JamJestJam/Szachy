using LogikaSzachy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestRuchowBierek
{
    [TestClass]
    public class GoniecTest
    {
        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataRow(4, 4)]
        [DataRow(5, 5)]
        [DataRow(6, 6)]
        [DataRow(7, 7)]
        [DataRow(4, 2)]
        [DataRow(5, 1)]
        [DataRow(6, 0)]
        [DataRow(2, 4)]
        [DataRow(1, 5)]
        [DataRow(0, 6)]
        public void TestRuchowGonca(int x, int y)
        {
            //tworzenie danych
            List<Bierka> listaBierek = new List<Bierka>();
            Plansza plansza = new Plansza(null, null, listaBierek);
            //dodawanie bierek na stol
            listaBierek.Add(new Goniec(new Punkt(3, 3), Strona.Bia³a, plansza));
            listaBierek.Add(new Krol(new Punkt(3, 0), Strona.Bia³a, plansza));
            listaBierek.Add(new Krol(new Punkt(3, 7), Strona.Czarna, plansza));

            plansza.Przydziel();
            //sprawdzanie poprawnosci danych
            Assert.IsTrue(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
        }

        [DataTestMethod]
        [DataRow(1, 0)]
        [DataRow(2, 0)]
        [DataRow(3, 0)]
        [DataRow(4, 0)]
        [DataRow(5, 0)]
        [DataRow(7, 0)]
        [DataRow(0, 1)]
        [DataRow(2, 1)]
        [DataRow(3, 1)]
        [DataRow(4, 1)]
        [DataRow(6, 1)]
        [DataRow(7, 1)]
        [DataRow(0, 2)]
        [DataRow(1, 2)]
        [DataRow(3, 2)]
        [DataRow(5, 2)]
        [DataRow(6, 2)]
        [DataRow(7, 2)]
        [DataRow(0, 3)]
        [DataRow(1, 3)]
        [DataRow(2, 3)]
        [DataRow(4, 3)]
        [DataRow(5, 3)]
        [DataRow(6, 3)]
        [DataRow(7, 3)]
        [DataRow(0, 4)]
        [DataRow(1, 4)]
        [DataRow(3, 4)]
        [DataRow(5, 4)]
        [DataRow(6, 4)]
        [DataRow(7, 4)]
        [DataRow(0, 5)]
        [DataRow(2, 5)]
        [DataRow(3, 5)]
        [DataRow(4, 5)]
        [DataRow(6, 5)]
        [DataRow(7, 5)]
        [DataRow(1, 6)]
        [DataRow(2, 6)]
        [DataRow(3, 6)]
        [DataRow(4, 6)]
        [DataRow(5, 6)]
        [DataRow(7, 6)]
        [DataRow(0, 7)]
        [DataRow(1, 7)]
        [DataRow(2, 7)]
        [DataRow(3, 7)]
        [DataRow(4, 7)]
        [DataRow(5, 7)]
        [DataRow(6, 7)]
        public void TestRuchowGoncaNiemozliwychDoWykonania(int x, int y)
        {
            //tworzenie danych
            List<Bierka> listaBierek = new List<Bierka>();
            Plansza plansza = new Plansza(null, null, listaBierek);
            //dodawanie bierek na stol
            listaBierek.Add(new Goniec(new Punkt(3, 3), Strona.Bia³a, plansza));
            listaBierek.Add(new Krol(new Punkt(3, 0), Strona.Bia³a, plansza));
            listaBierek.Add(new Krol(new Punkt(3, 7), Strona.Czarna, plansza));

            plansza.Przydziel();
            //sprawdzanie poprawnosci danych
            Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
        }

        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataRow(4, 4)]
        [DataRow(5, 5)]
        [DataRow(6, 6)]
        [DataRow(7, 7)]
        [DataRow(4, 2)]
        [DataRow(5, 1)]
        [DataRow(6, 0)]
        [DataRow(2, 4)]
        [DataRow(1, 5)]
        [DataRow(0, 6)]
        public void TestRuchowGoncaOgraniczonegoInnymiBierkamiTegoSamegoKoloru(int x, int y)
        {
            //tworzenie danych
            List<Bierka> listaBierek = new List<Bierka>();
            Plansza plansza = new Plansza(null, null, listaBierek);
            //dodawanie bierek na stol
            listaBierek.Add(new Goniec(new Punkt(3, 3), Strona.Bia³a, plansza));
            listaBierek.Add(new Goniec(new Punkt(1, 1), Strona.Bia³a, plansza));
            listaBierek.Add(new Goniec(new Punkt(6, 6), Strona.Bia³a, plansza));
            listaBierek.Add(new Goniec(new Punkt(1, 5), Strona.Bia³a, plansza));
            listaBierek.Add(new Goniec(new Punkt(5, 1), Strona.Bia³a, plansza));
            listaBierek.Add(new Krol(new Punkt(3, 0), Strona.Bia³a, plansza));
            listaBierek.Add(new Krol(new Punkt(3, 7), Strona.Czarna, plansza));

            plansza.Przydziel();
            //sprawdzanie poprawnosci danych
            if (x <= 1 && y <= 1)
                Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
            else if (x >= 6 && y >= 6)
                Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
            else if (x <= 1 && y >= 5)
                Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
            else if (x >= 5 && y <= 1)
                Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
            else
                Assert.IsTrue(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
        }

        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataRow(4, 4)]
        [DataRow(5, 5)]
        [DataRow(6, 6)]
        [DataRow(7, 7)]
        [DataRow(4, 2)]
        [DataRow(5, 1)]
        [DataRow(6, 0)]
        [DataRow(2, 4)]
        [DataRow(1, 5)]
        [DataRow(0, 6)]
        public void TestRuchowGoncaOgraniczonegoInnymiBierkamiInnegoKoloru(int x, int y)
        {
            //tworzenie danych
            List<Bierka> listaBierek = new List<Bierka>();
            Plansza plansza = new Plansza(null, null, listaBierek);
            //dodawanie bierek na stol
            listaBierek.Add(new Goniec(new Punkt(3, 3), Strona.Bia³a, plansza));
            listaBierek.Add(new Goniec(new Punkt(1, 1), Strona.Czarna, plansza));
            listaBierek.Add(new Goniec(new Punkt(6, 6), Strona.Czarna, plansza));
            listaBierek.Add(new Goniec(new Punkt(1, 5), Strona.Czarna, plansza));
            listaBierek.Add(new Goniec(new Punkt(5, 1), Strona.Czarna, plansza));
            listaBierek.Add(new Krol(new Punkt(3, 0), Strona.Bia³a, plansza));
            listaBierek.Add(new Krol(new Punkt(3, 7), Strona.Czarna, plansza));

            plansza.Przydziel();
            //sprawdzanie poprawnosci danych
            if (x < 1 && y < 1)
                Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
            else if (x > 6 && y > 6)
                Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
            else if (x < 1 && y > 5)
                Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
            else if (x > 5 && y < 1)
                Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
            else
                Assert.IsTrue(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
        }

        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 1)]
        [DataRow(2, 2)]
        [DataRow(4, 4)]
        [DataRow(5, 5)]
        [DataRow(6, 6)]
        [DataRow(7, 7)]
        [DataRow(4, 2)]
        [DataRow(5, 1)]
        [DataRow(6, 0)]
        [DataRow(2, 4)]
        [DataRow(1, 5)]
        [DataRow(0, 6)]
        public void TestRuchowGoncaZasloniecieKrola(int x, int y)
        {
            //tworzenie danych
            List<Bierka> listaBierek = new List<Bierka>();
            Plansza plansza = new Plansza(null, null, listaBierek);
            //dodawanie bierek na stol
            listaBierek.Add(new Goniec(new Punkt(3, 3), Strona.Bia³a, plansza));
            listaBierek.Add(new Hetman(new Punkt(4, 0), Strona.Czarna, plansza));
            listaBierek.Add(new Krol(new Punkt(4, 7), Strona.Bia³a, plansza));
            listaBierek.Add(new Krol(new Punkt(0, 7), Strona.Czarna, plansza));

            plansza.Przydziel();
            //sprawdzanie poprawnosci danych
            if(x==4&& y==4)
                Assert.IsTrue(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
            else
                Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(3, 3), new Punkt(x, y)));
        }
    }
}
