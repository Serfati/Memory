using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This bitwise gate takes as input one WireSet containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseNotGate : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        public int Size { get; private set; }

        //your code here

        public BitwiseNotGate(int iSize)
        {
            Input = new WireSet(iSize);
            Output = new WireSet(iSize);
            NotGate[] mgNot = new NotGate[iSize];
            for (int i = 0; i < iSize; i++)
                mgNot[i] = new NotGate();
            for (int i = 0; i < iSize; i++)
            {
                mgNot[i].ConnectInput(Input[i]);
                Output[i].ConnectInput(mgNot[i].Output);
            }
        }

        public void ConnectInput(WireSet ws)
        {
            Input.ConnectInput(ws);
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(not)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Not " + Input + " -> " + Output;
        }

        public override bool TestGate()
        {
            for (int j = 0; j < Math.Pow(2, Size); j++)
            {
                var mgNot = new BitwiseNotGate(Size);
                var ws = new WireSet(Size);
                ws.SetValue(j);
                mgNot.ConnectInput(ws);
                for (int k = ws.Size - 1; k >= 0; k--)
                {
                    if (mgNot.Output[k].Value != ws[k].Value)
                                return false;
                }
            }
            return true;
        }
    }
}
