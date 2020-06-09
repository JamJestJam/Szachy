using System.Collections.Generic;

namespace LogikaSzachy
{
    /// <summary>
    /// Bierka pionek
    /// </summary>
    public class Pionek : Bierka
    {
        int Strona { get { return (Kolor == LogikaSzachy.Strona.Biała) ? -1 : 1; } }

        /// <summary>
        /// tworzenie bierki pionek
        /// </summary>
        /// <param name="pozycja">pozycja startowa bierki</param>
        /// <param name="kolor">kolor bierki bialy/czarny</param>
        /// <param name="plansza">plansza na ktorej bierka sie znajduje</param>
        /// <param name="pierwszyRuch">wartosc logiczna zawierajaca informacje o tym czy bierka wykonala juz jakis ruch</param>
        public Pionek(Punkt pozycja, Strona kolor, Plansza plansza, bool pierwszyRuch = true)
        {
            this.Nazwa = Bierki.Pionek;
            this.Pozycja = pozycja;
            this.Kolor = kolor;
            this.plansza = plansza;
            this.PierwszyRuch = pierwszyRuch;
        }

        /// <summary>
        /// tworzenie listy możliwych do wykonania ruchow przez pionka
        /// </summary>
        /// <returns>zwraca listę punktów na które pionek moze się przemieścić</returns>
        protected override List<Punkt> MozliweRuchy()
        {
            List<Punkt> mozliweRuchy = new List<Punkt>();
            //pinek ma możliwość przemieszczenia się o jedno pole na przod
            if (!SprawdzMozliwoscWykonaniaRuchu(new Punkt(0, Strona) + Pozycja, mozliweRuchy, false))
                //na dwa pola na przod jezeli jest nie wykonal jeszcze zadnego ruchu
                if (PierwszyRuch)
                    SprawdzMozliwoscWykonaniaRuchu(new Punkt(0, 2 * Strona) + Pozycja, mozliweRuchy, false);


            //zbijac na boki
            //w lewo
            Punkt zbicie = new Punkt(-1, Strona) + Pozycja;
            if (plansza.BierkaNaPozycji(zbicie, out Bierka bierka))
                if (bierka.Kolor != Kolor)
                    mozliweRuchy.Add(zbicie);
            // w prawo
            zbicie = new Punkt(1, Strona) + Pozycja;
            if (plansza.BierkaNaPozycji(zbicie, out bierka))
                if (bierka.Kolor != Kolor)
                    mozliweRuchy.Add(zbicie);

            return mozliweRuchy;
        }
    }
}
