using Numbering_System.Shared.Abstracts.Interfaces;
using Numbering_System.Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numbering_System.Logic
{
    public class DecimalToAnyBaseLogic : IDecimalToAnyBaseLogic
    {
        string DecimalToBase(int decValue, int numbase)
        {
            string strBin = "";
            int[] result = new int[32];
            int MaxBit = 32;
            for (; decValue > 0; decValue /= numbase)
            {
                int rem = decValue % numbase;
                result[--MaxBit] = rem;
            }
            for (int i = 0; i < result.Length; i++)
                if ((int)result.GetValue(i) >= (int)Base.Decimal)
                    strBin += HexaSymbols.HexaCharacters[(int)result.GetValue(i) % (int)Base.Decimal];
                else
                    strBin += result.GetValue(i);
            strBin = strBin.TrimStart(new char[] { '0' });
            return strBin;
        }

        string FractionToBase(string decValue, int numbase)
        {
            var decFraction = $"0.{decValue}";
            var strBin = "";
            var  result = new List<string>();
            var maxBits = 32;
            var counter  = 0;
            while (Double.Parse(decFraction) != 0 && counter < maxBits-1)
            { 
                var answer = Double.Parse(decFraction) * numbase;
                var splittedAnswer = answer.ToString().Split(".");
                result.Add(splittedAnswer[0]);
                decFraction = splittedAnswer?.Count() > 1 ? $"0.{splittedAnswer[1]}" : "0";
                counter += 1;
            }

            foreach (var num in result)
                if (int.Parse(num) >= (int)Base.Decimal)
                    strBin += HexaSymbols.HexaCharacters[int.Parse(num) % (int)Base.Decimal];
                else
                    strBin += num;
           
            return strBin;
        }

        public async Task<string> ConvertDecimalToAnyBase(string decValue, Base toBase)
        {
            try
            {
                var splittedNum = decValue.Split(".");
                var result = await Task.Run(()=> this.DecimalToBase(int.Parse(splittedNum[0]), (int)toBase));
                result += splittedNum?.Count() > 1 ? $".{await Task.Run(()=>this.FractionToBase((splittedNum[1]), (int)toBase)) }" : "";
                return formatResult(result, toBase);
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }

        }

        private string formatResult(string result, Base toBase)
        {
            var strResult = "";
            switch (toBase)
            {
                case Base.HexaDecimal:
                    {
                        strResult += $"0x{result}";
                        break;
                    }
                case Base.Octal:
                    {
                        strResult +=$"0{result}";
                        break;
                    }
                default: {
                        strResult = result;
                        break;
                    }
            }
            return strResult;
        }
    }
}
