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
        List<Bierka> BierkiBiale { get => bierki.FindAll(x => x.Kolor == Strona.Biała); }
        /// <summary>
        /// lista czarnych bierek
        /// </summary>
        List<Bierka> BierkiCzarne { get => bierki.FindAll(x => x.Kolor == Strona.Czarna); }
        /// <summary>
        /// bialy krol
        /// </summary>
        Krol krolBialy { get => (Krol)bierki.Find(x => x.Kolor == Strona.Biała && x.Nazwa == Bierki.Krol); }
        /// <summary>
        /// czarny krol
        /// </summary>
        Krol krolCzarny { get => (Krol)bierki.Find(x => x.Kolor == Strona.Czarna && x.Nazwa == Bierki.Krol); }
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
        Krol krolGrajacy { get => (StronaGrajaca == Strona.Biała) ? krolBialy : krolCzarny; }
        /// <summary>
        /// lista bierek aktualnie grajacych
        /// </summary>
        List<Bierka> BierkiGrajace { get => (StronaGrajaca == Strona.Biała) ? BierkiBiale : BierkiCzarne; }
        /// <summary>
        /// Konstruktor tworzący planszę do gry
        /// </summary>
        /// <param name="promocjaPionka">funkcja wywolywana w momencie promocji pionka zwracajaca figure na ktora pionek ma sie zamienic</param>
        /// <param name="koniecGry">funkcja wywolywana na koniec gry</param>
        public Plansza(Func<Bierki> promocjaPionka, Action<Status> koniecGry)
        {
            this.promocjaPionka = promocjaPionka;
            this.koniecGry = koniecGry;

            Init();
        }
        /// <summary>
        /// Konstruktor tworzący planszę do gry
        /// </summary>
        /// <param name="promocjaPionka">funkcja wywolywana w momencie promocji pionka zwracajaca figure na ktora pionek ma sie zamienic</param>
        /// <param name="koniecGry">funkcja wywolywana na koniec gry</param>
        /// <param name="listaBierek">nieregularne ulozenie startowe bierek na planszy</param>
        public Plansza(Func<Bierki> promocjaPionka, Action<Status> koniecGry, List<Bierka> listaBierek)
        {
            this.promocjaPionka = promocjaPionka;
            this.koniecGry = koniecGry;

            bierki = listaBierek;
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
        }
        /// <summary>
        /// Sprawdza czy itnieje bierka na podanej pozycji
        /// </summary>
        /// <param name="pozycja">pozycja do srpawdzenia</param>
        /// <param name="bierka">bierka ktora znajduje sie na podanej pozycji</param>
        /// <returns>zwraca prawda jezeli istnieje bierka na pozycji</returns>
        public bool BierkaNaPozycji(Punkt pozycja, out Bierka bierka)
        {
            var tmp = (this.bierki.Find(x => x.Pozycja == pozycja));
            if (tmp == null)
            {
                bierka = null;
                return false;
            }
            bierka = tmp;
            return true;
        }
        /// <summary>
        /// Probuje wykonac przemieszczenie bierki na wskazana pozycje
        /// </summary>
        /// <param name="pozycjaBierki">pozycja bierki ktora chcesz przemiescic</param>
        /// <param name="pozycjaPrzemieszczenia">pozycja na ktora chcesz przemiescic bierke</param>
        /// <returns>zwraca prawda jezeli udalo sie przemiescic bierke</returns>
        public bool SprobujWykonacRuch(Punkt pozycjaBierki, Punkt pozycjaPrzemieszczenia)
        {
            Bierka bierka = BierkiGrajace.Find(x => x.Pozycja == pozycjaBierki);
            if (bierka == null)
                return false;
            if (bierka.WykonajRuch(pozycjaPrzemieszczenia))
            {
                StronaGrajaca = (StronaGrajaca == Strona.Biała) ? Strona.Czarna : Strona.Biała;
                Bierka zbita = BierkiGrajace.Find(x => x.Pozycja == pozycjaPrzemieszczenia);
                if(bierka!=null)
                    bierki.Remove(zbita);
                return true;
            }
            return false;
        }
    }
}
