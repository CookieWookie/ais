using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> List = new List<int>();
            //string input;
            int zadana = -1;
            int hladana;
            Console.WriteLine("Zadaním - 1 ukončíte program");

            do
            {
                Console.WriteLine("Zadajte číslo:");
                zadana = Convert.ToInt32(Console.ReadLine());

                if (zadana != -1)
                    List.Add(zadana);
            } while (zadana != -1);

            Console.WriteLine("Vyberte si z nasledujúcich čísiel");
            foreach (int a in List)
                Console.WriteLine(a);
            Console.WriteLine();
            Console.WriteLine("Napíšte hľadané číslo");

            hladana = Convert.ToInt32(Console.ReadLine());

            int i1 = 0;
            int x = List.Count;
            for (i1 = 0; i1 < x; i1++)
            {
                if (List[i1] == hladana)
                {
                    Console.WriteLine("Hladane cislo je pod indexom: " + i1 + " a pocet krokov je: " + (i1 + 1));
                    break;
                }
                if (i1 == (x - 1))
                    Console.WriteLine("Nenachadza sa");
            }

            int low = 0;
            int high = List.Count - 1;
            int mid = 0;
            int i2 = 0;

            while (low <= high)
            {
                i2++;
                mid = ((low + high) / 2);
                if (List[mid] == hladana)
                {

                    Console.WriteLine("Hladane cislo je pod indexom: " + mid + " a pocet krokov je: " + i2);
                    break;
                }
                else if (List[mid] < hladana)
                    low = mid + 1;
                else
                {
                    high = mid - 1;

                }
                mid = -1;

            }
            if (mid == -1)
                Console.WriteLine("Nenachadza sa!!");

            int n = List.Count;
            int y = hladana;
            int c = Convert.ToInt32(Math.Sqrt(n / 2));
            int high1 = c;
            while (high1 < n && List[high1] < y)
                high1 = high1 + c;
            int low1 = high1 - c + 1;
            int i3 = 0;


            for (int i = low1; i <= high; i++)
            {
                i3++;
                if (List[i] == y)
                {
                    Console.WriteLine("Hladane cislo je pod indexom: " + i + " a pocet krokov je: " + i3);
                    break;
                }
                else if (i == high1)
                {
                    Console.WriteLine("Nenachadza sa");

                }
            }

            using (StreamWriter sw = new StreamWriter("vyhladavanie.txt"))
            {
                sw.WriteLine("Mnozina");
                foreach (int c1 in List)
                {
                    sw.WriteLine(c1);

                }
                sw.WriteLine("Hladana hodnota " + hladana);
                sw.WriteLine();
                sw.WriteLine("Cas vyhladavania:");
                sw.WriteLine("Linearne vyhladavanie: " + i1);
                sw.WriteLine("Binarne vyhladavanie: " + i2);
                sw.WriteLine("Chunk vyhladavanie: " + i3);

            }








            Console.ReadLine();







        }
    }
}

