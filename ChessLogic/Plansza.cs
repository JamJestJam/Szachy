using System;
using System.Collections.Generic;
using System.Linq;

namespace LogikaSzachy
{
    /// <summary>
    /// Strony na planszy
    /// </summary>
    public enum Strona
    {
        Biała,
        Czarna
    }
    /// <summary>
    /// Klasa symulująca planszę do gry w szachy
    /// </summary>
    public class Plansza
    {
        /// <summary>
        /// numer ruchu w którym powstał przelom
        /// </summary>
        int zmianaStatusu = -1;
        /// <summary>
        /// numer ruchu w ktorym zostala stworzona lista ruchow przeciwnika
        /// </summary>
        int aktualizacjaRuchow = -1;
        /// <summary>
        /// Lusta ruchow przeciwnika w turze "aktualizacjaRuchow"
        /// </summary>
        List<Punkt> listaRuchowPrzeciwnika = new List<Punkt>();
        internal List<Punkt> ListaRuchowPrzeciwnika { get
            {
                if(aktualizacjaRuchow!=Ruchy)
                {
                    List<Bierka> bierki = (StronaGrajaca == Strona.Biała) ? BierkiCzarne : BierkiBiale;
                    List<Punkt> wynik = new List<Punkt>();
                    foreach (Bierka bierka in bierki)
                        foreach (Punkt punkt in bierka.PobMozliweRuchy)
                            wynik.Add(punkt);

                    listaRuchowPrzeciwnika = wynik.Distinct().ToList();
                }
                return listaRuchowPrzeciwnika;
            } }
        /// <summary>
        /// Lista ruchow mozliwych do wykonania przez bierki
        /// Jezeli lista jest null - brak ograniczen w wykonywaniu ruchow
        /// Jezeli lista jest pusta - brak mozliwosci wykonania ruchu, tylko krol moze sie ruszyc
        /// </summary>
        internal List<Punkt> zaslonieceiSzacha = null;
        /// <summary>
        /// lista ruchow wykonanych na planszy
        /// </summary>
        private readonly List<Tuple<Punkt, Punkt>> wykonaneRucy = new List<Tuple<Punkt, Punkt>>();
        /// <summary>
        /// lista wykonanych ruchow na planszy
        /// </summary>
        public IReadOnlyList<Tuple<Punkt, Punkt>> WykonaneRuchy { get => wykonaneRucy.AsReadOnly(); }
        /// <summary>
        /// ilosc wykonanych ruchow
        /// </summary>
        public int Ruchy { get => wykonaneRucy.Count; }
        /// <summary>
        /// Lista możliwych stanów gry
        /// </summary>
        public enum Status
        {
            Gra,
            Mat,
            Pat
        }
        /// <summary>
        /// Aktualny stan gry
        /// </summary>
        public Status StanGry { get; private set; }
        /// <summary>
        /// Lista wszystkich bierek na planszy
        /// </summary>
        internal readonly List<Bierka> bierki = new List<Bierka>();
        /// <summary>
        /// lista bialych bierek
        /// </summary>
        List<Bierka> BierkiBiale { get => bierki.FindAll(x => x.Kolor == Strona.Biała); }
        /// <summary>
        /// lista czarnych bierek
        /// </summary>
        List<Bierka> BierkiCzarne { get => bierki.FindAll(x => x.Kolor == Strona.Czarna); }
        /// <summary>
        /// bialy krol
        /// </summary>
        Krol KrolBialy { get => (Krol)bierki.Find(x => x.Kolor == Strona.Biała && x.Nazwa == Bierki.Krol); }
        /// <summary>
        /// czarny krol
        /// </summary>
        Krol KrolCzarny { get => (Krol)bierki.Find(x => x.Kolor == Strona.Czarna && x.Nazwa == Bierki.Krol); }
        public Strona StronaGrajaca { get; private set; }
        /// <summary>
        /// Funkcja wołana w momencie promocjii pionka
        /// </summary>
        readonly Func<Bierki> promocjaPionka;
        /// <summary>
        /// Funkcja wołana w momencie zakończenia gry
        /// </summary>
        readonly Action<Status> koniecGry;
        /// <summary>
        /// krol grajacy
        /// </summary>
        Krol KrolGrajacy { get => (StronaGrajaca == Strona.Biała) ? KrolBialy : KrolCzarny; }
        /// <summary>
        /// lista bierek aktualnie grajacych
        /// </summary>
        List<Bierka> BierkiGrajace { get => (StronaGrajaca == Strona.Biała) ? BierkiBiale : BierkiCzarne; }
        /// <summary>
        /// Konstruktor tworzący planszę do gry
        /// </summary>
        /// <param name="promocjaPionka">funkcja wywolywana w momencie promocji pionka zwracajaca figure na ktora pionek ma sie zamienic</param>
        /// <param name="koniecGry">funkcja wywolywana na koniec gry</param>
        public Plansza(Func<Bierki> promocjaPionka, Action<Status> koniecGry)
        {
            this.promocjaPionka = promocjaPionka;
            this.koniecGry = koniecGry;

            Init();
        }
        /// <summary>
        /// Konstruktor tworzący planszę do gry
        /// </summary>
        /// <param name="promocjaPionka">funkcja wywolywana w momencie promocji pionka zwracajaca figure na ktora pionek ma sie zamienic</param>
        /// <param name="koniecGry">funkcja wywolywana na koniec gry</param>
        /// <param name="listaBierek">nieregularne ulozenie startowe bierek na planszy</param>
        public Plansza(Func<Bierki> promocjaPionka, Action<Status> koniecGry, List<Bierka> listaBierek)
        {
            this.promocjaPionka = promocjaPionka;
            this.koniecGry = koniecGry;

            bierki = listaBierek;
        }
        /// <summary>
        /// Tworzenie podstawowej planszy
        /// </summary>
        void Init()
        {
            //pionki
            for (int i = 0; i < 8; i++)
                bierki.Add(new Pionek(new Punkt(i, 1), Strona.Czarna, this));
            for (int i = 0; i < 8; i++)
                bierki.Add(new Pionek(new Punkt(i, 6), Strona.Biała, this));
            //figury czarne
            bierki.Add(new Wieza(new Punkt(0, 0), Strona.Czarna, this));
            bierki.Add(new Wieza(new Punkt(7, 0), Strona.Czarna, this));
            bierki.Add(new Goniec(new Punkt(2, 0), Strona.Czarna, this));
            bierki.Add(new Goniec(new Punkt(5, 0), Strona.Czarna, this));
            bierki.Add(new Skoczek(new Punkt(1, 0), Strona.Czarna, this));
            bierki.Add(new Skoczek(new Punkt(6, 0), Strona.Czarna, this));
            bierki.Add(new Hetman(new Punkt(3, 0), Strona.Czarna, this));
            bierki.Add(new Krol(new Punkt(4, 0), Strona.Czarna, this));
            //figury biale
            bierki.Add(new Wieza(new Punkt(0, 7), Strona.Biała, this));
            bierki.Add(new Wieza(new Punkt(7, 7), Strona.Biała, this));
            bierki.Add(new Goniec(new Punkt(2, 7), Strona.Biała, this));
            bierki.Add(new Goniec(new Punkt(5, 7), Strona.Biała, this));
            bierki.Add(new Skoczek(new Punkt(1, 7), Strona.Biała, this));
            bierki.Add(new Skoczek(new Punkt(6, 7), Strona.Biała, this));
            bierki.Add(new Hetman(new Punkt(3, 7), Strona.Biała, this));
            bierki.Add(new Krol(new Punkt(4, 7), Strona.Biała, this));
        }
        /// <summary>
        /// Sprawdza czy itnieje bierka na podanej pozycji
        /// </summary>
        /// <param name="pozycja">pozycja do srpawdzenia</param>
        /// <param name="bierka">bierka ktora znajduje sie na podanej pozycji</param>
        /// <returns>zwraca prawda jezeli istnieje bierka na pozycji</returns>
        public bool BierkaNaPozycji(Punkt pozycja, out Bierka bierka)
        {
            var tmp = (this.bierki.Find(x => x.Pozycja == pozycja));
            if (tmp == null)
            {
                bierka = null;
                return false;
            }
            bierka = tmp;
            return true;
        }
        /// <summary>
        /// Probuje wykonac przemieszczenie bierki na wskazana pozycje
        /// </summary>
        /// <param name="pozycjaBierki">pozycja bierki ktora chcesz przemiescic</param>
        /// <param name="pozycjaPrzemieszczenia">pozycja na ktora chcesz przemiescic bierke</param>
        /// <returns>zwraca prawda jezeli udalo sie przemiescic bierke</returns>
        public bool SprobujWykonacRuch(Punkt pozycjaBierki, Punkt pozycjaPrzemieszczenia)
        {
            if (StanGry != Status.Gra)
                return false;
            Bierka bierka = BierkiGrajace.Find(x => x.Pozycja == pozycjaBierki);
            if (bierka == null)
                return false;
            if (bierka.WykonajRuch(pozycjaPrzemieszczenia))
            {
                StronaGrajaca = (StronaGrajaca == Strona.Biała) ? Strona.Czarna : Strona.Biała;
                ZbijBierke(pozycjaPrzemieszczenia, StronaGrajaca);
                wykonaneRucy.Add(new Tuple<Punkt, Punkt>(pozycjaBierki, pozycjaPrzemieszczenia));
                TestRuchow();
                SprawdzStatus();
                return true;
            }
            return false;
        }
        /// <summary>
        /// Sprawdza czy nalezy ograniczyc liste mozliwych ruchow do wykonania przez bierki
        /// </summary>
        public void TestRuchow()
        {
            //zmienne 
            Krol krol = KrolGrajacy;
            zaslonieceiSzacha = null;
            //linie proste
            //w gore
            List<Punkt> testLini = new List<Punkt>();
            List<Bierka> obronca = new List<Bierka>();
            for (int i = 1; i < 8; i++)
            {
                testLini.Add(krol.Pozycja - new Punkt(0, i));
                if (Test(testLini, obronca, new List<Bierki> { Bierki.Hetman, Bierki.Wieża, Bierki.Krol }, i))
                    break;
            }
            //w dol
            testLini = new List<Punkt>();
            obronca = new List<Bierka>();
            for (int i = 1; i < 8; i++)
            {
                testLini.Add(krol.Pozycja + new Punkt(0, i));
                if (Test(testLini, obronca, new List<Bierki> { Bierki.Hetman, Bierki.Wieża, Bierki.Krol }, i))
                    break;
            }
            //w lewo
            testLini = new List<Punkt>();
            obronca = new List<Bierka>();
            for (int i = 1; i < 8; i++)
            {
                testLini.Add(krol.Pozycja - new Punkt(i, 0));
                if (Test(testLini, obronca, new List<Bierki> { Bierki.Hetman, Bierki.Wieża, Bierki.Krol }, i))
                    break;
            }
            //w prawo
            testLini = new List<Punkt>();
            obronca = new List<Bierka>();
            for (int i = 1; i < 8; i++)
            {
                testLini.Add(krol.Pozycja + new Punkt(i, 0));
                if (Test(testLini, obronca, new List<Bierki> { Bierki.Hetman, Bierki.Wieża, Bierki.Krol }, i))
                    break;
            }
            //skosy
            //w gore i lewo
            testLini = new List<Punkt>();
            obronca = new List<Bierka>();
            for (int i = 1; i < 8; i++)
            {
                testLini.Add(krol.Pozycja - new Punkt(i, i));
                if (Test(testLini, obronca, new List<Bierki> { Bierki.Hetman, Bierki.Goniec, Bierki.Krol, Bierki.Pionek }, i))
                    break;
            }
            //w prawo i dol
            testLini = new List<Punkt>();
            obronca = new List<Bierka>();
            for (int i = 1; i < 8; i++)
            {
                testLini.Add(krol.Pozycja + new Punkt(i, i));
                if (Test(testLini, obronca, new List<Bierki> { Bierki.Hetman, Bierki.Goniec, Bierki.Krol, Bierki.Pionek }, i))
                    break;
            }
            //w prawo i gore
            testLini = new List<Punkt>();
            obronca = new List<Bierka>();
            for (int i = 1; i < 8; i++)
            {
                testLini.Add(krol.Pozycja + new Punkt(i, -i));
                if (Test(testLini, obronca, new List<Bierki> { Bierki.Hetman, Bierki.Goniec, Bierki.Krol, Bierki.Pionek }, i))
                    break;
            }
            //w lewo i dol
            testLini = new List<Punkt>();
            obronca = new List<Bierka>();
            for (int i = 1; i < 8; i++)
            {
                testLini.Add(krol.Pozycja + new Punkt(-i, i));
                if (Test(testLini, obronca, new List<Bierki> { Bierki.Hetman, Bierki.Goniec, Bierki.Krol, Bierki.Pionek }, i))
                    break;
            }
            //skoczek
            Test(new List<Punkt> { new Punkt(1, 2) + krol.Pozycja }, new List<Bierka>(), new List<Bierki> { Bierki.Skoczek }, 0);
            Test(new List<Punkt> { new Punkt(-1, 2) + krol.Pozycja }, new List<Bierka>(), new List<Bierki> { Bierki.Skoczek }, 0);
            Test(new List<Punkt> { new Punkt(-2, 1) + krol.Pozycja }, new List<Bierka>(), new List<Bierki> { Bierki.Skoczek }, 0);
            Test(new List<Punkt> { new Punkt(-2, -1) + krol.Pozycja }, new List<Bierka>(), new List<Bierki> { Bierki.Skoczek }, 0);
            Test(new List<Punkt> { new Punkt(-1, -2) + krol.Pozycja }, new List<Bierka>(), new List<Bierki> { Bierki.Skoczek }, 0);
            Test(new List<Punkt> { new Punkt(1, -2) + krol.Pozycja }, new List<Bierka>(), new List<Bierki> { Bierki.Skoczek }, 0);
            Test(new List<Punkt> { new Punkt(2, -1) + krol.Pozycja }, new List<Bierka>(), new List<Bierki> { Bierki.Skoczek }, 0);
            Test(new List<Punkt> { new Punkt(2, 1) + krol.Pozycja }, new List<Bierka>(), new List<Bierki> { Bierki.Skoczek }, 0);
        }
        /// <summary>
        /// test ograniczania ruchow bierki
        /// </summary>
        /// <param name="linia">linia po ktorej sie poruszamy</param>
        /// <param name="obronca">test czy powsal wczesniej obronca</param>
        /// <returns></returns>
        bool Test(List<Punkt> linia, List<Bierka> obronca, List<Bierki> atakujacyList, int odleglosc)
        {
            if (!linia.Last().Pomiedzy(7))//sprawdz czy punkt nie jest poza plansza
                return true;
            if (BierkaNaPozycji(linia.Last(), out Bierka bierka))//czy na pozycji znajduje sie bierka
            {
                if (bierka.Kolor == StronaGrajaca)//jezeli ten sam kolor
                {
                    if (obronca.Count == 0)//jezeli to pierwszy obronca
                        obronca.Add(bierka);//ustaw obronce
                    else//jezeli kolejny
                        return true;//jezeli jest dwuch obroncow to przestan sprawdzac
                }
                else//jezeli bierka jest innego koloru
                {
                    if (!atakujacyList.Contains(bierka.Nazwa))//sprawdzanie czy bierka atakujaca jest na liscie
                        return true;
                    if (bierka.Nazwa == Bierki.Krol || bierka.Nazwa == Bierki.Pionek)//test krola czy jest wystarczajaca odleglosc
                        if (odleglosc > 1)
                            return true;

                    if (obronca.Count == 0)//jezeli nie ma obroncow
                    {
                        if (zaslonieceiSzacha == null)//jezeli juz nie było zasloniecia szacha
                            zaslonieceiSzacha = linia;//ustaw linie jako jedyna mozliwosc do zasloniecia
                        else//jezeli podwujny szach
                            zaslonieceiSzacha = new List<Punkt>();//to brak mozliwosci ruchu
                    }
                    else//jezeli byl obronca
                    {
                        obronca[0].Ograniczenia = linia;//ustaw ograniczenie do tej lini
                        obronca[0].NumerOgraniczenia = Ruchy;//ustaw ograniczenie na ta ture
                    }
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// Usowa bierke z listy
        /// </summary>
        /// <param name="pozycja">pozycja na ktorej znajduje sie bierka do zbicia</param>
        /// <param name="kolor">kolor bierki do zbicia</param>
        internal void ZbijBierke(Punkt pozycja, Strona kolor)
        {
            Bierka zbita = bierki.Find(x => x.Pozycja == pozycja && x.Kolor == kolor);
            bierki.Remove(zbita);
        }
        /// <summary>
        /// Sprawdza i w razie potrzeby zmienia status gry
        /// </summary>
        private void SprawdzStatus()
        {
            var bierkiGrajace = BierkiGrajace;
            //jezeli na planszy pozostal tylko krole
            if (bierkiGrajace.Count == 2)
            {
                StanGry = Status.Pat;
                koniecGry(StanGry);
                return;
            }
            //jezeli nie wykonano przelomu od 50 ruchow
            if(Ruchy - zmianaStatusu > 50)
            {
                StanGry = Status.Pat;
                koniecGry(StanGry);
                return;
            }
            //jezeli jest mozliwosc wykonania ruchu kontynuujemy gre
            bierkiGrajace.ForEach(x =>
            {
                if (x.PobMozliweRuchy.Count > 0)
                {
                    StanGry = Status.Gra;
                    return;
                }
            });
            //jezeli krol jest pod atakiem to mat
            if(listaRuchowPrzeciwnika.Contains(KrolGrajacy.Pozycja))
            {
                StanGry = Status.Mat;
                koniecGry(StanGry);
                return;
            }
            //jezeli nie to pat
            StanGry = Status.Pat;
            koniecGry(StanGry);
            return;
        }
    }
}
