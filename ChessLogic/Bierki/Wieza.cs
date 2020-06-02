using System.Collections.Generic;

namespace LogikaSzachy
{
    /// <summary>
    /// Bierka wieza
    /// </summary>
    class Wieza : Bierka
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
        }
        /// <summary>
        /// tworzenie listy możliwych do wykonania ruchow przez wieze
        /// </summary>
        /// <returns>zwraca listę punktów na które wieza moze się przemieścić</returns>
        protected override List<Punkt> MozliweRuchy()
        {
            throw new System.NotImplementedException();
        }
    }
}
