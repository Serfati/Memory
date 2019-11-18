using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a HalfAdder, taking as input 2 bits - 2 numbers and computing the result in the output, and the carry out.

    class HalfAdder : TwoInputGate
    {
        public Wire CarryOutput { get; }


        public HalfAdder()
        {
            CarryOutput = new Wire();
            
            var gXor = new XorGate();
            var gAnd = new AndGate();
            
            Input1 = gAnd.Input1;
            Input2 = gAnd.Input2;
            
            gXor.ConnectInput1(Input1);
            gXor.ConnectInput2(Input2);

            Output = gXor.Output;
            CarryOutput = gAnd.Output;
        }


        public override string ToString()
        {
            return "HA " + Input1.Value + "," + Input2.Value + " -> " + Output.Value + " (C" + CarryOutput + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 1;
            Input2.Value = 1;
            if ((Output.Value != 0) || (CarryOutput.Value != 1))
                return false;
            Input1.Value = 0;
            Input2.Value = 0;
            if ((Output.Value != 0)||(CarryOutput.Value != 0))
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if ((Output.Value != 1) || (CarryOutput.Value != 0))
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if ((Output.Value != 1) || (CarryOutput.Value != 0))
                return false;
            return true;
        }
    }
}
