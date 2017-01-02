using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutionss
{
    public static class Solutions
    {
        /// <summary>
        /// https://www.codewars.com/kata/next-smaller-number-with-the-same-digits
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long NextSmaller(long n)
        {
            StringBuilder number = new StringBuilder(n.ToString());

            int index = number.Length - 1;
            int max = -1;
            int positionOfMaximum = 0;
            for (int i = number.Length - 1; i > 0; i--)
            {
                if (number[i - 1] - '0' <= number[i] - '0')
                {
                    index = i - 1;
                }
                else
                {
                    break;
                }
            }

            if (index == 0)
            {
                return -1;
            }

            for (int i = number.Length - 1; i >= index; i--)
            {
                if (number[i] - '0' > max && number[i] - '0' < number[index - 1] - '0')
                {
                    max = number[i] - '0';
                    positionOfMaximum = i;
                }
            }

            char temp = number[positionOfMaximum];
            number.Replace(number[positionOfMaximum], number[index - 1], positionOfMaximum, 1);
            number.Replace(number[index - 1], temp, index - 1, 1);

            int tempIndex = index;
            for (int i = 0; i < (number.Length - index) / 2; i++)
            {
                temp = number[tempIndex];
                number.Replace(number[tempIndex], number[number.Length - 1 - i], tempIndex++, 1);
                number.Replace(number[number.Length - 1 - i], temp, number.Length - 1 - i, 1);
            }

            if (number[0] == '0')
            {
                return -1;
            }

            return long.Parse(number.ToString());
        }

        /// <summary>
        /// https://www.codewars.com/kata/find-the-unknown-digit
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static bool IsManyZeroes(string left, string right, string result)
        {
            if (left.Length > 1)
            {
                if (left[0] == '?' && left[1] == '?')
                    return true;
            }

            if (right.Length > 1)
            {
                if (right[0] == '?' && right[1] == '?')
                    return true;
            }

            if (result.Length > 1)
            {
                if (result[0] == '?' && result[1] == '?')
                    return true;
            }

            return false;
        }
        public static int Check(int[] repeats, string left, string right, string result, char sign, bool isManyZeroes)
        {
            for (int i = 0; i <= 9; i++)
            {
                if (repeats[i] == 1 || (i == 0 && isManyZeroes)) continue;

                string leftTemp = left.Replace('?', (char)(i + 48));
                string rightTemp = right.Replace('?', (char)(i + 48));
                string resultTemp = result.Replace('?', (char)(i + 48));

                bool correct = false;
                switch (sign)
                {
                    case '+':
                        correct = int.Parse(leftTemp) + int.Parse(rightTemp) == int.Parse(resultTemp);
                        break;
                    case '-':
                        correct = int.Parse(leftTemp) - int.Parse(rightTemp) == int.Parse(resultTemp);
                        break;
                    case '*':
                        correct = int.Parse(leftTemp) * int.Parse(rightTemp) == int.Parse(resultTemp);
                        break;
                }
                if (correct)
                {
                    return i;
                }
            }

            return -1;
        }
        public static int SolveExpression(string expression)
        {
            string left = "";
            string right = "";
            string result = "";

            int index = 0;
            bool stop = false;
            char sign = ' ';
            int[] repeats = new int[10];
            for (int i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '*' || expression[i] == '+' || (expression[i] == '-' && i > 0 && !stop))
                {
                    left = expression.Substring(0, i);
                    index = i;
                    sign = expression[i];
                    stop = true;
                }

                if (expression[i] == '=')
                {
                    right = expression.Substring(index + 1, i - index - 1);
                    result = expression.Substring(i + 1, expression.Length - 1 - i);
                    break;
                }

                if (char.IsDigit(expression[i]))
                {
                    repeats[expression[i] - '0'] = 1;
                }
            }
            return Check(repeats, left, right, result, sign, IsManyZeroes(left, right, result));
        }

        /// <summary>
        /// https://www.codewars.com/kata/large-factorials
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public static string Reverse(string line)
        {
            char[] lines = line.ToCharArray();
            Array.Reverse(lines);
            return new string(lines);
        }
        public static string Multiply(string x, string y)
        {
            string top = x.Length >= y.Length ? Reverse(x) : Reverse(y);
            string bottom = y.Length <= x.Length ? Reverse(y) : Reverse(x);
            StringBuilder composition = new StringBuilder(new string('0', x.Length + y.Length + 1));

            int countNumbers = x.Length + y.Length + 1;
            for (int i = 0; i < bottom.Length; i++)
            {
                int multiplyTens = 0;
                int summaryTens = 0;
                for (int j = 0; j < top.Length; j++)
                {
                    int res = (bottom[i] - '0') * (top[j] - '0');
                    int temp = composition[i + j] - '0';
                    composition[i + j] = (char)(((res + multiplyTens) % 10 + temp + summaryTens) % 10 + 48);

                    if (i > 0)
                    {
                        summaryTens = ((res + multiplyTens) % 10 + temp + summaryTens) / 10;
                    }

                    if (j == top.Length - 1)
                    {
                        composition[i + j + 1] = (char)((res + multiplyTens) / 10 + summaryTens + 48);
                    }

                    multiplyTens = (res + multiplyTens) / 10;
                }
            }

            string reverse = Reverse(composition.ToString());
            int k = 0;

            while (reverse[k++] == '0' && k < reverse.Length)
            {
                countNumbers--;
            }

            return reverse.Substring(x.Length + y.Length + 1 - countNumbers, countNumbers);
        }
        public static string Factorial(int n)
        {
            string result = "1";

            if (n < 0)
            {
                return null;
            }

            for (int i = 2; i < n + 1; i++)
            {
                result = Multiply(result, i.ToString());
            }

            return result;
        }

        /// <summary>
        /// https://www.codewars.com/kata/next-bigger-number-with-the-same-digits
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long NextBiggerNumber(long n)
        {
            StringBuilder number = new StringBuilder(n.ToString());

            int index = number.Length - 1;
            int min = int.MaxValue;
            int positionOfMinimum = 0;
            for (int i = number.Length - 1; i > 0; i--)
            {
                if (number[i - 1] - '0' >= number[i] - '0')
                {
                    index = i - 1;
                }
                else
                {
                    break;
                }
            }

            if (index == 0)
            {
                return -1;
            }

            for (int i = number.Length - 1; i >= index; i--)
            {
                if (number[i] - '0' < min && number[i] - '0' > number[index - 1] - '0')
                {
                    min = number[i] - '0';
                    positionOfMinimum = i;
                }
            }

            char temp = number[positionOfMinimum];
            number.Replace(number[positionOfMinimum], number[index - 1], positionOfMinimum, 1);
            number.Replace(number[index - 1], temp, index - 1, 1);

            int tempIndex = index;
            for (int i = 0; i < (number.Length - index) / 2; i++)
            {
                temp = number[tempIndex];
                number.Replace(number[tempIndex], number[number.Length - 1 - i], tempIndex++, 1);
                number.Replace(number[number.Length - 1 - i], temp, number.Length - 1 - i, 1);
            }

            return long.Parse(number.ToString());
        }

        /// <summary>
        /// https://www.codewars.com/kata/valid-braces
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public static bool IsOpen(char symbol)
        {
            if (symbol == '[' || symbol == '{' || symbol == '(')
                return true;
            return false;
        }
        public static bool ValidBraces(string braces)
        {
            Dictionary<char, char> Table = new Dictionary<char, char>()
            {
                {'[', ']'},
                {']', '['},
                {')', '('},
                {'(', ')'},
                {'{', '}'},
                {'}', '{'}
            };

            Stack<char> storage = new Stack<char>();
            for (int i = 0; i < braces.Length; i++)
            {
                if (storage.Count == 0)
                {
                    storage.Push(braces[i]);
                }
                else
                {
                    if (storage.Peek() == Table[braces[i]] && IsOpen(storage.Peek()))
                    {
                        storage.Pop();
                    }
                    else
                    {
                        storage.Push(braces[i]);
                    }
                }
            }

            if (storage.Count == 0)
                return true;

            return false;
        }

        /// <summary>
        /// https://www.codewars.com/kata/sum-strings-as-numbers
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static string SumStrings(string x, string y)
        {
            string top = x.Length >= y.Length ? Reverse(x) : Reverse(y);
            string bottom = y.Length <= x.Length ? Reverse(y) : Reverse(x);
            StringBuilder summary = new StringBuilder(new string('0', top.Length + 1));

            int summaryTens = 0;
            for (int i = 0; i < bottom.Length; i++)
            {
                int res = (bottom[i] - '0') + (top[i] - '0');
                summary[i] = (char)((res + summaryTens) % 10 + 48);
                summaryTens = (res + summaryTens) / 10;
            }
            for (int i = bottom.Length; i < top.Length; i++)
            {
                summary[i] = (char)((top[i] - '0' + summaryTens) % 10 + 48);
                summaryTens = (top[i] - '0' + summaryTens) / 10;
            }
            summary[top.Length] = (char)(summaryTens + 48);

            string reverse = Reverse(summary.ToString());
            if (reverse[0] == '0' && reverse[1] == '0')
                return reverse.Substring(2, reverse.Length - 2);
            if (reverse[0] == '0')
                return reverse.Substring(1, reverse.Length - 1);

            return reverse;
        }

        /// <summary>
        /// https://www.codewars.com/kata/pascals-triangle
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static List<int> PascalsTriangle(int n)
        {
            List<int> result = new List<int>();

            int k = 0;
            int currentLevel = 0;
            while (currentLevel < n)
            {
                for (int j = 0; j < currentLevel + 1; j++)
                {
                    if (j == 0 || j == currentLevel)
                    {
                        result.Add(1);
                    }
                    else
                    {
                        result.Add(result[k - currentLevel] + result[k - currentLevel - 1]);
                    }
                    k++;
                }
                currentLevel++;
            }

            return result;
        }

        /// <summary>
        /// https://www.codewars.com/kata/counting-change-combinations
        /// </summary>
        /// <param name="money"></param>
        /// <param name="coins"></param>
        /// <returns></returns>
        public static int CountCombinations(int money, int[] coins)
        {
            Stack<KeyValuePair<int, int>> coinsWays = new Stack<KeyValuePair<int, int>>();

            foreach (int item in coins)
            {
                coinsWays.Push(new KeyValuePair<int, int>(item, item));
            }

            int count = 0;
            while (coinsWays.Count != 0)
            {
                KeyValuePair<int, int> current = coinsWays.Pop();

                if (current.Value > money)
                {
                    continue;
                }

                if (current.Value == money)
                {
                    count++;
                    continue;
                }

                foreach (int item in coins)
                {
                    if (item <= current.Key)
                    {
                        coinsWays.Push(new KeyValuePair<int, int>(item, current.Value + item));
                    }
                }
            }

            return count;
        }

        /// <summary>
        /// https://www.codewars.com/kata/reverse-polish-notation-calculator
        /// </summary>
        /// <param name="expr"></param>
        /// <returns></returns>
        public static double Evaluate(String expr)
        {
            if (string.IsNullOrEmpty(expr))
            {
                return 0;
            }

            string[] symbols = expr.Split(' ');
            Stack<string> inputs = new Stack<string>();

            int index = 0;
            while (index < symbols.Length)
            {
                if (symbols[index] == "+" || symbols[index] == "-" || symbols[index] == "*" || symbols[index] == "/")
                {
                    double b = double.Parse(inputs.Pop());
                    double a = double.Parse(inputs.Pop());

                    switch (symbols[index++])
                    {
                        case "+":
                            inputs.Push((a + b).ToString());
                            continue;
                        case "-":
                            inputs.Push((a - b).ToString());
                            continue;

                        case "*":
                            inputs.Push((a * b).ToString());
                            continue;

                        case "/":
                            inputs.Push((a / b).ToString());
                            continue;
                    }
                }
                inputs.Push(symbols[index]);
                index++;
            }

            return double.Parse(inputs.Pop());
        }
    }
}
