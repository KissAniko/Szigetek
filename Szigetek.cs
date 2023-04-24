using System;
using System.IO;

namespace _2022._11._19_Tenger_V3_elejétől
{
    class Program
    {
        static void Main(string[] args)
        {
            const char TENGER_JEL = '*';
            const char SZIGET_JEL = 'O';
            char[,] tenger = new char[10, 20];

            Alaphelyzet(TENGER_JEL, tenger);

            bool futasVege = false;
            do
            {
                switch (valasztMenubol())
                {
                    case 'm':
                        Megjelenit(tenger);
                        break;
                    case 'g': //generál egy pályát
                        Console.Write("Kérem a sorokszámát: ");
                        int sorokSzama = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Kérem az oszlopok számát: ");
                        int oszlopokSzama = Convert.ToInt32(Console.Read());
                        tenger = General(sorokSzama, oszlopokSzama, TENGER_JEL);
                        break;
                    case 'u': //üres pálya
                        palyaUritese(tenger, TENGER_JEL);
                        break;
                    case 's': //szigetet elhelyez
                        Console.Write("Adja meg a szigetek számát: ");
                        int szigetSzam = Convert.ToInt32(Console.ReadLine());
                        tenger = szigeteketRak(tenger, szigetSzam, SZIGET_JEL);
                        break;
                    case 'b': //betöltés fájlból
                        Console.Write("Adja meg az elérési út és a fájl nevét: ");
                        string fajlnev = Console.ReadLine();
                        FajlbolBetölt(fajlnev);
                        break;
                    case 'k': //kiírás fájlba
                        Console.Write("Adja meg az elérési utat és a fájl nevét: ");
                        string lementFajlnev = Console.ReadLine();
                        LementFajlba(tenger, lementFajlnev);
                        break;
                    case 'v': // van szomszédja
                        Console.WriteLine("Adja meg a sziget koordinátáit: ");
                        Console.WriteLine("A tenger sorszáma: ");
                        var sorszam = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("A tenger oszlopszáma: ");
                        var oszlopszam = Convert.ToInt32(Console.ReadLine());
                        if (vanSzomszedja(tenger, sorszam, oszlopszam, SZIGET_JEL))
                        {
                            Console.Write("Van szomszédja.");
                        }
                        else
                        {
                            Console.Write("Nincs szomszédja. ");
                        }
                        break;
                    case 'e': //kilépés
                        futasVege = true;
                        break;

                }
                InformacioKiirasa(tenger, SZIGET_JEL);

            } while (!futasVege);

            //Megjelenit(tenger, vanSzegely);

        }
        static void palyaUritese(char[,] terkep, char tengerjel)
        {
            for (int sorIndex = 0; sorIndex < terkep.GetLength(0); sorIndex++)
            {
                for (int oszlopIndex = 0; oszlopIndex < terkep.GetLength(1); oszlopIndex++)
                {
                    terkep[sorIndex, oszlopIndex] = tengerjel;
                }
            }
        }
        static bool vanSzomszedja(char[,] terkep, int sorszama, int oszlopszama, char szigetjel)
        {
            sorszama--;
            oszlopszama--;
            if (sorszama - 1 >= 0 && terkep[sorszama - 1, oszlopszama] == szigetjel)
            {
                return true;
            }
            if (sorszama + 1 < terkep.GetLength(0) && terkep[sorszama + 1, oszlopszama] == szigetjel)
            {
                return true;
            }
            if (oszlopszama - 1 > 0 && terkep[sorszama, oszlopszama - 1] == szigetjel)
            {
                return true;
            }
            if (oszlopszama < terkep.GetLength(1) && terkep[sorszama, oszlopszama + 1] == szigetjel)
            {
                return true;
            }
            return false;

        }

        static char valasztMenubol()
        {

            char choose;


            Console.WriteLine("\nMenu");
            Console.WriteLine("\t(m)egjelenít");
            Console.WriteLine("\t(g)enerál egy pályát. ");
            Console.WriteLine("\t(u)res pálya - A meglévő pálya alaphelyzetbe állítása. ");
            Console.WriteLine("\t(s)zigetek elhelyezése. ");
            Console.WriteLine("\t(b)etöltés fájlból");
            Console.WriteLine("\t(k)iírás fájlba. ");
            Console.WriteLine("\t(v)an szomszédja.");
            Console.WriteLine("\t(e)xit");
            do
            {
                choose = Console.ReadKey().KeyChar;

                if (choose == 'm' || choose == 'g' || choose == 'u' || choose == 'u' || choose == 's'

                    || choose == 'b' || choose == 'k' || choose == 'v')
                {
                    return choose;
                }

            } while (choose != 'e');
            return choose;
        }

        static void InformacioKiirasa(char[,] terkep, char szigetjel)
        {
            // hány sziget van a térképen?
            var szigetekSzama = 0;
            var vanSziget = false;

            for (int sorIndex = 0; sorIndex < terkep.GetLength(0); sorIndex++)
            {
                for (int oszlopIndex = 0; oszlopIndex < terkep.GetLength(1); oszlopIndex++)
                {
                    if (terkep[sorIndex, oszlopIndex] == szigetjel)
                    {
                        szigetekSzama++;
                    }
                    // A tenger szélén van-e sziget?
                    if ((sorIndex == 0 || sorIndex == terkep.GetLength(0) - 1 || oszlopIndex == 0 || oszlopIndex == terkep.GetLength(1)) && terkep[sorIndex, oszlopIndex] == szigetjel)
                    {
                        vanSziget = true;
                    }
                }
            }
            Console.WriteLine($"\nA szigetek száma:  {szigetekSzama}");
            if (vanSziget)
            {
                Console.WriteLine("Van sziget a tenger szélén.");
            }
            else
            {
                Console.WriteLine("\nNincs sziget a tenger szélén. ");
            }




        }

        static void LementFajlba(char[,] terkep, string mentesfajlNeve)
        {
            string[] tengerTomb = new string[terkep.GetLength(0)];
            string terkepsor = "";
            for (int sorIndex = 0; sorIndex < terkep.GetLength(0); sorIndex++)
            {
                for (int oszlopIndex = 0; oszlopIndex < terkep.GetLength(1); oszlopIndex++)
                {
                    terkepsor += terkep[sorIndex, oszlopIndex];
                }
                tengerTomb[sorIndex] = terkepsor;
                terkepsor = "";
            }
            File.WriteAllLines(mentesfajlNeve, tengerTomb);


        }

        static char[,] FajlbolBetölt(string fajlNeve)
        {
            string[] sorokTomb = File.ReadAllLines(fajlNeve);
            char[,] betoltottTomb = new char[sorokTomb.Length, sorokTomb[0].Length];
            string sorokTombSor;
            for (int sorokTombIndex = 0; sorokTombIndex < betoltottTomb.GetLength(0); sorokTombIndex++)
            {
                sorokTombSor = sorokTomb[sorokTombIndex];
                for (int oszlopTombIndex = 0; oszlopTombIndex < betoltottTomb.GetLength(1); oszlopTombIndex++)
                {
                    betoltottTomb[sorokTombIndex, oszlopTombIndex] = sorokTombSor[oszlopTombIndex];
                }

            }

            return betoltottTomb;
        }

        static char[,] szigeteketRak(char[,] terkep, int szigetekSzama, char szigetjel)
        {
            int randomSor;
            int randomOszlop;
            Random rnd = new Random();
            for (int sorIndex = 0; sorIndex < szigetekSzama; sorIndex++)
            {

                randomSor = rnd.Next(terkep.GetLength(0));
                randomOszlop = rnd.Next(terkep.GetLength(1));
                terkep[randomSor, randomOszlop] = szigetjel;
            }
            return terkep;
        }

        static char[,] General(int sorszam, int oszlopszam, char tengerjel)
        {
            char[,] generaltTenger = new char[sorszam, oszlopszam];
            for (int sorIndex = 0; sorIndex < generaltTenger.GetLength(0); sorIndex++)
            {
                for (int oszlopIndex = 0; oszlopIndex < generaltTenger.GetLength(1); oszlopIndex++)
                {
                    generaltTenger[sorIndex, oszlopIndex] = tengerjel;
                }
            }
            return generaltTenger;
        }

        static void Alaphelyzet(char tengerjel, char[,] terkep)
        {
            for (int sorokIndex = 0; sorokIndex < terkep.GetLength(0); sorokIndex++)
            {
                for (int oszlopIndex = 0; oszlopIndex < terkep.GetLength(1); oszlopIndex++)
                {
                    terkep[sorokIndex, oszlopIndex] = tengerjel;
                }
            }
        }
        static void Megjelenit(char[,] terkep)
        {
            Console.Clear();
            Console.Write(" ");
            for (int oszlopindex = 0; oszlopindex < terkep.GetLength(1); oszlopindex++)
            {
                if (oszlopindex % 10 == 0)
                {
                    Console.Write(".");
                }
                else
                {
                    Console.Write((oszlopindex + 1) % 10);
                }

            }
            Console.WriteLine();
            for (int sorokIndex = 0; sorokIndex < terkep.GetLength(0); sorokIndex++)
            {
                if (sorokIndex % 10 == 0)
                {
                    Console.Write(".");

                }
                else
                {
                    Console.Write((sorokIndex + 1) % 10);
                }

                for (int oszlopIndex = 0; oszlopIndex < terkep.GetLength(1); oszlopIndex++)
                {

                    Console.Write(terkep[sorokIndex, oszlopIndex]);
                }
                Console.WriteLine();
            }
        }
    }
}

