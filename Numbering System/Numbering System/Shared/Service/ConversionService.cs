using Numbering_System.Models;
using Numbering_System.Shared.Abstracts.Interfaces;
using Numbering_System.Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Numbering_System.Shared.Service
{
    public class ConversionService : IConversionService
    {
        private IDecimalToAnyBaseLogic _decimalToAnyBaseLogic;
        private INumToDecimalConversionLogic _numToDecimalConversionLogic;
        public ConversionService(INumToDecimalConversionLogic numToDecimalConversionLogic, IDecimalToAnyBaseLogic decimalToAnyBaseLogic)
        {
            _decimalToAnyBaseLogic = decimalToAnyBaseLogic;
            _numToDecimalConversionLogic = numToDecimalConversionLogic;
        }

        private async Task<string> DecimalToAnyBase(Number number, Base toBase)
        {

            return await _decimalToAnyBaseLogic.ConvertDecimalToAnyBase(number.DecimalValue, toBase);

        }

        private async Task<string> NumberToDecimal(Number number)
        {
            return await _numToDecimalConversionLogic.ConvertNumToDecimal(number.Value, number.Base);

        }


        public async Task<Number> DecimalToAnyBases(Number number)
        {
            var result = Validate(number);
            if (string.IsNullOrEmpty(result))
            {
                number.DecimalValue = number.Value;
                number.OctalValue = await this.DecimalToAnyBase(number, Base.Octal);
                number.HexaDecimalValue = await this.DecimalToAnyBase(number, Base.HexaDecimal);
                number.BinaryValue = await this.DecimalToAnyBase(number, Base.Binary);
                return number;
            }

            return new Number
            {
                Errors = result
            };

        }
        public async Task<Number> OctalToAnyBases(Number number)
        {
            var result = Validate(number);
            if (string.IsNullOrEmpty(result))
            {
                number.Value = number.Value.Substring(1, number.Value.Length - 1);
                number.OctalValue = $"0{ number.Value}";
                number.DecimalValue = await this.NumberToDecimal(number);
                number.HexaDecimalValue = await this.DecimalToAnyBase(number, Base.HexaDecimal);
                number.BinaryValue = await this.DecimalToAnyBase(number, Base.Binary);
                return number;
            }
            return new Number
            {
                Errors = result
            };

        }

        public async Task<Number> HexaToAnyBases(Number number)
        {
            var result = Validate(number);
            if (string.IsNullOrWhiteSpace(result))
            {
                number.Value = number.Value.Substring(2, number.Value.Length - 2);
                number.HexaDecimalValue = $"0x{ number.Value}";
                number.DecimalValue = await this.NumberToDecimal(number);
                number.OctalValue = await this.DecimalToAnyBase(number, Base.Octal);
                number.BinaryValue = await this.DecimalToAnyBase(number, Base.Binary);
                return number;
            }
            return new Number
            {
                Errors = result
            };

        }

        public void Display(Number number)
        {
            Console.WriteLine($"Decimal : {number.DecimalValue}");
            Console.WriteLine($"Octal   : {number.OctalValue}");
            Console.WriteLine($"HexaDec : {number.HexaDecimalValue}");
            Console.WriteLine($"Binary  : {number.BinaryValue}");
            if (!string.IsNullOrWhiteSpace(number.Errors))
                Console.WriteLine(number.Errors);
            else Console.WriteLine("");
        }
        public async Task<Number> BinaryToAnyBases(Number number)
        {
            var result = Validate(number);
            if (string.IsNullOrEmpty(result))
            {
                number.DecimalValue = await this.NumberToDecimal(number);
                number.OctalValue = await this.DecimalToAnyBase(number, Base.Octal);
                number.HexaDecimalValue = await this.DecimalToAnyBase(number, Base.HexaDecimal);
                return number;
            }
            return new Number
            {
                Errors = result
            };

        }

        private string Validate(Number number)
        {
            if (number == null || string.IsNullOrWhiteSpace(number?.Value ?? null))
            {
                return "Invalid Number";
            }

            if (number.Value.Contains(" "))
            {
                return "Spaces Are not Allowed";
            }
            
            if (number.Base == Base.Octal)
            {
                if (number.Value.Contains("8"))
                {
                    return "Invalid Number";
                }
                if (!number.Value.ToUpper().StartsWith("0"))
                {
                    return "Invalid Octal Number";
                }
            }
            if (number.Base == Base.Binary)
            {
                if (int.Parse(number.Value) >=2)

                {
                    return "Invalid Binary Number";
                }
            }

            if (number.Base == Base.HexaDecimal)
            {
                if (!number.Value.ToUpper().StartsWith("0X"))
                {
                    return "Invalid Hexadecimal Number";
                }

            }
            return "";
        }
    }
}
