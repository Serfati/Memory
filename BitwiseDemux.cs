using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A bitwise gate takes as input WireSets containing n wires, and computes a bitwise function - z_i=f(x_i)
    class BitwiseDemux : Gate
    {
        public int Size { get; private set; }
        public WireSet Output1 { get; private set; }
        public WireSet Output2 { get; private set; }
        public WireSet Input { get; private set; }
        public Wire Control { get; private set; }

        //your code here

        public BitwiseDemux(int iSize)
        {
            Size = iSize;
            Control = new Wire();
            Input = new WireSet(Size);
            Demux[] mgMux=new Demux[iSize];
            Output1 = new WireSet(Size);
            Output2 = new WireSet(Size);
            for (int i = 0; i < iSize; i++)
            {
                mgMux[i] = new Demux();
                mgMux[i].ConnectControl(Control);
                mgMux[i].ConnectInput(Input[i]);
                Output1[i].ConnectInput(mgMux[i].Output1);
                Output2[i].ConnectInput(mgMux[i].Output2);
            }
        }

        public void ConnectControl(Wire wControl)
        {
            Control.ConnectInput(wControl);
        }
        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        public override bool TestGate()
        {
            for (int k = 1; k >= 0; k--)
            {
                Control.Value = k;
                for (int i = 0; i < Math.Pow(2, Size); i++)
                {
                    BitwiseDemux mgDemux = new BitwiseDemux(Size);
                    WireSet ws = new WireSet(Size);
                    ws.SetValue(i);
                    mgDemux.ConnectInput(ws);
                    mgDemux.ConnectControl(Control);

                    for (int j = Size - 1; j >= 0; j--)
                        if (k == 0)
                        {
                            if (mgDemux.Output1[j].Value != ws[j].Value || mgDemux.Output2[j].Value != 0) 
                                return false;
                        }
                        else if (mgDemux.Output1[j].Value != ws[j].Value ||
                                 mgDemux.Output2[j].Value != 0) return false;
                }
            }
            return true;
        }
    }
}