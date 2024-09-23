using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yacht_Verf
{
    public class CustomButtonData
    {
        public string DisplayText { get; set; }
        public int Value { get; set; }
        public CustomButtonData(string displayText, int value)
        {
            DisplayText = displayText;
            Value = value;

        }
    }
}
