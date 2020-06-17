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
            this.WartoscPunktowa = 0;
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
            //w lewo dol
            SprawdzMozliwoscWykonaniaRuchu(Pozycja - new Punkt(1, -1), mozliweRuchy);
            //w dol
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(0, 1), mozliweRuchy);
            //w dol i prawo
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(1, 1), mozliweRuchy);
            //w prawo i gore
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(1, -1), mozliweRuchy);

            //dluga roszada
            //w lewo
            if (!SprawdzMozliwoscWykonaniaRuchu(Pozycja - new Punkt(1, 0), mozliweRuchy))
                if (PierwszyRuch && plansza.StronaGrajaca == Kolor)
                    if (!plansza.BierkaNaPozycji(Pozycja - new Punkt(3, 0), out _))
                        if (plansza.bierki.Exists(x => x.Nazwa == Bierki.Wieża && x.Pozycja == Pozycja - new Punkt(4, 0) && x.PierwszyRuch))
                            SprawdzMozliwoscWykonaniaRuchu(Pozycja - new Punkt(2, 0), mozliweRuchy);
            //krotka roszada
            //w prawo
            if (!SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(1, 0), mozliweRuchy))
                if (PierwszyRuch && plansza.StronaGrajaca == Kolor)
                    if (plansza.bierki.Exists(x => x.Nazwa == Bierki.Wieża && x.Pozycja == Pozycja + new Punkt(3, 0) && x.PierwszyRuch))
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
                    if (Kolor == plansza.StronaGrajaca)
                    {
                        policzoneRuchy = policzoneRuchy.Except(plansza.ListaRuchowPrzeciwnika).Except(Ograniczenia).ToList();

                        Bierka prawaWieza = plansza.bierki.Find(x => x.Nazwa == Bierki.Wieża && x.Pozycja == Pozycja + new Punkt(3, 0) && x.PierwszyRuch);
                        if (plansza.ListaRuchowPrzeciwnika.Contains(Pozycja))
                        {
                            policzoneRuchy.Remove(Pozycja + new Punkt(2, 0));
                            policzoneRuchy.Remove(Pozycja + new Punkt(-2, 0));
                        }
                        if (!policzoneRuchy.Exists(x => x == Pozycja + new Punkt(1, 0)))
                            policzoneRuchy.Remove(Pozycja + new Punkt(2, 0));
                        if (!policzoneRuchy.Exists(x => x == Pozycja + new Punkt(-1, 0)))
                            policzoneRuchy.Remove(Pozycja + new Punkt(-2, 0));
                    }
                }
                return policzoneRuchy.AsReadOnly();
            }
        }
        /// <summary>
        /// prubuje przemiescic bierke na wskazana pozycje
        /// </summary>
        /// <param name="przemieszczenie">pozycja na ktora ma sie przemiescic bierka</param>
        /// <returns>zwraca prawda jezeli udalo sie przemiescic bierke</returns>
        public override bool WykonajRuch(Punkt przemieszczenie)
        {
            //sprawdz czy przemieszczenie znajduje sie na liscie mozliwych ruchow
            if (PobMozliweRuchy.Contains(przemieszczenie))
            {
                PierwszyRuch = false;
                if (Pozycja - przemieszczenie == new Punkt(-2, 0))
                    plansza.bierki.Find(x => x.Pozycja == Pozycja + new Punkt(3, 0)).Pozycja = Pozycja + new Punkt(1, 0);
                if (Pozycja - przemieszczenie == new Punkt(2, 0))
                    plansza.bierki.Find(x => x.Pozycja == Pozycja - new Punkt(4, 0)).Pozycja = Pozycja - new Punkt(1, 0);
                Pozycja = przemieszczenie;

                return true;
            }
            return false;
        }
        /// <summary>
        /// Tworzy kopie Hetmana
        /// </summary>
        internal override Bierka Kopiuj(Plansza plansza)
        {
            return new Krol(Pozycja, Kolor, plansza, PierwszyRuch, PobMozliweRuchy.ToList(), Kolejka);
        }
        /// <summary>
        /// konstruktor do kopiowania
        /// </summary>
        /// <param name="pozycja">Pozycja kopiowanej bierki</param>
        /// <param name="kolor">Kolor bierki</param>
        /// <param name="plansza">nowa plansza</param>
        /// <param name="pierwszyRuch">stan zmiennej pierwszy ruch</param>
        /// <param name="policzoneRuchy">policzone ruchy bierki</param>
        Krol(Punkt pozycja, Strona kolor, Plansza plansza, bool pierwszyRuch, List<Punkt> policzoneRuchy, int kolejka)
        {
            this.Nazwa = Bierki.Krol;
            this.Pozycja = pozycja;
            this.Kolor = kolor;
            this.plansza = plansza;
            this.PierwszyRuch = pierwszyRuch;
            this.WartoscPunktowa = 0;
            this.policzoneRuchy = policzoneRuchy;
            this.Kolejka = kolejka;
        }
    }
}
