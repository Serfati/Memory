using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //this class is provided as an example
    class AndGate : TwoInputGate
    {
        //we will use a nand and a not after it
        private NotGate m_gNot;
        private NAndGate m_gNand;

        public AndGate()
        {
            //init the gates
            m_gNand = new NAndGate();
            m_gNot = new NotGate();
            //wire the output of the nand gate to the input of the not
            m_gNot.ConnectInput(m_gNand.Output);
            //set the inputs and the output of the and gate
            Output = m_gNot.Output;
            Input1 = m_gNand.Input1;
            Input2 = m_gNand.Input2;
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(and)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "And " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
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
            if (Output.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            return true;
        }
    }
}
