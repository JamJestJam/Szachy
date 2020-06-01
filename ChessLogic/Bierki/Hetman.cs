using System.Collections.Generic;

namespace LogikaSzachy
{
    /// <summary>
    /// Bierka hetman
    /// </summary>
    class Hetman : Bierka
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
        protected override List<Punkt> możliweRuchy()
        {
            throw new System.NotImplementedException();
        }
    }
}
