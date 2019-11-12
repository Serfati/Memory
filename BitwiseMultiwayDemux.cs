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
            BitwiseDemux[] demux = new BitwiseDemux[Outputs.Length-1];
            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i] = new WireSet(Size);
            }

            int maxDemux = 0;
          
            for (int i = 1; i < cControlBits; cControlBits--, i++, maxDemux += (int) Math.Pow(2, i))
            { 
                int currentDemux = 0;
               while (currentDemux <= maxDemux )
                {
                    int IN = currentDemux * 2 + 1;
                    demux[IN] = new BitwiseDemux(Size);
                    demux[IN+1] = new BitwiseDemux(Size);
                    if (currentDemux == 0)
                    {
                        demux[0] = new BitwiseDemux(Size);
                        demux[0].ConnectInput(Input);
                        demux[0].ConnectControl(Control[cControlBits - 1]);
                    }
                    demux[IN+1].ConnectInput(demux[currentDemux].Output2);
                    demux[IN+1].ConnectControl(Control[cControlBits-2]);
                    
                    demux[IN].ConnectInput(demux[currentDemux].Output1);
                    demux[IN].ConnectControl(Control[cControlBits-2]);
                    
                    currentDemux++;
                }
            }
            int place = demux.Length, position = demux.Length / 2, j = 0;
            for (int i = 0; i < place / 2 + 1; i++, j += 2, position++)
            {
                Outputs[j].ConnectInput(demux[position].Output1);
                Outputs[j + 1].ConnectInput(demux[position].Output2);
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
            return true;
        }
    }
}
