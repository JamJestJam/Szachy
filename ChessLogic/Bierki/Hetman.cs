using System.Collections.Generic;

namespace LogikaSzachy
{
    /// <summary>
    /// Bierka hetman
    /// </summary>
    public class Hetman : Bierka
    {
        /// <summary>
        /// tworzenie bierki hetman
        /// </summary>
        /// <param name="pozycja">pozycja startowa bierki</param>
        /// <param name="kolor">kolor bierki bialy/czarny</param>
        /// <param name="plansza">plansza na ktorej bierka sie znajduje</param>
        /// <param name="pierwszyRuch">wartosc logiczna zawierajaca informacje o tym czy bierka wykonala juz jakis ruch</param>
        public Hetman(Punkt pozycja, Strona kolor, Plansza plansza, bool pierwszyRuch = true)
        {
            this.Nazwa = Bierki.Hetman;
            this.Pozycja = pozycja;
            this.Kolor = kolor;
            this.plansza = plansza;
            this.PierwszyRuch = pierwszyRuch;
        }
        /// <summary>
        /// tworzenie listy możliwych do wykonania ruchow przez hetmana
        /// </summary>
        /// <returns>zwraca listę punktów na które hetman moze się przemieścić</returns>
        protected override List<Punkt> MozliweRuchy()
        {
            List<Punkt> mozliweRuchy = new List<Punkt>();
            //hetman moze sie poruszac w kazdym kierunku zarowno po skosie jak i po liniach prostych
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
            //poruszanie sie na skos w gore i lewo
            for (int i = 1; i < 8; i++)
            {
                Punkt przemieszczenie = Pozycja - new Punkt(i, i);
                if (SprawdzMozliwoscWykonaniaRuchu(przemieszczenie, mozliweRuchy))
                    break;
            }
            //poruszanie sie na skos w gore i prawo
            for (int i = 1; i < 8; i++)
            {
                Punkt przemieszczenie = Pozycja - new Punkt(i, -i);
                if (SprawdzMozliwoscWykonaniaRuchu(przemieszczenie, mozliweRuchy))
                    break;
            }
            //poruszanie sie na skos w dol i prawo
            for (int i = 1; i < 8; i++)
            {
                Punkt przemieszczenie = Pozycja + new Punkt(i, i);
                if (SprawdzMozliwoscWykonaniaRuchu(przemieszczenie, mozliweRuchy))
                    break;
            }
            //poruszanie sie na skos w dol i lewo
            for (int i = 1; i < 8; i++)
            {
                Punkt przemieszczenie = Pozycja + new Punkt(i, -i);
                if (SprawdzMozliwoscWykonaniaRuchu(przemieszczenie, mozliweRuchy))
                    break;
            }

            return mozliweRuchy;
        }
    }
}
