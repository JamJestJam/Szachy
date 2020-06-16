using System;
using System.Collections.Generic;
using System.Linq;

namespace LogikaSzachy
{
    /// <summary>
    /// Lista wszystkich możliwych bierek
    /// </summary>
    public enum Bierki
    {
        Hetman,
        Goniec,
        Skoczek,
        Wieża,
        Krol,
        Pionek
    }
    /// <summary>
    /// Interface dla każdej bierki
    /// </summary>
    public abstract class Bierka
    {
        /// <summary>
        /// Wartosc punktowa bierki
        /// </summary>
        public int WartoscPunktowa { get; protected set; }
        /// <summary>
        /// Nazwa bierki
        /// </summary>
        public Bierki Nazwa { get; protected set; }
        /// <summary>
        /// Strona po której gra bierka
        /// </summary>
        public Strona Kolor { get; protected set; }
        /// <summary>
        /// Plansza na której znajduje się bierka
        /// </summary>
        protected Plansza plansza;
        /// <summary>
        /// Informacja o tym czy bierka wykonała już jakiś ruch
        /// </summary>
        internal bool PierwszyRuch { get; set; }
        /// <summary>
        /// pozycja na której znajduje się bierka
        /// </summary>
        public Punkt Pozycja { get; internal set; }
        /// <summary>
        /// tworzy liste możliwych do wykonania przez daną bierkę ruchów
        /// </summary>
        /// <returns>zwraca listę punktów na które bierka może się przemieścić</returns>
        protected abstract List<Punkt> MozliweRuchy();
        /// <summary>
        /// Numer ruchu dla ktorego sa policzone ruchy
        /// </summary>
        internal int Kolejka = -1;
        /// <summary>
        /// lista ruchow mozliwych do wykonania w kolejce
        /// </summary>
        protected List<Punkt> policzoneRuchy;
        /// <summary>
        /// Lista możliwych punktów na które bierka może się przemiescić
        /// </summary>
        public virtual IReadOnlyList<Punkt> PobMozliweRuchy
        {
            get
            {
                if (Kolejka != plansza.Ruchy)
                {
                    policzoneRuchy = MozliweRuchy();
                    if (Kolor == plansza.StronaGrajaca)
                    {
                        if (NumerOgraniczenia == plansza.Ruchy)
                        {
                            policzoneRuchy = policzoneRuchy.Intersect(Ograniczenia).ToList();
                        }
                        if (plansza.zaslonieceiSzacha != null)
                        {
                            policzoneRuchy = policzoneRuchy.Intersect(plansza.zaslonieceiSzacha).ToList();
                        }
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
        public virtual bool WykonajRuch(Punkt przemieszczenie)
        {
            //sprawdz czy przemieszczenie znajduje sie na liscie mozliwych ruchow
            if (PobMozliweRuchy.Contains(przemieszczenie))
            {
                PierwszyRuch = false;
                Pozycja = przemieszczenie;
                return true;
            }
            return false;
        }
        /// <summary>
        /// Test czy punkt do przemieszczenia spełnia warunki.
        /// </summary>
        /// <param name="przemieszczenie">Punkt na ktory przemieszcza sie bierka</param>
        /// <param name="ruchy">lista ruchów do ktorej nalezy dodac bierke</param>
        /// <param name="zbicie">czy jest akcja zbijania bierki</param>
        /// <returns>zwraca prawde jeżeli nie wykryto problemow z przemieszczeniem</returns>
        protected bool SprawdzMozliwoscWykonaniaRuchu(Punkt przemieszczenie, List<Punkt> ruchy, bool zbicie = true)
        {
            //sprawdzenie czy nadal znajdujemy sie na planszy
            if (przemieszczenie.Pomiedzy(7))
            {
                //sprawdzenie czy na danej pozycji znajduje sie bierka
                if (plansza.BierkaNaPozycji(przemieszczenie, out Bierka bierka))
                {
                    //jezeli kolory sa rozne dodaj mozliwosc zbicia
                    if (bierka.Kolor != Kolor && zbicie)
                        ruchy.Add(przemieszczenie);
                    return true;
                }
                ruchy.Add(przemieszczenie);
                return false;
            }
            return true;
        }
        //sterowanie mozliwosciami ruchowymi
        int numerOgraniczenia = -1;
        /// <summary>
        /// Numer ruchu dla ktorego sa wprowadzone ograniczenia
        /// </summary>
        internal int NumerOgraniczenia
        {
            get => numerOgraniczenia; set
            {
                if (value != numerOgraniczenia)
                {
                    numerOgraniczenia = value;
                    Ograniczenia = new List<Punkt>();
                }
            }
        }
        /// <summary>
        /// ograniczenia w poruszaniu sie bierki
        /// </summary>
        internal List<Punkt> Ograniczenia
        {
            get; set;
        }
    }
}
