﻿using LogikaSzachy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Widok
{
    /// <summary>
    /// sterowanie i widok
    /// </summary>
    public static partial class Menu
    {
        //część menu głównego
        /// <summary>
        /// opcje do wyboru w menu głównym
        /// </summary>
        static readonly string[] opcjeMenu = { " Rozpocznij grę ", " Opcje ", " Wyjdz " };
        /// <summary>
        /// wybrana opcja w menu głównym
        /// </summary>
        static int wybranaGl = 0;
        /// <summary>
        /// Rysowanie menu wyboru
        /// </summary>
        static void RysujGl()
        {
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
            Console.OutputEncoding = Encoding.UTF8;
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
                                Console.Clear();
                                MainStart();
                                break;
                            case 1:
                                Console.Clear();
                                MainOpcje();
                                break;
                            case 2:
                                Console.Clear();
                                return;
                        }
                        break;
                    case ConsoleKey.Q:
                        Console.Clear();
                        return;
                }
                RysujGl();
            }
        }
        //część menu opcji
        /// <summary>
        /// lista możliwych opcji do wyboru
        /// </summary>
        static readonly Opcja[] opcje = {
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
            Console.WriteLine("\n");
            for (int i = 0; i < opcje.Length; i++)
            {
                int srodek = Console.WindowWidth / 2 - opcje[i].NazwaOpcji.Length - 2;
                if (i == wybranaOpcji)
                    Console.BackgroundColor = ConsoleColor.Green;

                Console.CursorLeft = srodek;
                Console.WriteLine($"{opcje[i].NazwaOpcji} : {opcje[i].Stan}");

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
                        Console.Clear();
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
        static readonly string[] opcjeStart = { "Tryb dla dwóch graczy", "Gracz vs komputer jako białe", "Gracz vs komputer jako czarne" };
        /// <summary>
        /// aktualnie wybrana opcja menu start
        /// </summary>
        static int wybranaStart = 0;
        /// <summary>
        /// rysuje menu opcji rozpoczęcia gry
        /// </summary>
        static void RysujStart()
        {
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
                    case ConsoleKey.Enter:
                        switch (wybranaStart)
                        {
                            case 0:
                                MainGry(Strona.Biała, false);
                                break;
                            case 1:
                                MainGry(Strona.Czarna);
                                break;
                            case 2:
                                MainGry(Strona.Biała);
                                break;
                        }
                        break;
                    case ConsoleKey.Q:
                        Console.Clear();
                        return;
                }
                RysujStart();
            }
        }
        //czesc dotycząca rysowania i sterowania grą
        /// <summary>
        /// Rysuje plansze do gry
        /// </summary>
        static void RysujPlansze()
        {
            //wysokosc
            for (int i = 0; i < PlanszaWielkosc; i++)
            {
                //szerokosc
                for (int j = 0; j < PlanszaWielkosc; j++)
                {
                    //pojedyncze pole
                    RysujPole(new Punkt(j, i));
                }
            }
        }
        /// <summary>
        /// rysuje pojedyncze pole
        /// </summary>
        static void RysujPole(Punkt punktDoRysowania)
        {
            //obliczenie odstepu na osi x
            int margines = (Console.WindowWidth - (PoleSzerokosc * PlanszaWielkosc)) / 2;
            //ustalenie koloru tła
            if (punktDoRysowania == kursorPozycja)
                Console.BackgroundColor = ConsoleColor.Blue;
            else if (punktDoRysowania == zaznaczenie)
                Console.BackgroundColor = ConsoleColor.DarkYellow;
            else if (mozliweRuchy.Contains(punktDoRysowania))
                Console.BackgroundColor = ConsoleColor.Red;
            else if (punktDoRysowania.XplusY % 2 == 0)
                Console.BackgroundColor = ConsoleColor.Green;
            else
                Console.BackgroundColor = ConsoleColor.White;
            //wysokosc pola
            for (int i = 0; i < PoleWysokosc; i++)
            {
                Console.CursorTop = i + (punktDoRysowania.Y * PoleWysokosc) + 2;
                Console.CursorLeft = margines + (punktDoRysowania.X * PoleSzerokosc);
                //szerokosc pola
                for (int j = 0; j < PoleSzerokosc; j++)
                {
                    //sprawdzenie czy na danej pozycje istnieje bierka
                    if (i == PoleWysokosc / 2 && j == PolowaSzerokosci && plansza.BierkaNaPozycji(punktDoRysowania, out Bierka bierka))
                    {
                        //jeżeli bierka istnieje to ustaw jej kolor
                        if (bierka.Kolor == Strona.Biała)
                            Console.ForegroundColor = ConsoleColor.Gray;
                        else
                            Console.ForegroundColor = ConsoleColor.Black;
                        //narysuj bierke
                        Console.Write(PrezentacjaBierki(bierka.Nazwa));
                    }
                    else
                        Console.Write(' ');
                }
            }
            Console.ResetColor();
        }
        /// <summary>
        /// sterowanie grą
        /// </summary>
        static void MainGry(Strona side, bool bot = true)
        {
            //inicjalizuje plansze
            plansza = new Plansza(PromocjaPionka, KoniecGry);
            gra = true;
            KomputerowyPrzeciwnik komputerowyPrzeciwnik = new KomputerowyPrzeciwnik(plansza, 1);
            //rysowanie
            Console.Clear();
            RysujPlansze();
            //działanie
            while (gra)
            {
                if (bot && side == plansza.StronaGrajaca)
                {
                    komputerowyPrzeciwnik.WykonajRuch();
                    RysujPlansze();
                }
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.UpArrow:
                        {
                            Punkt staraPozycja = kursorPozycja;
                            kursorPozycja = new Punkt(staraPozycja.X, staraPozycja.Y - 1);
                            if (!kursorPozycja.Pomiedzy(7))
                                kursorPozycja = staraPozycja;
                            RysujPole(staraPozycja);
                            RysujPole(kursorPozycja);
                            break;
                        }
                    case ConsoleKey.DownArrow:
                        {
                            Punkt staraPozycja = kursorPozycja;
                            kursorPozycja = new Punkt(staraPozycja.X, staraPozycja.Y + 1);
                            if (!kursorPozycja.Pomiedzy(7))
                                kursorPozycja = staraPozycja;
                            RysujPole(staraPozycja);
                            RysujPole(kursorPozycja);
                            break;
                        }
                    case ConsoleKey.LeftArrow:
                        {
                            Punkt staraPozycja = kursorPozycja;
                            kursorPozycja = new Punkt(staraPozycja.X - 1, staraPozycja.Y);
                            if (!kursorPozycja.Pomiedzy(7))
                                kursorPozycja = staraPozycja;
                            RysujPole(staraPozycja);
                            RysujPole(kursorPozycja);
                            break;
                        }
                    case ConsoleKey.RightArrow:
                        {
                            Punkt staraPozycja = kursorPozycja;
                            kursorPozycja = new Punkt(staraPozycja.X + 1, staraPozycja.Y);
                            if (!kursorPozycja.Pomiedzy(7))
                                kursorPozycja = staraPozycja;
                            RysujPole(staraPozycja);
                            RysujPole(kursorPozycja);
                            break;
                        }
                    case ConsoleKey.Enter:
                        if (mozliweRuchy.Contains(kursorPozycja))
                        {
                            plansza.SprobujWykonacRuch(zaznaczenie, kursorPozycja);
                            zaznaczenie = null;
                            mozliweRuchy = new List<Punkt>();
                            if (gra)
                            {
                                RysujPlansze();
                            }
                        }
                        else if (plansza.BierkaNaPozycji(kursorPozycja, out Bierka bierka))
                        {
                            if (bierka.Kolor == plansza.StronaGrajaca)
                            {
                                zaznaczenie = kursorPozycja;
                                mozliweRuchy = bierka.PobMozliweRuchy;
                                RysujPlansze();
                            }
                        }
                        break;
                    case ConsoleKey.Q:
                        Console.Clear();
                        return;
                }
                Console.CursorLeft = 0;
                Console.CursorTop = 0;
            }
            Console.Clear();
        }
        /// <summary>
        /// plansza na której się gra
        /// </summary>
        static Plansza plansza;
        /// <summary>
        /// jeżeli gra == true gra jest w trakcie
        /// </summary>
        static bool gra;
        static bool PrezentacjaGryZaPomocaSyboli
        {
            get
            {
                return opcje[1].Stan switch
                {
                    "litery" => false,
                    _ => true,
                };
            }
        }
        /// <summary>
        /// Zwraca szrokosc pojedynczego pola dla wybranej opcji
        /// </summary>
        static int PoleSzerokosc
        {
            get
            {
                switch (opcje[0].Stan)
                {
                    case "mały":
                        if (PrezentacjaGryZaPomocaSyboli)
                            return 2;
                        return 1;
                    case "średni":
                        if (PrezentacjaGryZaPomocaSyboli)
                            return 4;
                        return 3;
                    default:
                        if (PrezentacjaGryZaPomocaSyboli)
                            return 6;
                        return 5;
                }
            }
        }
        /// <summary>
        /// Zwraca szrokosc wysokość pola dla wybranej opcji
        /// </summary>
        static int PoleWysokosc
        {
            get
            {
                return opcje[0].Stan switch
                {
                    "mały" => 1,
                    "średni" => 3,
                    _ => 5,
                };
            }
        }
        /// <summary>
        /// ilosc pol na planszy w jednej osi
        /// </summary>
        const int PlanszaWielkosc = 8;
        /// <summary>
        /// zwraca polowe szerokosci pola dla wybranej metody prezencji bierek
        /// </summary>
        static int PolowaSzerokosci
        {
            get
            {
                if (opcje[1].Stan == "symbole")
                {
                    return PoleSzerokosc / 2 - 1;
                }
                else
                {
                    return PoleSzerokosc / 2;
                }
            }
        }
        /// <summary>
        /// aktyualna pozycja kursora na planszy
        /// </summary>
        static Punkt kursorPozycja = new Punkt(4, 6);
        /// <summary>
        /// lista możliwych ruchów do wykonania przez zaznaczoną bierke
        /// </summary>
        static IReadOnlyList<Punkt> mozliweRuchy = new List<Punkt>();
        /// <summary>
        /// aktualnie zaznaczona bierka;
        /// </summary>
        static Punkt zaznaczenie = new Punkt(-1, -1);
        /// <summary>
        /// zwraca zapisany w formie stringa wyglad bierki
        /// </summary>
        /// <param name="bierka">rodzaj bierki do wyswietlenia</param>
        /// <returns>string z bierka</returns>
        static string PrezentacjaBierki(Bierki bierka)
        {
            if (opcje[1].Stan == "symbole")
            {
                return bierkiPrezentacjaSymbole[bierka];
            }
            else
            {
                return bierkiPrezentacjaLitery[bierka];
            }
        }
        /// <summary>
        /// prezentacja bierek za pomocą liter
        /// </summary>
        static readonly Dictionary<Bierki, string> bierkiPrezentacjaLitery = new Dictionary<Bierki, string>
        {
            { Bierki.Hetman, "H"},
            { Bierki.Wieża, "W"},
            { Bierki.Goniec, "G"},
            { Bierki.Skoczek, "S"},
            { Bierki.Krol, "K" },
            { Bierki.Pionek, "P"}
        };
        /// <summary>
        /// prezentacja bierek za pomocą symboli
        /// </summary>
        static readonly Dictionary<Bierki, string> bierkiPrezentacjaSymbole = new Dictionary<Bierki, string>
        {

            { Bierki.Hetman, "♛"},
            { Bierki.Wieża, "♜"},
            { Bierki.Goniec, "♝"},
            { Bierki.Skoczek, "♞"},
            { Bierki.Krol, "♚" },
            { Bierki.Pionek, "♟"}
        };
        //czesc dotyczaca wydarzeń w grze
        /// <summary>
        /// obecnie wybrana opcja promocji
        /// </summary>
        static int zaznaczeniePromocji = 0;
        /// <summary>
        /// rysowanie pola do wyboru bierki
        /// </summary>
        static void RysujPolaDoPromocjiPionka()
        {
            //obliczenie odstepu na osi x
            int margines = (Console.WindowWidth - (PoleSzerokosc * 4)) / 2;
            //bierki
            for (int l = 0; l < 4; l++)
            {
                if (zaznaczeniePromocji == l)
                    Console.BackgroundColor = ConsoleColor.Red;
                else if (l % 2 == 0)
                    Console.BackgroundColor = ConsoleColor.Green;
                else
                    Console.BackgroundColor = ConsoleColor.White;
                Console.CursorTop = 2;
                //wysokosc
                for (int i = 0; i < PoleWysokosc; i++)
                {
                    Console.CursorLeft = margines + (l * PoleSzerokosc);
                    //szerokosc
                    for (int j = 0; j < PoleSzerokosc; j++)
                    {
                        if (i == PoleWysokosc / 2 && j == PolowaSzerokosci)
                            switch (l)
                            {
                                case 0:
                                    Console.Write(PrezentacjaBierki(Bierki.Hetman));
                                    break;
                                case 1:
                                    Console.Write(PrezentacjaBierki(Bierki.Goniec));
                                    break;
                                case 2:
                                    Console.Write(PrezentacjaBierki(Bierki.Skoczek));
                                    break;
                                case 3:
                                    Console.Write(PrezentacjaBierki(Bierki.Wieża));
                                    break;
                            }
                        else
                            Console.Write(" ");
                    }
                    Console.WriteLine();
                }
            }
            Console.ResetColor();
        }
        /// <summary>
        /// funkcja do wyboru bierki
        /// </summary>
        /// <returns>zwraca rodzaj bierki do promocji</returns>
        static Bierki PromocjaPionka()
        {
            Console.Clear();
            RysujPolaDoPromocjiPionka();
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        zaznaczeniePromocji = (zaznaczeniePromocji < 1) ? 3 : zaznaczeniePromocji - 1;
                        RysujPolaDoPromocjiPionka();
                        break;
                    case ConsoleKey.RightArrow:
                        zaznaczeniePromocji = (zaznaczeniePromocji > 2) ? 0 : zaznaczeniePromocji + 1;
                        RysujPolaDoPromocjiPionka();
                        break;
                    case ConsoleKey.Enter:
                        switch (zaznaczeniePromocji)
                        {
                            case 0:
                                return Bierki.Hetman;
                            case 1:
                                return Bierki.Goniec;
                            case 2:
                                return Bierki.Skoczek;
                            case 3:
                                return Bierki.Wieża;
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// funkcja odpalana na koniec gry
        /// </summary>
        /// <param name="statusGry">stan gry</param>
        static void KoniecGry(Plansza.Status statusGry)
        {
            gra = false;
            Console.Clear();
            if (statusGry == Plansza.Status.Pat)
                Console.WriteLine("Gra zakoncoczna remisem");
            else if (statusGry == Plansza.Status.Mat)
            {
                Console.Write("Wygrała storna");
                if (plansza.StronaGrajaca == Strona.Biała)
                    Console.WriteLine(" Czarna");
                else
                    Console.WriteLine(" Biała");
            }
            Console.ReadLine();
            Console.Clear();
        }
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
        readonly List<string> listaOpcji;
        public string Stan => listaOpcji[wybranaOpcja];
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
