using Numbering_System.Shared.Dictionary;
using System;
using System.Collections.Generic;
using System.Text;

namespace Numbering_System.Models
{
    public class Number
    {
        public string Value { get; set; }
        public Base Base { get; set; }
        public string DecimalValue { get; set; }
        public string BinaryValue { get; set; }
        public string OctalValue { get; set; }
        public string HexaDecimalValue { get; set; }
        public string Errors { get; set; }

    }
}
