/*
 * Author: Laith Oudah
 * Kurs: L0002B
 * LTU: LAIOUD-1
 * UPPGIFT: Inlämningsuppgift 2
 */

using System;
using System.Dynamic;
using System.Runtime.Serialization.Formatters;
using System.IO;
using System.Xml;

namespace L0002B_Uppgift2
{
    class Program
    {
        // Struct, ett enklet sätt att hålla information om säljaren
        public struct SELLER
        {
            public string name; // Namn String
            public string location; // Distrikt String
            public long IDNum; // Personnummer 64 bit
            public int sales; // Försäljningar 32 bti
        }

        static void Main()
        {
            // I syfte att ge provision måste vi veta hur mycket minne skall reserveras
            Console.WriteLine("\nHur många försäljare?");

            // Håller koll på antal
            long holder = Convert.ToInt64(Console.ReadLine());

            // Array med säljare och en array för att hålla koll på bonussystemet 
            SELLER[] SellerDB = new SELLER[holder];
            int[] bonusDB = new int[4];

            // Söker igenom m.h.a get
            for (int i = 0; i < holder; i++)
            {
                SellerDB[i] = getSeller();
            }

            bubbleSort(holder, SellerDB);
            bonusCheck(holder, SellerDB, bonusDB);
            Printer(holder, SellerDB, bonusDB);
        } // Main

        /*
         * -------- Functions --------
         */

        // getSeller - Hämta & Spara information
        static SELLER getSeller()
        {
            // Placeholder
            SELLER seller;

            // Sparar informationen
            Console.WriteLine("Namn: ");
            seller.name = Console.ReadLine();
            Console.WriteLine("Personnummer: ");
            seller.IDNum = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Distrikt: ");
            seller.location = Console.ReadLine();
            Console.WriteLine("Försäljning: ");
            seller.sales = Convert.ToInt32(Console.ReadLine());

            // Returnerar till databasen 
            return seller;
        }

        // bubbleSort - Sorterar genom att jämföra försäljnings siffror och byter plats.
        static void bubbleSort(long holder, SELLER[] SellerDB)
        {
            for (int i = 0; i < holder; i++)
            {
                for (int j = 0; j < holder - 1; j++)
                {
                    if (SellerDB[j].sales < SellerDB[j + 1].sales)
                    {
                        SELLER placeholder = SellerDB[j];
                        SellerDB[j] = SellerDB[j + 1];
                        SellerDB[j + 1] = placeholder;
                    }
                } // For int J
            } // For int i
        } // bubbleSort


        // bonusCheck - Söker igenom de olika bonus nivåerna
        static void bonusCheck(long holder, SELLER[] SellerDB, int[] bonusDB)
        {
            for (int i = 0; i < holder; i++)
            {
                if (SellerDB[i].sales < 50)
                {
                    bonusDB[0]++;
                }
                else if (SellerDB[i].sales >= 50 && SellerDB[i].sales <= 90)
                {
                    bonusDB[1]++;
                }
                else if (SellerDB[i].sales >= 100 && SellerDB[i].sales <= 199)
                {
                    bonusDB[2]++;
                }
                else
                {
                    bonusDB[3]++;
                }
            } // For Loop
        } // bonusCheck

        // Printer - Skriver ut output bereonde på de olika bonus värderna man har
        static void Printer(long holder, SELLER[] SellerDB, int[] bonusDB)
        {
            // StreamWriter & Anger vart textfilen ska sparas
            StreamWriter streamWriter = new StreamWriter("desktop");

            // Börjar med att loopa igenom databasen för säljaren
            // Formatterar så att outputten blir riktigt snygg.
            for (var i = 0; i < holder; i++)
            {
                Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10}\n", "Namn", "Personnummer", "Distrikt", "Antal");
                streamWriter.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10}\n", "Namn", "Personnummer", "Distrikt",
                    "Antal");
                foreach (var t in SellerDB)
                {
                    Console.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10}\n", SellerDB[i].name, SellerDB[i].IDNum,
                        SellerDB[i].location, SellerDB[i].sales);
                    streamWriter.WriteLine("{0, -10} {1, -15} {2, -10} {3, -10}\n", SellerDB[i].name, SellerDB[i].IDNum,
                        SellerDB[i].location, SellerDB[i].sales);
                }

                // Vilken bonus har säljaren?
                if (SellerDB[i].sales < 50)
                {
                    Console.WriteLine(bonusDB[0] + " Säljare har nått nivå 1: Under 50 Atriklar sålda\n");
                    streamWriter.WriteLine(bonusDB[0] + " Säljare har nått nivå 1: Under 50 Atriklar sålda\n");
                }
                else if (SellerDB[i].sales >= 50 && SellerDB[i].sales <= 90)
                {
                    Console.WriteLine(bonusDB[1] + " Säljare har nått nivå 2: 50 - 99 Atriklar sålda\n");
                    streamWriter.WriteLine(bonusDB[1] + " Säljare har nått nivå 1: 50 - 99 Atriklar sålda\n");
                }
                else if (SellerDB[i].sales >= 100 && SellerDB[i].sales <= 199)
                {
                    Console.WriteLine(bonusDB[3] + " Säljare har nått nivå 3: 100 - 199 Atriklar sålda\n");
                    streamWriter.WriteLine(bonusDB[3] + " Säljare har nått nivå 3: 100 - 199 Atriklar sålda\n");
                }
                else
                {
                    Console.WriteLine(bonusDB[4] + " Säljare har nått nivå 4: Över 199 Atriklar sålda\n");
                    streamWriter.WriteLine(bonusDB[4] + " Säljare har nått nivå 4: Över 199 Atriklar sålda\n");
                }
            } // For Loop 

            // Slutar skriva till filen
            streamWriter.Close();
        } // Printer
    } // Program
} // Namespace