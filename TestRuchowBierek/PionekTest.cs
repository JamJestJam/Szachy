using LogikaSzachy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestRuchowBierek
{
    public partial class TestRuchow
    {
        [DataTestMethod]
        [DataRow(4, 3)]
        [DataRow(4, 2)]
        public void TestRuchowPionka(int x, int y)
        {
            //tworzenie danych
            List<Bierka> listaBierek = new List<Bierka>();
            Plansza plansza = new Plansza(null, null, listaBierek);
            //dodawanie bierek na stol
            listaBierek.Add(new Krol(new Punkt(0, 0), Strona.Biała, plansza));
            listaBierek.Add(new Krol(new Punkt(7, 7), Strona.Czarna, plansza));
            listaBierek.Add(new Pionek(new Punkt(4, 4), Strona.Biała, plansza));
       
            //sprawdzanie poprawnosci danych
            Assert.IsTrue(plansza.SprobujWykonacRuch(new Punkt(4, 4), new Punkt(x, y)));
        }

        [DataTestMethod]
        [DataRow(0, 0)]
        [DataRow(1, 0)]
        [DataRow(2, 0)]
        [DataRow(3, 0)]
        [DataRow(4, 0)]
        [DataRow(5, 0)]
        [DataRow(6, 0)]
        [DataRow(7, 0)]
        [DataRow(0, 1)]
        [DataRow(1, 1)]
        [DataRow(2, 1)]
        [DataRow(3, 1)]
        [DataRow(4, 1)]
        [DataRow(5, 1)]
        [DataRow(6, 1)]
        [DataRow(7, 1)]
        [DataRow(0, 2)]
        [DataRow(1, 2)]
        [DataRow(2, 2)]
        [DataRow(3, 2)]
        [DataRow(5, 2)]
        [DataRow(6, 2)]
        [DataRow(7, 2)]
        [DataRow(0, 3)]
        [DataRow(1, 3)]
        [DataRow(2, 3)]
        [DataRow(3, 3)]
        [DataRow(5, 3)]
        [DataRow(6, 3)]
        [DataRow(7, 3)]
        [DataRow(0, 4)]
        [DataRow(1, 4)]
        [DataRow(2, 4)]
        [DataRow(3, 4)]
        [DataRow(4, 4)]
        [DataRow(5, 4)]
        [DataRow(6, 4)]
        [DataRow(7, 4)]
        [DataRow(0, 5)]
        [DataRow(1, 5)]
        [DataRow(2, 5)]
        [DataRow(3, 5)]
        [DataRow(4, 5)]
        [DataRow(5, 5)]
        [DataRow(6, 5)]
        [DataRow(7, 5)]
        [DataRow(0, 6)]
        [DataRow(1, 6)]
        [DataRow(2, 6)]
        [DataRow(3, 6)]
        [DataRow(4, 6)]
        [DataRow(5, 6)]
        [DataRow(6, 6)]
        [DataRow(7, 6)]
        [DataRow(0, 7)]
        [DataRow(1, 7)]
        [DataRow(2, 7)]
        [DataRow(3, 7)]
        [DataRow(4, 7)]
        [DataRow(5, 7)]
        [DataRow(6, 7)]
        [DataRow(7, 7)]
        public void TestRuchowPionkaNiemozliwychDoWykonania(int x, int y)
        {
            //tworzenie danych
            List<Bierka> listaBierek = new List<Bierka>();
            Plansza plansza = new Plansza(null, null, listaBierek);
            //dodawanie bierek na stol
            listaBierek.Add(new Krol(new Punkt(0, 0), Strona.Biała, plansza));
            listaBierek.Add(new Krol(new Punkt(7, 7), Strona.Czarna, plansza));
            listaBierek.Add(new Pionek(new Punkt(4, 4), Strona.Biała, plansza));
       
            //sprawdzanie poprawnosci danych
            Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(4, 4), new Punkt(x, y)));
        }

        [DataTestMethod]
        [DataRow(4, 5)]
        [DataRow(4, 6)]
        public void TestRuchowPionkaOgraniczonegoInnymiBierkamiTegoSamegoKoloru(int x, int y)
        {
            //tworzenie danych
            List<Bierka> listaBierek = new List<Bierka>();
            Plansza plansza = new Plansza(null, null, listaBierek);
            //dodawanie bierek na stol
            listaBierek.Add(new Krol(new Punkt(0, 0), Strona.Biała, plansza));
            listaBierek.Add(new Krol(new Punkt(7, 7), Strona.Czarna, plansza));
            listaBierek.Add(new Pionek(new Punkt(4, 4), Strona.Biała, plansza));
            listaBierek.Add(new Pionek(new Punkt(4, 5), Strona.Biała, plansza));
        
            //sprawdzanie poprawnosci danych
            Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(4, 4), new Punkt(x, y)));
        }

        [DataTestMethod]
        [DataRow(4, 5)]
        [DataRow(4, 6)]
        public void TestRuchowPionkaOgraniczonegoInnymiBierkamiInnegoKoloru(int x, int y)
        {
            //tworzenie danych
            List<Bierka> listaBierek = new List<Bierka>();
            Plansza plansza = new Plansza(null, null, listaBierek);
            //dodawanie bierek na stol
            listaBierek.Add(new Krol(new Punkt(0, 0), Strona.Biała, plansza));
            listaBierek.Add(new Krol(new Punkt(7, 7), Strona.Czarna, plansza));
            listaBierek.Add(new Pionek(new Punkt(4, 4), Strona.Biała, plansza));
            listaBierek.Add(new Pionek(new Punkt(4, 5), Strona.Czarna, plansza));

            //sprawdzanie poprawnosci danych
            Assert.IsFalse(plansza.SprobujWykonacRuch(new Punkt(4, 4), new Punkt(x, y)));
        }
        [TestMethod]
        [DataRow(3, 3)]
        [DataRow(5, 3)]
        public void TestBiciaPionkiem(int x, int y)
        {
            //tworzenie danych
            List<Bierka> listaBierek = new List<Bierka>();
            Plansza plansza = new Plansza(null, null, listaBierek);
            //dodawanie bierek na stol
            listaBierek.Add(new Krol(new Punkt(0, 0), Strona.Biała, plansza));
            listaBierek.Add(new Krol(new Punkt(7, 7), Strona.Czarna, plansza));
            listaBierek.Add(new Pionek(new Punkt(4, 4), Strona.Biała, plansza));
            listaBierek.Add(new Pionek(new Punkt(x, y), Strona.Czarna, plansza));
   
            //sprawdzanie poprawnosci danych
            Assert.IsTrue(plansza.SprobujWykonacRuch(new Punkt(4, 4), new Punkt(x, y)));
        }

        [TestMethod]
        public void TestWykonaniaBiciaWprzelocie()
        {
            //tworzenie danych
            List<Bierka> listaBierek = new List<Bierka>();
            Plansza plansza = new Plansza(null, Wygrana, listaBierek);
            //dodawanie bierek na stol
            listaBierek.Add(new Krol(new Punkt(0, 0), Strona.Biała, plansza));
            listaBierek.Add(new Krol(new Punkt(7, 7), Strona.Czarna, plansza));
            listaBierek.Add(new Pionek(new Punkt(3, 4), Strona.Czarna, plansza));
            listaBierek.Add(new Pionek(new Punkt(4, 6), Strona.Biała, plansza));
            plansza.TestRuchow();
            //sprawdzanie poprawnosci danych
            Assert.IsTrue(plansza.SprobujWykonacRuch(new Punkt(4, 6), new Punkt(4, 4)));
            Assert.IsTrue(plansza.BierkaNaPozycji(new Punkt(3, 4), out _));
            Assert.IsTrue(plansza.SprobujWykonacRuch(new Punkt(3, 4), new Punkt(3, 5)));
        }
    }
}
