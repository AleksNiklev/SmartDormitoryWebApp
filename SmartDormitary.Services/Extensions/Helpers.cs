using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SmartDormitary.Services.Extensions
{
    internal static class Helpers
    {
        internal static int[] GetNumbersFromString(string input)
        {
            var result = new List<int>();
            var numbers = Regex.Split(input, @"\D+");
            foreach (var value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    result.Add(int.Parse(value));
                }
            }

            return result.ToArray();
        }
    }
}