using System.Collections.Generic;

namespace LogikaSzachy
{
    /// <summary>
    /// Bierka krol
    /// </summary>
    public class Krol : Bierka
    {
        /// <summary>
        /// tworzenie bierki krol
        /// </summary>
        /// <param name="pozycja">pozycja startowa bierki</param>
        /// <param name="kolor">kolor bierki bialy/czarny</param>
        /// <param name="plansza">plansza na ktorej bierka sie znajduje</param>
        /// <param name="pierwszyRuch">wartosc logiczna zawierajaca informacje o tym czy bierka wykonala juz jakis ruch</param>
        public Krol(Punkt pozycja, Strona kolor, Plansza plansza, bool pierwszyRuch = true)
        {
            this.Nazwa = Bierki.Krol;
            this.Pozycja = pozycja;
            this.Kolor = kolor;
            this.plansza = plansza;
            this.PierwszyRuch = pierwszyRuch;
            this.wartoscPunktowa = int.MaxValue;
        }
        /// <summary>
        /// tworzenie listy możliwych do wykonania ruchow przez krola
        /// </summary>
        /// <returns>zwraca listę punktów na które krol moze się przemieścić</returns>
        protected override List<Punkt> MozliweRuchy()
        {
            List<Punkt> mozliweRuchy = new List<Punkt>();
            //krol moze przemieszczac sie w kazdym kierunku o jedno pole
            //w gore
            SprawdzMozliwoscWykonaniaRuchu(Pozycja - new Punkt(0, 1), mozliweRuchy);
            //w gore lewo
            SprawdzMozliwoscWykonaniaRuchu(Pozycja - new Punkt(1, 1), mozliweRuchy);
            //w lewo
            SprawdzMozliwoscWykonaniaRuchu(Pozycja - new Punkt(1, 0), mozliweRuchy);
            //w lewo dol
            SprawdzMozliwoscWykonaniaRuchu(Pozycja - new Punkt(1, -1), mozliweRuchy);
            //w dol
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(0, 1), mozliweRuchy);
            //w dol i prawo
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(1, 1), mozliweRuchy);
            //w prawo
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(1, 0), mozliweRuchy);
            //w prawo i gore
            SprawdzMozliwoscWykonaniaRuchu(Pozycja + new Punkt(1, -1), mozliweRuchy);

            //krol moze rowniez wykonac roszady
            //to do
            return mozliweRuchy;
        }
    }
}
