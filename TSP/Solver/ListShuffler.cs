using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSP.Solver
{
    /*
     * Algorytm Fishera-Yatesa
     * */
    static class ListShuffler
    {
        private static Random randomGenerator = new Random();

        public static void Shuffle<T>(this IList<T> list)
        {
            int itemsToShuffle = list.Count;
            while (itemsToShuffle > 0)
            {
                int pickedElementIndex = randomGenerator.Next(itemsToShuffle);
                --itemsToShuffle;
                SwapListElements(list, pickedElementIndex, itemsToShuffle);
            }
        }

        private static void SwapListElements<T>(IList<T> list, int firstIndex, int secondIndex)
        {
            T temp = list[firstIndex];
            list[firstIndex] = list[secondIndex];
            list[secondIndex] = temp;
        }
    }
}
