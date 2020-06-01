using System.Collections.Generic;

namespace LogikaSzachy
{
    /// <summary>
    /// Bierka pionek
    /// </summary>
    class Pionek : Bierka
    {
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
        protected override List<Punkt> możliweRuchy()
        {
            throw new System.NotImplementedException();
        }
    }
}
