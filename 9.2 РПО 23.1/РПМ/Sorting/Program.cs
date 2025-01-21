using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sorting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create mass");
            int[] array = CreateRandomMass();

            #region Bubble sort
            Console.WriteLine("Before bubble sort:");
            ShowMass(array);

            array = DistinctBubbleSort(array);
            Console.WriteLine("\n\n\nSorting...\n\n\n");

            Console.WriteLine("After bubble sort:");
            ShowMass(array);
            #endregion

            #region Quick sort
            //Console.WriteLine("\n-----------------------------------------Before quick sort:---------------------------");
            //ShowMass(array);

            //array = QuickSort(array, 0, array.Length - 1);

            //Console.WriteLine("\n-----------------------------------------After quick sort:----------------------------");
            //ShowMass(array);
            #endregion
        }

        static int[] BubbleSort(int[] mass)
        {
            int temp;
            for (int i = 0; i < mass.Length; i++)
            {
                for (int j = 0; j < mass.Length; j++)
                {
                    if ((mass[i] < mass[j]))
                    {
                        temp = mass[i];
                        mass[i] = mass[j];
                        mass[j] = temp;
                    }
                }
                ShowMass(mass);
            }
            return mass;
        }

        static int[] EvenBubbleSort(int[] mass)
        {
            int temp;
            for (int i = 1; i < mass.Length; i+=2)
            {
                for (int j = 1; j < mass.Length; j+=2)
                {
                    if ((mass[i] < mass[j]))
                    {
                        temp = mass[i];
                        mass[i] = mass[j];
                        mass[j] = temp;
                    }
                }
                ShowMass(mass);
            }
            return mass;
        }

        static int[] OddEvenBubbleSort(int[] mass)
        {
            int temp;
            for (int i = 0; i < mass.Length; i++)
            {
                if (i % 2 == 0)
                {
                    for (int j = 0; j < mass.Length; j += 2)
                    {
                        if ((mass[i] < mass[j]))
                        {
                            temp = mass[i];
                            mass[i] = mass[j];
                            mass[j] = temp;
                        }
                    }
                }
                else
                {
                    for (int j = 1; j < mass.Length; j += 2)
                    {
                        if ((mass[i] > mass[j]))
                        {
                            temp = mass[i];
                            mass[i] = mass[j];
                            mass[j] = temp;
                        }
                    }
                }
                ShowMass(mass);
            }
            return mass;
        }

        static int[] DistinctBubbleSort(int[] mass)
        {
            int temp;
            int[] result = new int[mass.Length];
            for (int i = 0; i < mass.Length; i++)
            {
                for (int j = 0; j < mass.Length; j++)
                {
                    if ((mass[i] < mass[j]))
                    {
                        temp = mass[i];
                        mass[i] = mass[j];
                        mass[j] = temp;
                    }
                }
                ShowMass(mass);
            }
            for (int i = 0; i < mass.Length; i++)
            {
                for (int j = 0; j < mass.Length; j++)
                {                    
                    if ((mass[i] != mass[j]))
                    {
                        result[i] = mass[i];
                    }
                    else
                    {
                        continue;
                    }
                }        
                ShowMass(result);
            }
            return result;
        }

        static int[] QuickSort(int[] array, int leftIndex, int rightIndex)
        {
            var i = leftIndex;
            var j = rightIndex;
            var pivot = array[leftIndex];
            while (i <= j)
            {
                while (array[i] < pivot)
                {
                    i++;
                }
                while (array[j] > pivot)
                {
                    j--;
                }
                if (i <= j)
                {
                    int temp = array[i];
                    array[i] = array[j];
                    array[j] = temp;
                    i++;
                    j--;
                }
            }
            ShowMass(array);
            if (leftIndex < j)
                QuickSort(array, leftIndex, j);
            if (i < rightIndex)
                QuickSort(array, i, rightIndex);
            return array;
        }

        static int[] CreateRandomMass()
        {
            Console.Write("Please, enter array length: ");
            int num = Convert.ToInt32(Console.ReadLine());
            int[] mass = new int[num];
            Random random = new Random();
            for (int i = 0; i < num; i++)
            {
                mass[i] = random.Next(0, 10);
            }
            return mass;
        }

        static int[] CreateMass()
        {
            Console.Write("Please, enter array length: ");
            int num = Convert.ToInt32(Console.ReadLine());
            int[] mass = new int[num];
            Console.WriteLine("Please, enter array elements: ");
            for (int i = 0; i < num; i++)
            {
                mass[i] = Convert.ToInt32(Console.ReadLine());
            }
            return mass;
        }

        static void ShowMass(int[] mass)
        {
            Console.WriteLine("\n-----------------------------------------Array elements:------------------------------");
            foreach (int i in mass)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
    }
}
