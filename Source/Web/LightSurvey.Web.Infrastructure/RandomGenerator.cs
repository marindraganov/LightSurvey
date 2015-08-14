using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightSurvey.Web.Infrastructure
{
    public static class RandomGenerator
    {
        
        private const string Letters = "ABCDEFGHIJKLMNOPQRSTUWXVYZabcdefghijklmnopqrstuwxvyz";
        private const string Digits = "0123456789";

        private static Random random = new Random();

        public static string RandomString(int minLength = 5, int maxLength = 50)
        {
            var result = new StringBuilder();
            var length = random.Next(minLength, maxLength + 1);
            for (int i = 0; i <= length; i++)
            {
                result.Append(Letters[random.Next(0, Letters.Length)]);
            }

            return result.ToString();
        }

        public static string RandomAlphaNumericSeq(int length)
        {
            var result = new StringBuilder();
            var chars = Letters + Digits;

            for (int i = 0; i <= length; i++)
            {
                result.Append(chars[random.Next(0, chars.Length)]);
            }

            return result.ToString();
        }

        public static int RandomNumber(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }
}
