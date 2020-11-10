using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using static Generator.Letters;

namespace Generator
{
    public class Generator
    {
        public static void Main()
        {
            //проверка наличия файла-образца
            while (!File.Exists(Environment.CurrentDirectory + "\\input\\input.txt"))
            {
                Console.WriteLine("не существует файла образца. положите текстовый файл input.txt с образцом текста в папку input");
                Console.WriteLine("нажмите любую клавишу, как только сделаете это");
                Console.ReadKey();
            }

            Console.WriteLine();
            Console.WriteLine("введите глубину анализа (от 1 до 15) и нажмите enter");
            Console.WriteLine();
            Console.WriteLine("при большой глубине и большом размере файла-образца анализ и генерация текста могут занять много времени");
            Console.WriteLine();

            //проверка условия ввода
            int depth = -1;
            while ((depth < 0) || (depth > 15))
            {
                var inputDepth = Console.ReadLine();
                if ((inputDepth.IsInputNum()) && (int.Parse(inputDepth) <= 15) && (int.Parse(inputDepth) >= 0)) depth = int.Parse(inputDepth);
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("введите глубину анализа (от 1 до 15) и нажмите enter");
                    Console.WriteLine();
                }
            }

            Console.WriteLine();
            Console.WriteLine("подождите, идёт подготовка коэффициентов для анализа");
            Console.WriteLine();

            var listHopes = L.GetHopeArray(File.ReadAllText(Environment.CurrentDirectory + "\\input\\input.txt", Encoding.Default), depth);

            Console.WriteLine("коэффициенты подготовлены. нажмите любую клавишу");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("введите желаемую длину сгенерированного текста и нажмите enter");
                Console.WriteLine();

                //проверка ввода длины текста
                int textLenth = 0;
                while (textLenth < 1)
                {
                    var inputTextSize = Console.ReadLine();
                    if (inputTextSize.IsInputNum()) textLenth = int.Parse(inputTextSize);
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("введите желаемую длину сгенерированного текста и нажмите enter");
                        Console.WriteLine();
                    }
                }

                Console.WriteLine();
                Console.WriteLine("введите какое-то начало текста и нажмите enter или просто нажмите enter");
                Console.WriteLine();
                var text = new StringBuilder(Console.ReadLine());

                Console.Write(text);
                for (int i = 0; i < textLenth; i++)
                {
                    var letter = L.GetLetter(L.GetNetArray(L.GetArraysForGen(text, depth, listHopes)));
                    Console.Write(letter);
                    text.Append(letter);
                }

                var head = string.Format("{0:d-MM-yyyy} {0:HH:mm:ss} {1}", DateTime.Now, "глубина = "+depth);
                var textToFile = new List<string>() {head, "", text.ToString(), "", ""};

                File.AppendAllLines(Environment.CurrentDirectory + "\\out\\out.txt", textToFile, Encoding.Default);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("текст сохранён в файле out.txt в папке out");
                Console.WriteLine();
                Console.WriteLine("если хотите выйти, закройте программу. если хотите продолжить - нажмите enter");
                Console.ReadLine();
                //return text.ToString();
            }
        }
    }
}
