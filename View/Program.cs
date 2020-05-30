using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading;
using Widok;

namespace View
{
    /// <summary>
    /// Sterowanie grą i jej rysowanie
    /// </summary>
    class Start
    {
        //czesc dotyczaca planszy
        /// <summary>
        /// rozmiar planszy
        /// </summary>
        public const int rozmiarPlanszy = 8;
        /// <summary>
        /// Szerokość rysowanego pola
        /// </summary>
        public static int poleWysokosc = 3;
        /// <summary>
        /// Wysokość rysowanego pola
        /// </summary>
        public static int poleSzerokosc = 5;
        /// <summary>
        /// Funkcja rysująca planszę
        /// </summary>
        public static void RysujPlansze()
        {
            //wysokość
            for (int i = 0; i < rozmiarPlanszy; i++)
            {
                //szerokość
                for (int j = 0; j < rozmiarPlanszy; j++)
                {
                    RysujPole(j, i);
                }
            }
        }
        /// <summary>
        /// Funkcja rysująca pojedyncze pole na planszy
        /// </summary>
        /// <param name="x">współrzędna X pola z zakresu od 0 do 7</param>
        /// <param name="y">współrzędna Y pola z zakresu od 0 do 7</param>
        public static void RysujPole(int x, int y)
        {
            //ustaw kolor pola
            if ((x + y) % 2 == 0)
                Console.BackgroundColor = ConsoleColor.White;
            else
                Console.BackgroundColor = ConsoleColor.Green;
            //wysokość pola
            for (int i = 0; i < poleWysokosc; i++)
            {
                //ustaw pozycje kursora
                Console.CursorLeft = poleSzerokosc * x + (Console.WindowWidth / 2 - (poleSzerokosc * rozmiarPlanszy / 2));
                Console.CursorTop = poleWysokosc * y + i;
                //szerokosc pola
                for (int j = 0; j < poleSzerokosc; j++)
                {
                    //rysuj pole
                    Console.Write(' ');
                }
            }
            //resetuj kolory do domyślnych
            Console.ResetColor();
        }
    }
}
