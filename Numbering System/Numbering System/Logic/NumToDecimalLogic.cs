using Numbering_System.Shared.Abstracts.Interfaces;
using Numbering_System.Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Numbering_System.Logic
{
    public class NumToDecimalLogic : INumToDecimalConversionLogic
    {
        private const int asciiDiff = 48;
        int BaseToDecimal(string value, int baseNum)
        {
            int decimalNum = 0;
            int b;
            int iProduct = 1;
            string hexaSymbol = "";
            if (baseNum > (int)Base.Decimal)
                for (int i = 0; i < HexaSymbols.HexaCharacters.Length; i++)
                    hexaSymbol += HexaSymbols.HexaCharacters.GetValue(i).ToString();
            for (int i = value.Length - 1; i >= 0; i--, iProduct *= baseNum)
            {
                string stringValue = value[i].ToString();
                if (stringValue.IndexOfAny(HexaSymbols.HexaCharacters) >= 0)
                    b = HexaSymbols.HexaNumeric[hexaSymbol.IndexOf(value[i])];
                else
                    b = (int)value[i] - asciiDiff;
                decimalNum += (b * iProduct);
            }
            return decimalNum;
        }

       private double FractionToDecimal(string value, int baseNum)
        {
            double decimalNum = 0;
            int b;
            double iProduct = -1;
            string hexaSymbol = "";
            if (baseNum > (int)Base.Decimal)
                for (int i = 0; i < HexaSymbols.HexaCharacters.Length; i++)
                    hexaSymbol += HexaSymbols.HexaCharacters.GetValue(i).ToString();
            for (int i = 0; i <=value.Length - 1; i++)
            {
               
                string stringValue = value[i].ToString();
                if (stringValue.IndexOfAny(HexaSymbols.HexaCharacters) >= 0)
                    b = HexaSymbols.HexaNumeric[hexaSymbol.IndexOf(value[i])];
                else
                    b = (int)value[i] - asciiDiff;
                decimalNum += (b * Math.Pow(baseNum,iProduct));
                iProduct += -1;
            }
            return decimalNum;
        }

        public async Task<string> ConvertNumToDecimal(string value, Base baseNum)
        {
            try
            {
                var splittedNum = value.Split(".");
                double result =  await Task.Run(()=>this.BaseToDecimal(splittedNum[0], (int)baseNum));
                result += splittedNum?.Count() > 1 ?  await Task.Run(()=>this.FractionToDecimal(splittedNum[1], (int)baseNum)) : 0;
                return result.ToString();
            }
            catch (Exception ex)
            {
                return ex.GetBaseException().Message;
            }

        }

    }
}
