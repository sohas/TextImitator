using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace Generator
{
    public class Prob
    {
        public double[] P;

        public int GetNumberLetter(double rand)
        //находит позицию в массиве накопленных частот по случайной величине распределённой на [0,1]
        {
            var n = P.Length;
            if (P[0] >= rand * P[n - 1]) return 0;
            else
            {
                for (int i = 1; i < n; i++)
                    if ((P[i - 1] < rand * P[n - 1]) && (P[i] >= rand * P[n - 1]))
                        return i;
            }
            return n - 1;
        }

        public char GetLetter(Letters alph)
        //генерирует букву по суммарному массиву
        {
            var randArr = new byte[10];
            var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(randArr);
            double z = 0;
            for (int i = 0; i < 10; i++)
                z += ((int)randArr[i]) / (Math.Pow(256, i));
            return alph.A[GetNumberLetter(z / 256)];
        }

        public Prob Normalize(Letters alph, int size, int dCount, int allTextSize)
        //Нормализует массивы частот и строит накопительную функцию. для каждой входной строки "s" для каждой буквы алфавита 'c' считается число раз, когда
        //'c' встретилась после "s" - получаем массив из 34 чисел. после этого значение для каждого из 34 символа домножается на сигма, вычисленную для 
        //всех букв в массиве и делится на среднее по этому массиву. после этого вводятся поправки. 1. делим на число букв во всём тексте. 2. умножаем на число
        //полученных словарей. 3. умножаем на расчётную вероятность q, что в тексте при случайной генерации встретится хотя бы одна копия "s."
        {
            var alphSize = alph.A.Length;
            var sum = P.Sum();
            var sumsqw = P.Sum(x => x * x);
            var sigma = Math.Sqrt((sumsqw - sum * sum / alphSize) / (alphSize - 1));
            var p = allTextSize / Math.Pow(alphSize, size);
            var q = (p + 1 - Math.Abs(p - 1)) / 2;
            var newP = new double[alphSize];
            newP[0] = Math.Round((P[0] * sigma * alphSize / sum / allTextSize * dCount * q), 3);
            for (int i = 1; i < alphSize; i++) newP[i] = newP[i - 1] + Math.Round((P[i] * sigma * alphSize / sum / allTextSize * dCount * q), 3);
            return new Prob() {P = newP};
        }

        public void PrintDic(SortedDictionary<string, Prob> dict)
        //тупо печатает словарь
        {
            foreach (var e in dict.Keys)
            {
                Console.WriteLine(e);
                foreach (var d in dict[e].P)
                {
                    Console.Write(d + " ");
                }
                Console.WriteLine();
                //Console.ReadKey();
            }
        }
    }
}
