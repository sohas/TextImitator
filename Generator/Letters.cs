using System;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Text;

namespace Generator
{
    public class Letters
    {
        public char[] A;

        public static readonly Letters L = new Letters()
        { A = new char[]
            {
            ' ',//0
            'а',//1
            'б',//2
            'в',//3
            'г',//4
            'д',//5
            'е',//6
            'ё',//7
            'ж',//8
            'з',//9
            'и',//10
            'й',//11
            'к',//12
            'л',//13
            'м',//14
            'н',//15
            'о',//16
            'п',//17
            'р',//18
            'с',//19
            'т',//20
            'у',//21
            'ф',//22
            'х',//23
            'ц',//24
            'ч',//25
            'ш',//26
            'щ',//27
            'ъ',//28
            'ы',//29
            'ь',//30
            'э',//31
            'ю',//32
            'я',//33
            }
        };

        public int NumberOfLetter(char letter)
        //находит номер буквы в словаре
        {
            for (int i = 0; i < A.Length; i++) if (A[i] == letter) return i;
            return -1;
        }

        public bool IsGoodSymbol(char symbol)
        //проверяет, есть ли бкува в словаре
        {
            foreach (var e in A)
                if (char.ToLower(symbol) == e) return true;
            return false;
        }

        public char GetLetter(Prob prob)
        //генерирует букву по суммарному массиву
        {
            var randArr = new byte[10];
            var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(randArr);
            double z = 0;
            for (int i = 0; i < 10; i++)
                z += ((int)randArr[i]) / (Math.Pow(256, i));
            return A[prob.GetNumberLetter(z / 256)];
        }

        public StringBuilder GetSBText(string text)
        //переводит текст в SB(строчные), убирая не алфавитные символы и лишние пробелы
        {
            var sbText = new StringBuilder();
            var temp = true;
            var l = new Letters() {A = A};
            foreach (var e in text)
            {
                if (l.IsGoodSymbol(char.ToLower(e)) && !(e == A[0]))
                {
                    sbText.Append(char.ToLower(e));
                    temp = true;
                }
                else if ((temp == true) && (!(l.IsGoodSymbol(char.ToLower(e))) || (e == l.A[0])))
                {
                    sbText.Append(l.A[0]);
                    temp = false;
                }
            }
            return sbText;
        }

        public Prob GetNetArray(List<Prob> listArrays)
        //суммирует все значения массивов на i-х позициях в новом накопительном массиве 
        {
            var alphSize = A.Length;
            var addArray = new Prob() { P = new double[alphSize] };
            for (int i = 0; i < alphSize; i++)
                foreach (var e in listArrays)
                    addArray.P[i] += e.P[i];
            return addArray;
        }

        public List<Hope> GetHopeArray(string text, int depth)
        //создаёт список групп коэффициентов для сериализации
        {
            var listHopes = new List<Hope>();

            var alph = new Letters() { A = A };

            for (int size = 0; size <= depth; size++)
            {
                if (size == 0) listHopes.Add(alph.GetProb(alph.GetSBText(text), 0));
                if (size > 0)
                {
                    listHopes.Add(alph.GetProb(alph.GetSBText(text), size));
                    GC.Collect();
                }
            }
            return listHopes;
        }

        public Prob FindHope(string sample, int size, List<Hope> listHopes)
        //из набора коэффициентов по size, shift и по образцу поиска sample вытаскивает массив нормированных коэффициентов 
        {
            var array = new double[A.Length];
            foreach (var e in listHopes)
                if (e.Size == size)
                    foreach (var h in e.DP)
                        if (h.Key == sample) return h.Value;
            return new Prob() { P = array };
        }

        public List<Prob> GetArraysForGen(StringBuilder text, int depth, List<Hope> listHopes)
        //из готового текста и организованного списка массивов коэффициентов вытаскивает список массивов для генерации следующего символа
        {
            var alph = new Letters() {A = A};
            var listArrays = new List<Prob>();
            var newDepth = Math.Min(depth, text.Length);

            foreach (var e in listHopes)
                if (e.Size == 0)
                    listArrays.Add(e.DP[""]);

            for (int size = 1; size <= newDepth; size++)
                listArrays.Add(alph.FindHope(text.FindSample(size), size, listHopes));
            return listArrays;
        }

        public Hope GetProb(StringBuilder text, int size)
        //создаёт группу частот 
        {
            var alph = new Letters() {A = A};
            var alphSize = A.Length;
            var dict = new SortedDictionary<string, Prob>();
            for (int i = size; i < text.Length; i++)
            {
                char s = text[i];

                int e = alph.NumberOfLetter(s);

                if (!(dict.ContainsKey(text.ToString(i - size, size))))
                    dict[text.ToString(i - size, size)] = new Prob() { P = new double[alphSize] };
                dict[text.ToString(i - size, size)].P[e]++;
            }
            var newDict = new SortedDictionary<string, Prob>();
            foreach (var e in dict) newDict[e.Key] = dict[e.Key].Normalize(alph, size, dict.Count, text.Length - size);
            Console.WriteLine("обработана глубина " + size);
            //var ppp = new Prob();
            //ppp.PrintDic(newDict);
            //Console.WriteLine();
            //Console.ReadKey();
            return new Hope() { DP = newDict, Size = size };
        }
    }
}
