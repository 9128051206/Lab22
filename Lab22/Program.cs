using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = Convert.ToInt32(Console.ReadLine());

            Func<object, int[]> func1 = new Func<object, int[]>(GetArray);
            Task<int[]> task1 = new Task<int[]>(func1, n);

            Func<Task<int[]>, int[]> func2 = new Func<Task<int[]>, int[]>(SummArray);
            Task<int[]> task2 = task1.ContinueWith<int[]>(func2);

            Func<Task<int[]>, int[]> func3 = new Func<Task<int[]>, int[]>(MaxArray);
            Task<int[]> task3 = task1.ContinueWith<int[]>(func3);

            Action<Task<int[]>> action = new Action<Task<int[]>>(PrintArray);
            Task task4 = task2.ContinueWith(action);

            task1.Start();
            Console.ReadKey();
        }
        static int[] GetArray(object a)
        {
            int n = (int)a;
            int[] array = new int[n];
            Random random = new Random();
            for (int i = 0; i < n; i++)
            {
                array[i] = random.Next(0, 100);
            }
            return array;
        }

        static int[] SummArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int sum = 0;

            for (int i = 0; i < array.Length; i++)
            {

                sum += array[i];
            }
            Console.WriteLine("Сумма всех элементов  массива равна: {0}", sum);
            return array;
        }

        static int[] MaxArray(Task<int[]> task)
        {
            int[] array = task.Result;
            int max = int.MinValue;
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] > max)
                {
                    max = array[i];
                }                
            }
            Console.WriteLine("Максимальный элемент массива равен: {0}", max);
            return array;
        }
        static void PrintArray(Task<int[]> task)
        {
            int[] array = task.Result;
            for (int i = 0; i < array.Count(); i++)
            {
                Console.Write($"{array[i]} ");
            }
        }

    }
}
