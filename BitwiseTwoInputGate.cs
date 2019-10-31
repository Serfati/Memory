using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //base class for all bitwise gates with two inputs
    //A two input bitwise gate takes as input two WireSets containing n wires, and computes a bitwise function - z_i=f(x_i,y_i)
    abstract class BitwiseTwoInputGate : Gate
    {
        public WireSet Input1 { get; protected set; }
        public WireSet Input2 { get; protected set; }
        public WireSet Output { get; protected set; }
        public int Size { get; private set; }

        public BitwiseTwoInputGate(int iSize)
        {
            Size = iSize;
            Input1 = new WireSet(iSize);
            Input2 = new WireSet(iSize);
            Output = new WireSet(iSize);
        }

        public void ConnectInput1(WireSet wInput)
        {
            Input1.ConnectInput(wInput);
        }
        public void ConnectInput2(WireSet wInput)
        {
            Input2.ConnectInput(wInput);
        }

    }
}
