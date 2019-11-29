using Numbering_System.Logic;
using Numbering_System.Models;
using Numbering_System.Shared.Abstracts.Interfaces;
using Numbering_System.Shared.Dictionary;
using Numbering_System.Shared.Service;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Test
{
    public class Tests
    {
        IConversionService conversionService;
        IDecimalToAnyBaseLogic decimalToAnyBaseLogic;
        INumToDecimalConversionLogic numToDecimalConversionLogic;
        Number number;

        [SetUp]
        public void Setup()
        {
            decimalToAnyBaseLogic = new DecimalToAnyBaseLogic();
            numToDecimalConversionLogic = new NumToDecimalLogic();
            conversionService = new ConversionService(numToDecimalConversionLogic, decimalToAnyBaseLogic);
            number = new Number
            {
                Base = Base.Decimal,
                Value = "291"
            };
        }

        [Test]
        public async Task DecimalToOtherBases()
        {
            var result = await conversionService.DecimalToAnyBases(number);
            Assert.AreEqual("291", result.DecimalValue);
            Assert.AreEqual("0443", result.OctalValue);
            Assert.AreEqual("0x123", result.HexaDecimalValue);
            Assert.AreEqual("100100011", result.BinaryValue);


        }
        [Test]
        public async Task OctalToAnyyBase()
        {
            number.Base = Base.Octal;
            number.Value = "0443";
            var result = await conversionService.OctalToAnyBases(number);
            Assert.AreEqual("291", result.DecimalValue);
            Assert.AreEqual("0443", result.OctalValue);
            Assert.AreEqual("0x123", result.HexaDecimalValue);
            Assert.AreEqual("100100011", result.BinaryValue);



        }

            [Test]
        //public async Task FractionDecimalToanyBase()
        //{
        //    number.Base = Base.Decimal;
        //    number.Value = "123.456";
        //    var result = await conversionService.DecimalToAnyBases(number);
        //    Assert.AreEqual("123.456", result.DecimalValue);
        //    Assert.AreEqual("0173.3513615237",result.OctalValue);
        //    Assert.AreEqual("0x7B.74BC6A7EF9",result.HexaDecimalValue);
        //    Assert.AreEqual("1111011.0111010010", result.BinaryValue);


        //}
    }
}