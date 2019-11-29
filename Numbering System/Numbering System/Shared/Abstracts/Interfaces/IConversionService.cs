using Numbering_System.Models;
using Numbering_System.Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Numbering_System.Shared.Abstracts.Interfaces
{
    public interface IConversionService
    {
        Task<Number> DecimalToAnyBases(Number number);
        Task<Number> OctalToAnyBases(Number number);
        Task<Number> HexaToAnyBases(Number number);
        Task<Number> BinaryToAnyBases(Number number);
        void Display(Number number);
    }
}
