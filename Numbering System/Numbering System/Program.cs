using Microsoft.Extensions.DependencyInjection;
using Numbering_System.Logic;
using Numbering_System.Models;
using Numbering_System.Shared.Abstracts.Interfaces;
using Numbering_System.Shared.Dictionary;
using Numbering_System.Shared.Service;
using System;
using System.Threading.Tasks;

namespace Numbering_System
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                Console.WriteLine("WELCOME");
                var service = new ServiceCollection();
                ConfigureServices(service);
                var serviceProvider = service.BuildServiceProvider();
                var conService = serviceProvider.GetService<ConversionService>();
                Console.WriteLine("Enter Decimal Number eg 10 or 10.5");
                var input = Console.ReadLine();
                var numObject = new Number
                {
                    Base = Base.Decimal,
                    Value = input,
                };

                var result = await conService.DecimalToAnyBases(numObject);
                conService.Display(result);
                Console.ReadLine();


                Console.WriteLine("Enter Octal Number eg 0123 or 0123.45");
                input = Console.ReadLine();
                numObject = new Number
                {
                    Base = Base.Octal,
                    Value = input,
                };
                result = await conService.OctalToAnyBases(numObject);
                conService.Display(result);
                Console.ReadLine();


                Console.WriteLine("Enter Hexadecimal Number eg 0X10 or 0X0123.45");
                input = Console.ReadLine();
                numObject = new Number
                {
                    Base = Base.HexaDecimal,
                    Value = input,
                };
                result = await conService.HexaToAnyBases(numObject);
                conService.Display(result);
                Console.ReadLine();

                Console.WriteLine("Enter Binary Number eg 01010 or 0101.00");
                input = Console.ReadLine();
                numObject = new Number
                {
                    Base = Base.Binary,
                    Value = input,
                };
                result = await conService.BinaryToAnyBases(numObject);
                conService.Display(result);
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }


        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IConversionService, ConversionService>();
            serviceCollection.AddTransient<IDecimalToAnyBaseLogic, DecimalToAnyBaseLogic>();
            serviceCollection.AddTransient<INumToDecimalConversionLogic, NumToDecimalLogic>();
            serviceCollection.AddTransient<ConversionService>();
        }

    }
}
