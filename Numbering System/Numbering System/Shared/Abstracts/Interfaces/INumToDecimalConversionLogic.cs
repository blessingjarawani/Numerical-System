using Numbering_System.Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Numbering_System.Shared.Abstracts.Interfaces
{
    public interface INumToDecimalConversionLogic
    {
         Task<string> ConvertNumToDecimal(string value, Base baseNum);

    }
}
