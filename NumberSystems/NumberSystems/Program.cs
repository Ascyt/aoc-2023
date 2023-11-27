using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace NumberSystems
{
    class Programm
    {
        private static Dictionary<char, int> NonNumbers = new Dictionary<char, int> {
            { 'A', 10}, { 'B', 11}, { 'C', 12},{ 'D', 13}, { 'E', 14}, { 'F', 15},{ '=', -2}, { '-', -1} };
        public static void Main(string[] args)
        {
            string path = "text.txt"; //INPUT FILE PATH
            int NumberSystem = 0; //IN WELCHES NUMBERSYSTEM IHT WOLLT Z.B. HEXADEZIMAL (16)
            double sum = 0;
            double[] arr = ConvertToDezimal(Read(path), NumberSystem);
            foreach (double e in arr)
            {
                sum += e;
            }
            Console.WriteLine($"Summer alle Zahlen im 10er System: {sum}");
            Console.WriteLine($"Die Summe im {NumberSystem}er System: {ConvertToNonDezimal(sum, NumberSystem)}"); //KONVERTIERT DIE SUMME ZURÜCK IN DAS VORGEGBEN NUMMERSYSTEM
            //Console.WriteLine($"Die Summe im SNAFU System: {DecimalToSnafu(sum)}"); WENN IHR ZU SNAFU RECHNEN WOLLT
            Console.WriteLine("");
            Console.WriteLine("");
            TryToDezimal(Read(path), NumberSystem); //VERSUCHT DIE SUMME IM DEZIMAL SYSTEM AUSZUGEBEN WENN MÖGLICH FALLS ES OBEN NICHT GEKLAPPT HAT
        }
        /// <summary>
        /// Kvertiert zu snafu
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private static string DecimalToSnafu(double number)
        {
            string result = "";
            bool continues = true;
            int length = 0;
            for (int i = 0; continues; i++)
            {
                if (number < Math.Pow(5, i) * 2)
                {
                    continues = false;
                    length = i;
                }
            }
            double tmp = number;
            if (Math.Abs(tmp - Pow(length, 2)) < Math.Abs(tmp - Pow(length, 1)))
            {
                result += "2";
                tmp = Pow(length, 2);
            }
            else
            {
                result += "1";
                tmp = (Pow(length, 1));
            }
            for (int i = length - 1; i >= 0; i--)
            {
                double diff = Math.Abs(Pow(i, 2)) + 1;
                int index = 0;
                for (int k = 2; k > -3; k--)
                {
                    double b = Pow(i, k);
                    double c = number - tmp - Pow(i, k);
                    double a = Math.Abs(number - tmp - Pow(i, k));
                    if (Math.Abs(number - tmp - Pow(i, k)) < diff)
                    {
                        diff = Math.Abs(number - tmp - Pow(i, k));
                        index = k;
                    }
                }
                if (index < 0) result += NonNumbers.FirstOrDefault(x => x.Value == index).Key;
                else result += index;
                tmp += Pow(i, index);
            }
            return result;
        }
        /// <summary>
        /// Eine Funktion die eine Zahl hoch eine andere rechenet.
        /// </summary>
        /// <param name="index"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private static double Pow(double index, double number)
        {
            double a = Math.Pow(5, index) * number;
            return Math.Pow(5, index) * number;
        }

        /// <summary>
        /// Versucht das Ergebniss in Dezimal zu konvertieren falss möglich sonst leere Rückgabe.
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="number"></param>
        private static void TryToDezimal(string[] arr, int number)
        {
            try
            {
                uint sum = 0;
                foreach (string s in arr)
                {
                    sum += uint.Parse(s);
                }
                Console.WriteLine($"Die Summe in Dezimal Zahl im {number}er System: {ConvertToNonDezimal(sum, number)}");
            }
            catch (Exception)
            {
            }
        }
        /// <summary>
        /// Liest den Input und parsed ihn.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static string[] Read(string path)
        {
            string[] array = File.ReadAllLines(path);
            return array;
        }

        /// <summary>
        /// CKonvertiert die Summe in das gewünschte Zahelensystem.
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        private static string ConvertToNonDezimal(double sum, int number)
        {
            double rest = 0;
            string list = "";
            while (sum > 0)
            {
                rest = sum % number;
                sum /= number;

                if (NonNumbers.ContainsValue(Convert.ToInt32(rest)))
                {
                    int counter = 0;
                    Dictionary<char, int>.KeyCollection keys = NonNumbers.Keys;
                    foreach (char key in keys)
                    {
                        if (counter == rest - 10)
                        {
                            list += (Convert.ToString(key));
                            break;
                        }
                        counter++;
                    }
                }
                else list += rest;
            }
            char[] chars = list.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// Konvertiert die Summe zu Dezimal.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static double[] ConvertToDezimal(string[] input, int number)
        {
            double[] arr = new double[input.Length];
            for (int i = 0; i < input.Length; i++)
            {
                string tmp = input[i];
                double sum = 0;
                double counter = 0;
                for (int j = tmp.Length - 1; j >= 0; j--)
                {
                    if (Char.IsDigit(tmp[j]))
                    {
                        int a = Convert.ToInt32(tmp[j] - '0');
                        double b = Math.Pow(number, counter);
                        sum += Convert.ToInt32(tmp[j] - '0') * Math.Pow(number, counter);
                    }
                    else
                    {
                        if (NonNumbers.ContainsKey(tmp[j]))
                        {
                            int a = NonNumbers[tmp[j]];
                            double b = Math.Pow(number, counter);
                            sum += NonNumbers[tmp[j]] * Math.Pow(number, counter);
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    counter++;
                }
                arr[i] = sum;
            }
            return arr;
        }
    }
}