using System.Diagnostics;
using System.Linq; // 다양한 자료구조들에 대한 탐색/취합/추출 등의 기능을 제공하는 namespace

namespace SortAlgos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            int[] array = //{ 1, 4, 3, 3, 9, 8, 7, 2, 5, 0 };
                Enumerable.Repeat(0, 10000000)
                          .Select(x => random.Next(0, 1000)).ToArray();

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            //ArraySort.BubbleSort(array);
            //ArraySort.SelectionSort(array);
            //ArraySort.InsertionSort(array);
            //ArraySort.RecursiveMergeSort(array);
            //ArraySort.MergeSort(array);
            //ArraySort.RecursiveQuickSort(array); // 중복 많을때 성능저하 있음
            //ArraySort.QuickSort(array);

            stopwatch.Stop();

            Console.WriteLine($"ElapsedTime : {stopwatch.ElapsedMilliseconds}");


            //Console.Write("{");
            //for (int i = 0; i < array.Length; i++)
            //{
            //    Console.Write($"{array[i]}, ");
            //}
            //Console.Write("}");
        }
    }
}