using System;
using System.Collections.Generic;
using System.Linq;

namespace LogikaSzachy
{
    /// <summary>
    /// Bierka pionek
    /// </summary>
    public class Pionek : Bierka
    {
        /// <summary>
        /// zwraca kierunek na osi Y w ktorym podazaja pionki
        /// </summary>
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
            this.WartoscPunktowa = 1;
        }
        /// <summary>
        /// tworzenie listy możliwych do wykonania ruchow przez pionka
        /// </summary>
        /// <returns>zwraca listę punktów na które pionek moze się przemieścić</returns>
        protected override List<Punkt> MozliweRuchy()
        {
            List<Punkt> mozliweRuchy = new List<Punkt>();
            //pinek ma możliwość przemieszczenia się o jedno pole na przod
            if(Kolor == plansza.StronaGrajaca)
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

            //zbicie w przelocie
            //prawo
            zbicie = new Punkt(1, 0) + Pozycja;
            if (plansza.BierkaNaPozycji(zbicie, out bierka))
                if (bierka.Kolor != Kolor)
                    if (bierka.Nazwa == Bierki.Pionek)
                        if (plansza.WykonaneRuchy.Last().Item2 == zbicie && plansza.WykonaneRuchy.Last().Item1 == new Punkt(1, 2 * Strona) + Pozycja)
                            mozliweRuchy.Add(new Punkt(1, Strona) + Pozycja);
            //lewo
            zbicie = new Punkt(-1, 0) + Pozycja;
            if (plansza.BierkaNaPozycji(zbicie, out bierka))
                if (bierka.Kolor != Kolor)
                    if (bierka.Nazwa == Bierki.Pionek)
                        if (plansza.WykonaneRuchy.Last().Item2 == zbicie && plansza.WykonaneRuchy.Last().Item1 == new Punkt(-1, 2 * Strona) + Pozycja)
                            mozliweRuchy.Add(new Punkt(-1, Strona) + Pozycja);


            return mozliweRuchy;
        }
        /// <summary>
        /// prubuje przemiescic bierke na wskazana pozycje
        /// </summary>
        /// <param name="przemieszczenie">pozycja na ktora ma sie przemiescic bierka</param>
        /// <returns>zwraca prawda jezeli udalo sie przemiescic bierke</returns>
        public override bool WykonajRuch(Punkt przemieszczenie)
        {
            //sprawdz czy przemieszczenie znajduje sie na liscie mozliwych ruchow
            if (PobMozliweRuchy.Contains(przemieszczenie))
            {
                if (przemieszczenie - Pozycja == new Punkt(-1, Strona) || przemieszczenie - Pozycja == new Punkt(1, Strona))
                    if (!plansza.BierkaNaPozycji(przemieszczenie, out _))
                        plansza.ZbijBierke(przemieszczenie - new Punkt(0, Strona), (Kolor == LogikaSzachy.Strona.Biała) ? LogikaSzachy.Strona.Czarna : LogikaSzachy.Strona.Biała);
                PierwszyRuch = false;
                Pozycja = przemieszczenie;

                if (Pozycja.Y == 7 || Pozycja.Y == 0)
                    plansza.PromocjaPionka(this);
                return true;
            }
            return false;
        }
    }
}
