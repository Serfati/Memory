using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A two input bitwise gate takes as input two WireSets containing n wires, and computes a bitwise function - z_i=f(x_i,y_i)
    class BitwiseAndGate : BitwiseTwoInputGate
    {
        public BitwiseAndGate(int iSize)
            : base(iSize)
        {
            AndGate[] mgAnd=new AndGate[iSize];
            for (int i = 0; i < iSize; i++)
                mgAnd[i] = new AndGate();

            for (int i = 0; i < iSize; i++)
            {
                mgAnd[i].ConnectInput1(Input1[i]);
                mgAnd[i].ConnectInput2(Input2[i]);
                Output[i].ConnectInput(mgAnd[i].Output);
            }
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(and)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "And " + Input1 + ", " + Input2 + " -> " + Output;
        }

        public override bool TestGate()
        {
            var ws1 = new WireSet(Size);
            for (int i = 0; i < Math.Pow(2, Size); i++)
            {
                ws1.SetValue(i);
                for (int j = 0; j < Math.Pow(2, Size); j++)
                {
                    var mgAnd = new BitwiseAndGate(Size);
                    var ws2 = new WireSet(Size);
                    ws2.SetValue(j);
                    mgAnd.ConnectInput1(ws1);
                    mgAnd.ConnectInput2(ws2);
                    for (int k = 0; k < ws2.Size; k++)
                    {
                        if (mgAnd.Output[k].Value != (ws1[k].Value & ws2[k].Value))
                             return false;
                    }
                }
            }
            return true;
        }
    }
}
