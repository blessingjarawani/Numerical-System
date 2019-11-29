using Numbering_System.Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Numbering_System.Shared.Abstracts.Interfaces
{
    public interface IDecimalToAnyBaseLogic
    {
        Task<string> ConvertDecimalToAnyBase(string decValue, Base toBase);
    }
}
