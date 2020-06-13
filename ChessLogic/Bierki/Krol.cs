using System.Collections.Generic;
using System.Linq;

namespace LogikaSzachy
{
    /// <summary>
    /// Bierka krol
    /// </summary>
    public class Krol : Bierka
    {
        /// <summary>
        /// tworzenie bierki krol
        /// </summary>
        /// <param name="pozycja">pozycja startowa bierki</param>
        /// <param name="kolor">kolor bierki bialy/czarny</param>
        /// <param name="plansza">plansza na ktorej bierka sie znajduje</param>
        /// <param name="pierwszyRuch">wartosc logiczna zawierajaca informacje o tym czy bierka wykonala juz jakis ruch</param>
        public Krol(Punkt pozycja, Strona kolor, Plansza plansza, bool pierwszyRuch = true)
        {
            this.Nazwa = Bierki.Krol;
            this.Pozycja = pozycja;
            this.Kolor = kolor;
            this.plansza = plansza;
            this.PierwszyRuch = pierwszyRuch;
            this.wartoscPunktowa = int.MaxValue;
        }
        /// <summary>
        /// tworzenie listy możliwych do wykonania ruchow przez krola
        /// </summary>
        /// <returns>zwraca listę punktów na które krol moze się przemieścić</returns>
        protected override List<Punkt> MozliweRuchy()
        {
            List<Punkt> mozliweRuchy = new List<Punkt>();
            //krol moze przemieszczac sie w kazdym kierunku o jedno pole
            //w gore
            SprawdzMozliwoscWykonaniaRuchu(Pozycja - new Punkt(0, 1), mozliweRuchy);
            //w gore lewo
            SprawdzMozliwoscWykonaniaRuchu(Pozycja - new Punkt(1, 1), mozliweRuchy);
            //w lewo
            SprawdzMozliwoscWykonaniaRuchu(Pozycja - new Punkt(1, 0), mozliweRuchy);
            //w lewo dol
            SprawdzMozliwoscWykonaniaRuchu(Pozycja - new Punkt(1, -1), mozliweRuchy);
            //w dol
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(0, 1), mozliweRuchy);
            //w dol i prawo
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(1, 1), mozliweRuchy);
            //w prawo
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(1, 0), mozliweRuchy);
            //w prawo i gore
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(1, -1), mozliweRuchy);

            //dluga roszada
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(-2, 0), mozliweRuchy);
            //krotka roszada
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(2, 0), mozliweRuchy);

            //krol moze rowniez wykonac roszady
            //to do
            return mozliweRuchy;
        }
        /// <summary>
        /// Lista możliwych punktów na które bierka może się przemiescić
        /// </summary>
        public override IReadOnlyList<Punkt> PobMozliweRuchy
        {
            get
            {
                if (Kolejka != plansza.Ruchy)
                {
                    policzoneRuchy = MozliweRuchy();
                    TestPozostalychPol();
                    policzoneRuchy = policzoneRuchy.Except(Ograniczenia).ToList();
                }
                return policzoneRuchy.AsReadOnly();
            }
        }
        /// <summary>
        /// test ograniczenia ruchow dla krola
        /// </summary>
        /// <returns></returns>
        private void TestPozostalychPol()
        {
            Ograniczenia = new List<Punkt>();
            //podstawowe pozycje
            TestPozycji(Pozycja + new Punkt(0, 0), true, true, true, true);//srodek

            TestPozycji(Pozycja + new Punkt(1, 0), true, false, true, true);//prawo
            TestPozycji(Pozycja + new Punkt(-1, 0), true, false, true, true);//lewo
            TestPozycji(Pozycja + new Punkt(0, -1), false, true, false, false);//gora
            TestPozycji(Pozycja + new Punkt(0, 1), false, true, false, false);//dol

            TestPozycji(Pozycja + new Punkt(-1, -1), false, false, true, false);//lewo gora
            TestPozycji(Pozycja + new Punkt(1, -1), false, false, false, true);//prawo gora
            TestPozycji(Pozycja + new Punkt(-1, 1), false, false, false, true);//lewo dol
            TestPozycji(Pozycja + new Punkt(1, 1), false, false, true, false);//prawo dol

            //do roszady
            if (!Ograniczenia.Contains(Pozycja + new Punkt(0, 1)))
                TestPozycji(Pozycja + new Punkt(2, 0), true, false, true, true);
            else
                Ograniczenia.Add(Pozycja + new Punkt(2, 0));
            if (!Ograniczenia.Contains(Pozycja - new Punkt(0, 1)))
                TestPozycji(Pozycja - new Punkt(2, 0), true, false, true, true);
            else
                Ograniczenia.Add(Pozycja - new Punkt(2, 0));
        }

        private void TestPozycji(Punkt start, bool pion, bool poziom, bool skos1, bool skos2)
        {
            //pion
            if (pion)
            {
                bool plus = true;
                bool minus = true;
                for (int i = 2; i < 8; i++)
                {
                    Punkt test1 = start + new Punkt(0, i);
                    Punkt test2 = start - new Punkt(0, i);

                    if (plus)
                        if (TestLini(test1, new List<Bierki> { Bierki.Hetman, Bierki.Wieża, Bierki.Krol }, i, start + new Punkt(0, 1), start, start - new Punkt(0, 1), out minus))
                            plus = false;
                    if (minus)
                        if (TestLini(test2, new List<Bierki> { Bierki.Hetman, Bierki.Wieża, Bierki.Krol }, i, start - new Punkt(0, 1), start, start + new Punkt(0, 1), out plus))
                            minus = false;
                }
            }
            //poziom
            if (poziom)
            {
                bool plus = true;
                bool minus = true;
                for (int i = 2; i < 8; i++)
                {
                    Punkt test1 = start + new Punkt(i, 0);
                    Punkt test2 = start - new Punkt(i, 0);

                    if (plus)
                        if (TestLini(test1, new List<Bierki> { Bierki.Hetman, Bierki.Wieża, Bierki.Krol }, i, start + new Punkt(1, 0), start, start - new Punkt(1, 0), out minus))
                            plus = false;
                    if (minus)
                        if (TestLini(test2, new List<Bierki> { Bierki.Hetman, Bierki.Wieża, Bierki.Krol }, i, start - new Punkt(1, 0), start, start + new Punkt(1, 0), out plus))
                            minus = false;
                }
            }
            //skos +
            if (skos1)
            {
                bool plus = true;
                bool minus = true;
                for (int i = 2; i < 8; i++)
                {
                    Punkt test1 = start + new Punkt(i, i);
                    Punkt test2 = start - new Punkt(i, i);

                    if (plus)
                        if (TestLini(test1, new List<Bierki> { Bierki.Hetman, Bierki.Wieża, Bierki.Krol }, i, start + new Punkt(1, 1), start, start - new Punkt(1, 1), out minus))
                            plus = false;
                    if (minus)
                        if (TestLini(test2, new List<Bierki> { Bierki.Hetman, Bierki.Wieża, Bierki.Krol }, i, start - new Punkt(1, 1), start, start + new Punkt(1, 1), out plus))
                            minus = false;
                }
            }
            if (skos2)
            {
                //skos -
                bool plus = true;
                bool minus = true;
                for (int i = 2; i < 8; i++)
                {
                    Punkt test1 = start + new Punkt(i, -i);
                    Punkt test2 = start - new Punkt(i, -i);

                    if (plus)
                        if (TestLini(test1, new List<Bierki> { Bierki.Hetman, Bierki.Wieża, Bierki.Krol }, i, start + new Punkt(1, -1), start, start - new Punkt(1, -1), out minus))
                            plus = false;
                    if (minus)
                        if (TestLini(test2, new List<Bierki> { Bierki.Hetman, Bierki.Wieża, Bierki.Krol }, i, start - new Punkt(1, -1), start, start + new Punkt(1, -1), out plus))
                            minus = false;
                }
            }
        }

        private bool TestLini(Punkt pozycja, List<Bierki> atakujacy, int odleglosc, Punkt test1, Punkt start, Punkt test2, out bool test)
        {
            test = true;
            if (!pozycja.Pomiedzy(7))
                return true;
            //sprawdz czy istnieje bierka na pozycji
            if (plansza.BierkaNaPozycji(pozycja, out Bierka bierka))
            {
                if (bierka.Kolor != Kolor)//sprawdz czy jest przeciwnego koloru
                {
                    if (!atakujacy.Contains(bierka.Nazwa))
                        return true;
                    if ((bierka.Nazwa == Bierki.Pionek || bierka.Nazwa == Bierki.Krol))
                    {
                        if (odleglosc == 2)
                            Ograniczenia.Add(test1);
                        return true;
                    }
                    else
                    {
                        test = true;
                        Ograniczenia.Add(test1);
                        Ograniczenia.Add(test2);
                        Ograniczenia.Add(start);
                    }
                }
                return true;
            }
            return false;
        }
    }
}
