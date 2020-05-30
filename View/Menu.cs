using System;
using System.Collections.Generic;

namespace Widok
{
    /// <summary>
    /// Menu główne sterowanie i widok
    /// </summary>
    public static class Menu
    {
        //część menu głównego
        /// <summary>
        /// opcje do wyboru w menu głównym
        /// </summary>
        static string[] opcjeMenu = { " Rozpocznij grę ", " Opcje ", " Wyjdz " };
        /// <summary>
        /// wybrana opcja w menu głównym
        /// </summary>
        static int wybranaGl = 0;
        /// <summary>
        /// Rysowanie menu wyboru
        /// </summary>
        static void RysujGl()
        {
            Console.Clear();
            Console.WriteLine("\n");
            for (int i = 0; i < opcjeMenu.Length; i++)
            {
                int srodek = (Console.WindowWidth - opcjeMenu[i].Length) / 2;
                if (i == wybranaGl)
                    Console.BackgroundColor = ConsoleColor.Green;

                Console.CursorLeft = srodek;
                Console.WriteLine(opcjeMenu[i]);

                Console.ResetColor();
            }
            Console.CursorLeft = 0;
            Console.CursorTop = Console.WindowHeight - 1;
            Console.WriteLine("Sterowanie szczałki");
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
        }
        /// <summary>
        /// sterowanie menu główne
        /// </summary>
        public static void Main()
        {
            RysujGl();
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        wybranaGl--;
                        wybranaGl = (wybranaGl < 0) ? opcjeMenu.Length - 1 : wybranaGl;
                        break;
                    case ConsoleKey.DownArrow:
                        wybranaGl++;
                        wybranaGl = (wybranaGl >= opcjeMenu.Length) ? 0 : wybranaGl;
                        break;
                    case ConsoleKey.Enter:
                        switch (wybranaGl)
                        {
                            case 0:
                                MainStart();
                                break;
                            case 1:
                                MainOpcje();
                                break;
                            case 2:
                                return;
                        }
                        break;
                    case ConsoleKey.Q:
                        return;
                }
                RysujGl();
            }
        }
        //część menu opcji
        /// <summary>
        /// lista możliwych opcji do wyboru
        /// </summary>
        static Opcja[] opcje = {
            new Opcja("Rozmiar pola", new List<string>{ "mały","średni","duży" }),
            new Opcja("Prezentacja bierek", new List<string>{ "symbole","litery" })
        };
        /// <summary>
        /// wybrana opcja w menu opcji
        /// </summary>
        static int wybranaOpcji = 0;
        /// <summary>
        /// rysowanie menu opcji
        /// </summary>
        static void RysujOpcje()
        {
            Console.Clear();
            Console.WriteLine("\n");
            for (int i = 0; i < opcje.Length; i++)
            {
                int srodek = Console.WindowWidth / 2 - opcje[i].NazwaOpcji.Length - 2;
                if (i == wybranaOpcji)
                    Console.BackgroundColor = ConsoleColor.Green;

                Console.CursorLeft = srodek;
                Console.WriteLine($"{opcje[i].NazwaOpcji} : {opcje[i].stan}");

                Console.ResetColor();
            }
            Console.CursorTop = Console.WindowHeight - 1;
            Console.Write("Wciśnij q aby wyjść");
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
        }
        /// <summary>
        /// sterowanie menu opcji
        /// </summary>
        static void MainOpcje()
        {
            RysujOpcje();
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        wybranaOpcji--;
                        wybranaOpcji = (wybranaOpcji < 0) ? opcje.Length - 1 : wybranaOpcji;
                        break;
                    case ConsoleKey.DownArrow:
                        wybranaOpcji++;
                        wybranaOpcji = (wybranaOpcji >= opcje.Length) ? 0 : wybranaOpcji;
                        break;
                    case ConsoleKey.Q:
                        return;
                    case ConsoleKey.LeftArrow:
                        opcje[wybranaOpcji]--;
                        break;
                    case ConsoleKey.RightArrow:
                        opcje[wybranaOpcji]++;
                        break;
                }
                RysujOpcje();
            }
        }
        //część menu rozpoczęcia gry
        /// <summary>
        /// lista opcji menu start
        /// </summary>
        static string[] opcjeStart = { "Tryb dla dwóch graczy", "Gracz vs komputer jako białe", "Gracz vs komputer jako czarne" };
        /// <summary>
        /// aktualnie wybrana opcja menu start
        /// </summary>
        static int wybranaStart = 0;
        /// <summary>
        /// rysuje menu opcji rozpoczęcia gry
        /// </summary>
        static void RysujStart()
        {
            Console.Clear();
            Console.WriteLine("\n");
            for (int i = 0; i < opcjeStart.Length; i++)
            {
                int srodek = (Console.WindowWidth - opcjeStart[i].Length) / 2;
                if (i == wybranaStart)
                    Console.BackgroundColor = ConsoleColor.Green;

                Console.CursorLeft = srodek;
                Console.WriteLine($"{opcjeStart[i]}");

                Console.ResetColor();
            }
            Console.CursorTop = Console.WindowHeight - 1;
            Console.Write("Wciśnij q aby wyjść");
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
        }
        /// <summary>
        /// sterowanie menu opcji rozpoczęcia gry
        /// </summary>
        static void MainStart()
        {
            RysujStart();
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        wybranaStart--;
                        wybranaStart = (wybranaStart < 0) ? opcjeStart.Length - 1 : wybranaStart;
                        break;
                    case ConsoleKey.DownArrow:
                        wybranaStart++;
                        wybranaStart = (wybranaStart >= opcjeStart.Length) ? 0 : wybranaStart;
                        break;
                    case ConsoleKey.Q:
                        return;
                }
                RysujStart();
            }
        }
        //część sterowania grą
    }
    /// <summary>
    /// przechowuje opcje do wyswietlenia w menuOpcji
    /// </summary>
    public class Opcja
    {
        /// <summary>
        /// aktualny status opcji
        /// </summary>
        int wybranaOpcja = 0;
        /// <summary>
        /// nazwa opcji
        /// </summary>
        public string NazwaOpcji { get; private set; }
        /// <summary>
        /// lista możliwych stanów
        /// </summary>
        List<string> listaOpcji;
        public string stan => listaOpcji[wybranaOpcja];
        /// <summary>
        /// Konstruktor opcji
        /// </summary>
        /// <param name="nazwaOpcji">nazwa opcji</param>
        /// <param name="listaOpcji">lista możliwych stanów opcji</param>
        public Opcja(string nazwaOpcji, List<string> listaOpcji)
        {
            this.NazwaOpcji = nazwaOpcji;
            this.listaOpcji = listaOpcji;
        }
        /// <summary>
        /// zmiana wybranej opcji
        /// </summary>
        /// <param name="opcja">opcja do zmiany</param>
        /// <returns>opcja po zmianie</returns>
        public static Opcja operator ++(Opcja opcja)
        {
            opcja.wybranaOpcja++;
            if (opcja.wybranaOpcja >= opcja.listaOpcji.Count)
                opcja.wybranaOpcja = 0;
            return opcja;
        }
        /// <summary>
        /// zmiana wybranej opcji
        /// </summary>
        /// <param name="opcja">opcja do zmiany</param>
        /// <returns>opcja po zmianie</returns>
        public static Opcja operator --(Opcja opcja)
        {
            opcja.wybranaOpcja--;
            if (opcja.wybranaOpcja < 0)
                opcja.wybranaOpcja = opcja.listaOpcji.Count - 1;
            return opcja;
        }
    }
}
