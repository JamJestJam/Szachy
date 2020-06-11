using System.Collections.Generic;

namespace LogikaSzachy
{
    /// <summary>
    /// Bierka skoczek
    /// </summary>
    public class Skoczek : Bierka
    {
        /// <summary>
        /// tworzenie bierki skoczek
        /// </summary>
        /// <param name="pozycja">pozycja startowa bierki</param>
        /// <param name="kolor">kolor bierki bialy/czarny</param>
        /// <param name="plansza">plansza na ktorej bierka sie znajduje</param>
        /// <param name="pierwszyRuch">wartosc logiczna zawierajaca informacje o tym czy bierka wykonala juz jakis ruch</param>
        public Skoczek(Punkt pozycja, Strona kolor, Plansza plansza, bool pierwszyRuch = true)
        {
            this.Nazwa = Bierki.Skoczek;
            this.Pozycja = pozycja;
            this.Kolor = kolor;
            this.plansza = plansza;
            this.PierwszyRuch = pierwszyRuch;
            this.wartoscPunktowa = 3;
        }
        /// <summary>
        /// tworzenie listy możliwych do wykonania ruchow przez skoczka
        /// </summary>
        /// <returns>zwraca listę punktów na które skoczek moze się przemieścić</returns>
        protected override List<Punkt> MozliweRuchy()
        {
            List<Punkt> mozliweRuchy = new List<Punkt>();
            //skoczek moze przemieszczac sie w kazdym kierunku o dwa pola w jednej osi i jedno pole w drugiej osi
            //dol prawo
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(1, 2), mozliweRuchy);
            //dol lewo
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(-1, 2), mozliweRuchy);
            //lewo dol
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(-2, 1), mozliweRuchy);
            //lewo gora
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(-2, -1), mozliweRuchy);
            //gora lewo
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(-1, -2), mozliweRuchy);
            //gora prawo
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(1, -2), mozliweRuchy);
            //prawo gora
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(2, -1), mozliweRuchy);
            //prawo dol
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(2, 1), mozliweRuchy);

            return mozliweRuchy;
        }
    }
}
