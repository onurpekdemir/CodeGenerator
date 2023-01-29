using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class CodeGeneratorService
    {
        private const string _characterSet = "ACDEFGHKLMNPRTXYZ234579";
        private readonly IDictionary<char, int> _characterSetDict;
        private readonly int _characterSetLength = _characterSet.Length;
        public  int Count { get; set; }
        public  double Offset { get; set; } = Math.Pow(13, 5);
        public  double TopSeed { get; set; } 

        public CodeGeneratorService(int count)
        {
            Count = count;
            TopSeed = Offset * Count;

            _characterSetDict = _characterSet
               .Select((c, i) => new { Index = i, Char = c })
               .ToDictionary(c => c.Char, c => c.Index);
        }

        public string GenerateCode(double seed)
        {
            if (seed < _characterSetLength)
            {
                return _characterSet[0].ToString();
            }

            var sb = new StringBuilder();
            var i = seed;

            while (i > 0)
            {
                sb.Insert(0, _characterSet[(int)(i % _characterSetLength)]);
                i = Math.Floor(i / _characterSetLength);
            }

            var str = sb.ToString();
            return ReverseXor(str);
        }

        private static string ReverseXor(string s)
        {
            char[] charArray = s.ToCharArray();
            int len = s.Length - 1;

            for (int i = 0; i < len; i++, len--)
            {
                charArray[i] ^= charArray[len];
                charArray[len] ^= charArray[i];
                charArray[i] ^= charArray[len];
            }

            return new string(charArray);
        }

        public double VerifyCode(string reversed)
        {
            string code = ReverseXor(reversed);
            double total = 0;
            for (int i = 0; i < code.Length; i++)
            {
                total = total * _characterSetLength + _characterSetDict[code[i]];
            }
            return total;
        }
    }

}
