using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BellTest
{
    // An object which wraps the InstrumentMode enum so it can be easily displayed in a combo box without auto-boxing.
    public struct InstrumentModeValue
    {
        public InstrumentMode Value;

        public InstrumentModeValue(InstrumentMode value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Enum.GetName(typeof(InstrumentMode), Value);
        }
    }
}
