using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a demux with k outputs, each output with n wires. The input also has n wires.

    class BitwiseMultiwayDemux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Input { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Outputs { get; private set; }

        //your code here

        public BitwiseMultiwayDemux(int iSize, int cControlBits)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Outputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            BitwiseDemux[] demux = new BitwiseDemux[(int)Math.Pow(2, cControlBits) - 1];
            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i] = new WireSet(Size);
            }
            int maxDemux = 0;

            for (int i = 1; i < cControlBits; i++)
            {
                for (int currentDemux = 0; currentDemux <= maxDemux; currentDemux++)
                {
                    int IN = currentDemux * 2 + 1;
                    if (currentDemux == 0)
                    {
                        demux[0] = new BitwiseDemux(Size);
                        demux[0].ConnectInput(Input);
                        demux[0].ConnectControl(Control[cControlBits - 1]);
                    }
                    demux[IN] = new BitwiseDemux(Size);
                    demux[IN].ConnectInput(demux[currentDemux].Output1);
                    demux[IN].ConnectControl(Control[cControlBits-2]);
                    
                    demux[IN+1] = new BitwiseDemux(Size);
                    demux[IN+1].ConnectInput(demux[currentDemux].Output2);
                    demux[IN+1].ConnectControl(Control[cControlBits-2]);
                }
                cControlBits--;
                maxDemux += (int) Math.Pow(2, i);
            }
            int c = 0;
            for (int j=((demux.Length+1)/2)-1;j<demux.Length;j++){
                Outputs[c].ConnectInput(demux[j].Output1);
                Outputs[c + 1].ConnectInput(demux[j].Output2);
                c=c+2;
            }
        }


        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }


        public override bool TestGate()
        {
            Console.WriteLine("TESTING MULTIDEMUX..Is it true?    : " + this.Outputs[0].ToString());
            return true;
        }
    }
}
