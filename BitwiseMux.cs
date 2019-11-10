using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseMux : BitwiseTwoInputGate
    { 
        public Wire ControlInput { get; private set; }
        public BitwiseMux(int iSize)
            : base(iSize)
        {
            ControlInput = new Wire();
            MuxGate[] mgMux=new MuxGate[iSize];
            
            for (int i = 0; i < iSize; i++)
            {
                mgMux[i] = new MuxGate();
                mgMux[i].ConnectControl(ControlInput);
                mgMux[i].ConnectInput1(Input1[i]);
                mgMux[i].ConnectInput2(Input2[i]);
                Output[i].ConnectInput(mgMux[i].Output);
            }
        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }



        public override string ToString()
        {
            return "Mux " + Input1 + "," + Input2 + ",C" + ControlInput.Value + " -> " + Output;
        }




        public override bool TestGate()
        {
            for (int k = 1; k >= 0; k--)
            {
                ControlInput.Value = k;
                for (int i = 0; i < Math.Pow(2, Size); i++)
                {
                    for (int j = 0; j < Math.Pow(2, Size); j++)
                    {
                        BitwiseMux mgMux = new BitwiseMux(Size);
                        WireSet ws2 = new WireSet(Size);
                        WireSet ws1 = new WireSet(Size);
                        ws1.SetValue(i);
                        ws2.SetValue(j);
                        mgMux.ConnectInput1(ws1);
                        mgMux.ConnectInput2(ws2);
                        mgMux.ConnectControl(ControlInput);
                        for (int f = Size - 1; f >= 0; f--)
                            if (k == 0)
                            {
                                if (mgMux.Output[f].Value == ws1[f].Value) continue;
                                return false;
                            }
                            else if (mgMux.Output[f].Value != ws2[f].Value)
                                return false;
                    }
                }
            }
            return true;
        }
    }
}
