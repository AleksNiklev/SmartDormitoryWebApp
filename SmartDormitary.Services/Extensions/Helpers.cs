using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SmartDormitary.Services.Extensions
{
    internal static class Helpers
    {
        internal static double[] GetNumbersFromString(string input)
        {
            var result = new List<double>();
            var numbers = input.Split(' ');

            foreach (string num in numbers)
            {
                double numValue;
                if (double.TryParse(num, out numValue))
                {
                    result.Add(numValue);
                }
            }

            return result.ToArray();
        }
    }
}