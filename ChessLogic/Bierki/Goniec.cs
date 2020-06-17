using System;
using System.Collections.Generic;
using System.Linq;

namespace LogikaSzachy
{
    /// <summary>
    /// Bierka goniec
    /// </summary>
    public class Goniec : Bierka
    {
        /// <summary>
        /// tworzenie bierki goniec
        /// </summary>
        /// <param name="pozycja">pozycja startowa bierki</param>
        /// <param name="kolor">kolor bierki bialy/czarny</param>
        /// <param name="plansza">plansza na ktorej bierka sie znajduje</param>
        /// <param name="pierwszyRuch">wartosc logiczna zawierajaca informacje o tym czy bierka wykonala juz jakis ruch</param>
        public Goniec(Punkt pozycja, Strona kolor, Plansza plansza, bool pierwszyRuch = true)
        {
            this.Nazwa = Bierki.Goniec;
            this.Pozycja = pozycja;
            this.Kolor = kolor;
            this.plansza = plansza;
            this.PierwszyRuch = pierwszyRuch;
            this.WartoscPunktowa = 3;
        }
        /// <summary>
        /// tworzenie listy możliwych do wykonania ruchow przez goncia
        /// </summary>
        /// <returns>zwraca listę punktów na które goniec moze się przemieścić</returns>
        protected override List<Punkt> MozliweRuchy()
        {
            List<Punkt> mozliweRuchy = new List<Punkt>();
            //goniec ma mozliwosc poruszania sie na skosy do konca planszy lub do napotkania na inna bierke
            //poruszanie sie na skos w gore i lewo
            for (int i = 1; i < 8; i++)
            {
                Punkt przemieszczenie = Pozycja - new Punkt(i, i);
                if(SprawdzMozliwoscWykonaniaRuchu(przemieszczenie, mozliweRuchy))
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
        /// <summary>
        /// Tworzy kopie Gonca
        /// </summary>
        internal override Bierka Kopiuj(Plansza plansza)
        {
            return new Goniec(Pozycja, Kolor, plansza, PierwszyRuch, PobMozliweRuchy.ToList(), Kolejka);
        }
        /// <summary>
        /// konstruktor do kopiowania
        /// </summary>
        /// <param name="pozycja">Pozycja kopiowanej bierki</param>
        /// <param name="kolor">Kolor bierki</param>
        /// <param name="plansza">nowa plansza</param>
        /// <param name="pierwszyRuch">stan zmiennej pierwszy ruch</param>
        /// <param name="policzoneRuchy">policzone ruchy bierki</param>
        Goniec(Punkt pozycja, Strona kolor, Plansza plansza, bool pierwszyRuch, List<Punkt> policzoneRuchy, int kolejka)
        {
            this.Nazwa = Bierki.Goniec;
            this.Pozycja = pozycja;
            this.Kolor = kolor;
            this.plansza = plansza;
            this.PierwszyRuch = pierwszyRuch;
            this.WartoscPunktowa = 3;
            this.policzoneRuchy = policzoneRuchy;
            this.Kolejka = kolejka;
        }
    }
}
