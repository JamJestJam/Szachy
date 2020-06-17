using System.Collections.Generic;
using System.Linq;

namespace LogikaSzachy
{
    /// <summary>
    /// Bierka wieza
    /// </summary>
    public class Wieza : Bierka
    {
        /// <summary>
        /// tworzenie bierki wieza
        /// </summary>
        /// <param name="pozycja">pozycja startowa bierki</param>
        /// <param name="kolor">kolor bierki bialy/czarny</param>
        /// <param name="plansza">plansza na ktorej bierka sie znajduje</param>
        /// <param name="pierwszyRuch">wartosc logiczna zawierajaca informacje o tym czy bierka wykonala juz jakis ruch</param>
        public Wieza(Punkt pozycja, Strona kolor, Plansza plansza, bool pierwszyRuch = true)
        {
            this.Nazwa = Bierki.Wieża;
            this.Pozycja = pozycja;
            this.Kolor = kolor;
            this.plansza = plansza;
            this.PierwszyRuch = pierwszyRuch;
            this.WartoscPunktowa = 5;
        }
        /// <summary>
        /// tworzenie listy możliwych do wykonania ruchow przez wieze
        /// </summary>
        /// <returns>zwraca listę punktów na które wieza moze się przemieścić</returns>
        protected override List<Punkt> MozliweRuchy()
        {
            List<Punkt> mozliweRuchy = new List<Punkt>();
            //wieza moze poruszac sie w kazdym kierunku tylko po liniach prostych
            //przemieszczenie w gore
            for (int i = 1; i < 8; i++)
            {
                Punkt przemieszczenie = Pozycja - new Punkt(0, i);
                if (SprawdzMozliwoscWykonaniaRuchu(przemieszczenie, mozliweRuchy))
                    break;
            }
            //przemieszczenie w dol
            for (int i = 1; i < 8; i++)
            {
                Punkt przemieszczenie = Pozycja + new Punkt(0, i);
                if (SprawdzMozliwoscWykonaniaRuchu(przemieszczenie, mozliweRuchy))
                    break;
            }
            //przemieszczenie w lewo
            for (int i = 1; i < 8; i++)
            {
                Punkt przemieszczenie = Pozycja - new Punkt(i, 0);
                if (SprawdzMozliwoscWykonaniaRuchu(przemieszczenie, mozliweRuchy))
                    break;
            }
            //przemieszczenie w prawo
            for (int i = 1; i < 8; i++)
            {
                Punkt przemieszczenie = Pozycja + new Punkt(i, 0);
                if (SprawdzMozliwoscWykonaniaRuchu(przemieszczenie, mozliweRuchy))
                    break;
            }

            return mozliweRuchy;
        }
        /// <summary>
        /// Tworzy kopie Hetmana
        /// </summary>
        internal override Bierka Kopiuj(Plansza plansza)
        {
            return new Wieza(Pozycja, Kolor, plansza, PierwszyRuch, PobMozliweRuchy.ToList(), Kolejka);
        }
        /// <summary>
        /// konstruktor do kopiowania
        /// </summary>
        /// <param name="pozycja">Pozycja kopiowanej bierki</param>
        /// <param name="kolor">Kolor bierki</param>
        /// <param name="plansza">nowa plansza</param>
        /// <param name="pierwszyRuch">stan zmiennej pierwszy ruch</param>
        /// <param name="policzoneRuchy">policzone ruchy bierki</param>
        Wieza(Punkt pozycja, Strona kolor, Plansza plansza, bool pierwszyRuch, List<Punkt> policzoneRuchy, int kolejka)
        {
            this.Nazwa = Bierki.Wieża;
            this.Pozycja = pozycja;
            this.Kolor = kolor;
            this.plansza = plansza;
            this.PierwszyRuch = pierwszyRuch;
            this.WartoscPunktowa = 5;
            this.policzoneRuchy = policzoneRuchy;
            this.Kolejka = kolejka;
        }
    }
}
