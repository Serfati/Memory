using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This gate implements the xor operation. To implement it, follow the example in the And gate.
    class XorGate : TwoInputGate
    {
        //A XOR B = (A ^ ~B)U(~A ^ B)
        public XorGate()
        {
            //init the gates
            var mGNot1 = new NotGate();
            var mGNot2 = new NotGate();
            var mGAnd1 = new AndGate();
            var mGAnd2 = new AndGate();
            var mGOr = new OrGate();
            //wire 
            mGAnd1.ConnectInput1(mGNot1.Output);
            mGAnd1.ConnectInput2(mGNot2.Input);
            mGAnd2.ConnectInput1(mGNot1.Input);
            mGAnd2.ConnectInput2(mGNot2.Output);
            mGOr.ConnectInput1(mGAnd1.Output);
            mGOr.ConnectInput2(mGAnd2.Output);
            
            //set the inputs and the output of the xor gate
            Output = mGOr.Output;
            Input1 = mGNot1.Input;
            Input2 = mGNot2.Input;
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(xor)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Xor " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
        }


        //this method is used to test the gate. 
        //we simply check whether the truth table is properly implemented.
        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            if (Output.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 0)
                return false;
            return true;
        }
    }
}
