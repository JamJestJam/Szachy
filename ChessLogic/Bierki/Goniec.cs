using System.Collections.Generic;

namespace LogikaSzachy
{
    /// <summary>
    /// Bierka goniec
    /// </summary>
    class Goniec : Bierka
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
        }
        /// <summary>
        /// tworzenie listy możliwych do wykonania ruchow przez goncia
        /// </summary>
        /// <returns>zwraca listę punktów na które goniec moze się przemieścić</returns>
        protected override List<Punkt> możliweRuchy()
        {
            throw new System.NotImplementedException();
        }
    }
}
