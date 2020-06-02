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
        }
        /// <summary>
        /// tworzenie listy możliwych do wykonania ruchow przez skoczka
        /// </summary>
        /// <returns>zwraca listę punktów na które skoczek moze się przemieścić</returns>
        protected override List<Punkt> MozliweRuchy()
        {
            throw new System.NotImplementedException();
        }
    }
}
