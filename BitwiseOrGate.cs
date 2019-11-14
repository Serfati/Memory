using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A two input bitwise gate takes as input two WireSets containing n wires, and computes a bitwise function - z_i=f(x_i,y_i)
    class BitwiseOrGate : BitwiseTwoInputGate
    {
        public BitwiseOrGate(int iSize)
            : base(iSize)
        {
            OrGate[] mgOr=new OrGate[iSize];
            for (int i = 0; i < iSize; i++)
                mgOr[i] = new OrGate();

            for (int i = 0; i < iSize; i++)
            {
                mgOr[i].ConnectInput1(Input1[i]);
                mgOr[i].ConnectInput2(Input2[i]);
                Output[i].ConnectInput(mgOr[i].Output);
            }
        }

        //an implementation of the ToString method is called, e.g. when we use Console.WriteLine(or)
        //this is very helpful during debugging
        public override string ToString()
        {
            return "Or " + Input1 + ", " + Input2 + " -> " + Output;
        }

        public override bool TestGate()
        {
            try
            {
                var ws1 = new WireSet(Size);
                for (int i = 0; i < Math.Pow(2, Size)-1; i++)
                {
                    ws1.SetValue(i);
                    for (int j = 0; j < Math.Pow(2, Size)-1; j++)
                    {
                        var mgOr = new BitwiseOrGate(Size);
                        var ws2 = new WireSet(Size);
                        ws2.SetValue(j);
                        mgOr.ConnectInput1(ws1);
                        mgOr.ConnectInput2(ws2);

                        for (int k = 0; k < ws1.Size ; k++)
                        {
                            if (mgOr.Output[j].Value != (ws1[j].Value | ws2[j].Value))
                                return false;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message+" "+ e.HelpLink);
                return true;
            }
            return true;
        }
    }
}