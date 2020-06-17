using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace LogikaSzachy
{
    public class KomputerowyPrzeciwnik
    {
        /// <summary>
        /// plansza do gry
        /// </summary>
        readonly Plansza plansza;
        /// <summary>
        /// ile ruchow do przodu komputer ma przewidywac
        /// </summary>
        readonly int glebokoscAnalizy;
        /// <summary>
        /// Tworzenie komputerowego przeciwnika
        /// </summary>
        /// <param name="plansza">plansza na ktorej bot ma wykonywac ruchy</param>
        /// <param name="glebokoscAnalizy">ile ruchow do przodu komputer ma przewidywac</param>
        public KomputerowyPrzeciwnik(Plansza plansza, int glebokoscAnalizy = 1)
        {
            this.plansza = plansza;
            this.glebokoscAnalizy = glebokoscAnalizy;
        }
        /// <summary>
        /// Wykonanie za pomoca bota ruchu na planszy
        /// </summary>
        public void WykonajRuch()
        {
            Analiza analiza = new Analiza(plansza, glebokoscAnalizy * 2);
            var tmp = analiza.dzieci.FindAll(x => x.WartoscAnalizy == analiza.WartoscAnalizy);
            var los = tmp[new Random().Next(0, tmp.Count)];
            if(!plansza.SprobujWykonacRuch(los.ruchZ, los.ruchDo))
            {
                throw new Exception("cos jest nie tak");
            }
        }
    }

    class Analiza
    {
        readonly internal Punkt ruchZ;
        readonly internal Punkt ruchDo;

        /// <summary>
        /// plansza rodzica
        /// </summary>
        Plansza plansza;
        /// <summary>
        /// strona po ktorej jest toczona gra
        /// </summary>
        Strona strona;
        /// <summary>
        /// glebokosc analizy
        /// </summary>
        int glebokoscAnalizy;

        /// <summary>
        /// lista dzieci w analizie
        /// </summary>
        internal List<Analiza> dzieci = new List<Analiza>();
        /// <summary>
        /// jeze true analiza nie została jeszcze policzona
        /// </summary>
        bool analiza = true;
        /// <summary>
        /// wartosc punktowa analizy
        /// </summary>
        decimal wartoscAnalizy;
        /// <summary>
        /// wartosc punktowa analizy
        /// </summary>
        public decimal WartoscAnalizy
        {
            get
            {
                if (analiza)
                {
                    if (strona == plansza.StronaGrajaca)
                        wartoscAnalizy = dzieci.Max(x => x.WartoscAnalizy);
                    else
                        wartoscAnalizy = dzieci.Min(x => x.WartoscAnalizy);
                    analiza = false;
                }
                return wartoscAnalizy;
            }
        }
        /// <summary>
        /// tworzenie nowej analizy
        /// </summary>
        /// <param name="plansza">plansza do analizowania</param>
        /// <param name="glebokoscAnalizy">glembokosc analizy</param>
        public Analiza(Plansza plansza, int glebokoscAnalizy)
        {
            this.plansza = plansza;
            this.strona = plansza.StronaGrajaca;
            this.glebokoscAnalizy = glebokoscAnalizy;

            RobDzieci(1);
        }

        void RobDzieci(int analizaNr)
        {
            if (analizaNr > glebokoscAnalizy)
            {
                wartoscAnalizy = plansza.bierki.FindAll(x => x.Kolor == strona).Sum(x => x.WartoscPunktowa);
                wartoscAnalizy /= plansza.bierki.FindAll(x => x.Kolor != strona).Sum(x => x.WartoscPunktowa);
                analiza = false;
            }
            else
                foreach (Bierka bierka in plansza.BierkiGrajace)
                    foreach (Punkt punkt in bierka.PobMozliweRuchy)
                        dzieci.Add(new Analiza(plansza, strona, analizaNr, glebokoscAnalizy, bierka.Pozycja, punkt));
        }

        Analiza(Plansza plansza, Strona strona, int analizaNr, int glebokoscAnalizy, Punkt ruchZ, Punkt ruchDo)
        {
            void Koniec(Plansza.Status status)
            {
                switch (status)
                {
                    case Plansza.Status.Mat:
                        wartoscAnalizy = decimal.MaxValue;
                        analiza = false;
                        break;
                    case Plansza.Status.Pat:
                        wartoscAnalizy = 1;
                        analiza = false;
                        break;
                }
            }
            Bierki Promocja()
            {
                return Bierki.Hetman;
            }

            this.ruchZ = ruchZ;
            this.ruchDo = ruchDo;
            this.strona = strona;
            this.glebokoscAnalizy = glebokoscAnalizy;
            this.plansza = plansza.Kopiuj(Promocja, Koniec);
            this.plansza.SprobujWykonacRuch(ruchZ, ruchDo);

            if (analiza)
                RobDzieci(analizaNr + 1);
        }
    }
}
