using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a FullAdder, taking as input 3 bits - 2 numbers and a carry, and computing the result in the output, and the carry out.
    class FullAdder : TwoInputGate
    {
        public Wire CarryInput { get; private set; }
        public Wire CarryOutput { get; private set; }
        
        public FullAdder()
        {
            CarryInput = new Wire();
            CarryOutput = new Wire();
            
            var halfAdd1 = new HalfAdder();
            var halfAdd2 = new HalfAdder();
            var gOr = new OrGate();

            halfAdd2.ConnectInput2(CarryInput);
            halfAdd2.ConnectInput1(halfAdd1.Output);
            
            gOr.ConnectInput1(halfAdd1.CarryOutput);
            gOr.ConnectInput2(halfAdd2.CarryOutput);
            
            Input1 = halfAdd1.Input1;
            Input2 = halfAdd1.Input2;
            
            CarryOutput = gOr.Output;
            Output = halfAdd2.Output;
        }


        public override string ToString()
        {
            return Input1.Value + "+" + Input2.Value + " (C" + CarryInput.Value + ") = " + Output.Value + " (C" + CarryOutput.Value + ")";
        }

        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            CarryInput.Value = 0;
            if ((Output.Value != 0) || (CarryOutput.Value != 0))
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            CarryInput.Value = 0;
            if ((Output.Value !=1) || (CarryOutput.Value != 0))
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            CarryInput.Value = 0;
            if ((Output.Value != 1) || (CarryOutput.Value != 0))
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            CarryInput.Value = 0;
            if ((Output.Value != 0) || (CarryOutput.Value != 1))
                return false;
            return true;
        }
    }
}
