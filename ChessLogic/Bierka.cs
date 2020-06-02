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
        Krol,
        Hetman,
        Goniec,
        Skoczek,
        Wieża,
        Pionek
    }
    /// <summary>
    /// Interface dla każdej bierki
    /// </summary>
    public abstract class Bierka
    {
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
        public bool PierwszyRuch { get; protected set; }
        /// <summary>
        /// pozycja na której znajduje się bierka
        /// </summary>
        public Punkt Pozycja { get; protected set; }
        /// <summary>
        /// tworzy liste możliwych do wykonania przez daną bierkę ruchów
        /// </summary>
        /// <returns>zwraca listę punktów na które bierka może się przemieścić</returns>
        protected abstract List<Punkt> MozliweRuchy();
        /// <summary>
        /// Lista możliwych punktów na które bierka może się przemiescić
        /// </summary>
        public IReadOnlyList<Punkt> PobMozliweRuchy
        {
            get
            {
                return MozliweRuchy().AsReadOnly();
            }
        }
        /// <summary>
        /// prubuje przemiescic bierke na wskazana pozycje
        /// </summary>
        /// <param name="przemieszczenie">pozycja na ktora ma sie przemiescic bierka</param>
        /// <returns>zwraca prawda jezeli udalo sie przemiescic bierke</returns>
        public bool WykonajRuch(Punkt przemieszczenie)
        {
            //sprawdz czy przemieszczenie znajduje sie na liscie mozliwych ruchow
            if (PobMozliweRuchy.Contains(przemieszczenie))
            {
                Pozycja = przemieszczenie;
                return true;
            }
            return false;
        }
        protected bool SprawdzMozliwoscWykonaniaRuchu(Punkt przemieszczenie, List<Punkt> ruchy)
        {
            //sprawdzenie czy nadal znajdujemy sie na planszy
            if (przemieszczenie.Pomiedzy(7))
            {
                //sprawdzenie czy na danej pozycji znajduje sie bierka
                if (plansza.BierkaNaPozycji(przemieszczenie, out Bierka bierka))
                {
                    //jezeli kolory sa rozne dodaj mozliwosc zbicia
                    if (bierka.Kolor != Kolor)
                        ruchy.Add(przemieszczenie);
                    return true;
                }
                ruchy.Add(przemieszczenie);
                return false;
            }
            return true;
        }
    }
}
