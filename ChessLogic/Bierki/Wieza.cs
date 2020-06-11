using System.Collections.Generic;

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
            this.wartoscPunktowa = 5;
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
    }
}
