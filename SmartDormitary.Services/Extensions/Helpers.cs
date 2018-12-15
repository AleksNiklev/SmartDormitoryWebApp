using System.Collections.Generic;

namespace SmartDormitary.Services.Extensions
{
    public static class Helpers
    {
        public static double[] GetNumbersFromString(string input)
        {
            var result = new List<double>();
            var numbers = input.Split(' ');

            foreach (var num in numbers)
                if (double.TryParse(num, out var numValue))
                    result.Add(numValue);

            return result.ToArray();
        }
    }
}