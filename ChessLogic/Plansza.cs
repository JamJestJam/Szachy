using System;
using System.Collections.Generic;

namespace LogikaSzachy
{
    /// <summary>
    /// Strony na planszy
    /// </summary>
    public enum Strona
    {
        Biała,
        Czarna
    }
    /// <summary>
    /// Klasa symulująca planszę do gry w szachy
    /// </summary>
    public class Plansza
    {
        /// <summary>
        /// Lista możliwych stanów gry
        /// </summary>
        public enum Status
        {
            Gra,
            Mat,
            Pat
        }
        /// <summary>
        /// Aktualny stan gry
        /// </summary>
        public Status StanGry { get; private set; }
        /// <summary>
        /// Lista wszystkich bierek na planszy
        /// </summary>
        readonly List<Bierka> bierki = new List<Bierka>();
        /// <summary>
        /// lista bialych bierek
        /// </summary>
        readonly List<Bierka> bierkiBiale = new List<Bierka>();
        /// <summary>
        /// lista czarnych bierek
        /// </summary>
        readonly List<Bierka> bierkiCzarne = new List<Bierka>();
        /// <summary>
        /// bialy krol
        /// </summary>
        Krol krolBialy;
        /// <summary>
        /// czarny krol
        /// </summary>
        Krol krolCzarny;
        public Strona StronaGrajaca { get; private set; }
        /// <summary>
        /// Funkcja wołana w momencie promocjii pionka
        /// </summary>
        readonly Func<Bierki> promocjaPionka;
        /// <summary>
        /// Funkcja wołana w momencie zakończenia gry
        /// </summary>
        readonly Action<Status> koniecGry;
        /// <summary>
        /// krol grajacy
        /// </summary>
        Krol krolGrajacy
        {
            get { return (StronaGrajaca == Strona.Biała) ? krolBialy : krolCzarny; }
        }
        /// <summary>
        /// lista bierek aktualnie grajacych
        /// </summary>
        public IReadOnlyList<Bierka> bierkiGrajace
        {
            get
            { return (StronaGrajaca == Strona.Biała) ? bierkiBiale.AsReadOnly() : bierkiCzarne.AsReadOnly(); }
        }
        /// <summary>
        /// lista bierek aktualnie nie grajacych
        /// </summary>
        public IReadOnlyList<Bierka> bierkiNieGrajace
        {
            get
            { return (StronaGrajaca == Strona.Biała) ? bierkiBiale.AsReadOnly() : bierkiCzarne.AsReadOnly(); }
        }
        /// <summary>
        /// Lista wszyskich bierek na planszy
        /// </summary>
        public IReadOnlyList<Bierka> Bierki => bierki.AsReadOnly();
        /// <summary>
        /// Konstruktor tworzący planszę do gry
        /// </summary>
        public Plansza(Func<Bierki> promocjaPionka, Action<Status> koniecGry, bool init = true)
        {
            this.promocjaPionka = promocjaPionka;
            this.koniecGry = koniecGry;

            if (init)
                Init();
        }
        /// <summary>
        /// Tworzenie podstawowej planszy
        /// </summary>
        void Init()
        {
            //pionki
            for (int i = 0; i < 8; i++)
                bierki.Add(new Pionek(new Punkt(i, 1), Strona.Czarna, this));
            for (int i = 0; i < 8; i++)
                bierki.Add(new Pionek(new Punkt(i, 6), Strona.Biała, this));
            //figury czarne
            bierki.Add(new Wieza(new Punkt(0, 0), Strona.Czarna, this));
            bierki.Add(new Wieza(new Punkt(7, 0), Strona.Czarna, this));
            bierki.Add(new Goniec(new Punkt(2, 0), Strona.Czarna, this));
            bierki.Add(new Goniec(new Punkt(5, 0), Strona.Czarna, this));
            bierki.Add(new Skoczek(new Punkt(1, 0), Strona.Czarna, this));
            bierki.Add(new Skoczek(new Punkt(6, 0), Strona.Czarna, this));
            bierki.Add(new Hetman(new Punkt(3, 0), Strona.Czarna, this));
            bierki.Add(new Krol(new Punkt(4, 0), Strona.Czarna, this));
            //figury biale
            bierki.Add(new Wieza(new Punkt(0, 7), Strona.Biała, this));
            bierki.Add(new Wieza(new Punkt(7, 7), Strona.Biała, this));
            bierki.Add(new Goniec(new Punkt(2, 7), Strona.Biała, this));
            bierki.Add(new Goniec(new Punkt(5, 7), Strona.Biała, this));
            bierki.Add(new Skoczek(new Punkt(1, 7), Strona.Biała, this));
            bierki.Add(new Skoczek(new Punkt(6, 7), Strona.Biała, this));
            bierki.Add(new Hetman(new Punkt(3, 7), Strona.Biała, this));
            bierki.Add(new Krol(new Punkt(4, 7), Strona.Biała, this));

            Przydziel();
        }
        /// <summary>
        /// przydziela bierki do odpowiednich list
        /// </summary>
        void Przydziel()
        {
            for (int i = 0; i < bierki.Count; i++)
            {
                if (bierki[i].Kolor == Strona.Biała)
                {
                    bierkiBiale.Add(bierki[i]);
                }
                else
                {
                    bierkiCzarne.Add(bierki[i]);
                }

                if (bierki[i].Nazwa == LogikaSzachy.Bierki.Krol)
                {
                    if (bierki[i].Kolor == Strona.Biała)
                    {
                        krolBialy = (Krol)bierki[i];
                    }
                    else
                    {
                        krolCzarny = (Krol)bierki[i];
                    }
                }
            }
        }
        public bool BierkaNaPozycji(Punkt pozycja, out LogikaSzachy.Bierki bierka)
        {
            var tmp = (this.bierki.Find(x=>x.Pozycja==pozycja));
            if(tmp == null)
            {
                bierka = 0;
                return false;
            }
            bierka = tmp.Nazwa;
            return true;
        }
    }
}
