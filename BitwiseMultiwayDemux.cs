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
            Outputs = new WireSet[(int) Math.Pow(2, cControlBits)];
            BitwiseDemux[] demux = new BitwiseDemux[Outputs.Length - 1];
            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i] = new WireSet(Size);
            }

            int maxDemux = 0;
            int currControl = cControlBits;
            for (int i = 1; i < currControl; currControl--, i++, maxDemux += (int) Math.Pow(2, i))
            {
                int currentDemux = 0;
                while (currentDemux <= maxDemux)
                {
                    int IN = currentDemux * 2 + 1;
                    demux[IN] = new BitwiseDemux(Size);
                    demux[IN + 1] = new BitwiseDemux(Size);
//                    if (currentDemux == 0)
//                    {
//                        demux[0] = new BitwiseDemux(Size);
//                        demux[0].ConnectInput(Input);
//                        demux[0].ConnectControl(Control[cControlBits - 1]);
//                    }
                    demux[IN].ConnectInput(demux[currentDemux].Output1);
                    demux[IN].ConnectControl(Control[currControl - 2]);
                    demux[IN + 1].ConnectInput(demux[currentDemux].Output2);
                    demux[IN + 1].ConnectControl(Control[currControl - 2]);

                    currentDemux++;
                }
            }

            for (int j = Outputs.Length / 2 - 1, index0 = 0; j < Outputs.Length - 1; j++, index0 += 2)
            {
                Outputs[index0].ConnectInput(demux[j].Output1);
                Outputs[index0 + 1].ConnectInput(demux[j].Output2);
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
            Control[0].Value = 0; Control[1].Value = 0;
            Input[0].Value = 0; Input[1].Value = 0; Input[2].Value = 0;
            if ( Outputs[0][0].Value != 0 || Outputs[0][1].Value != 0 || Outputs[0][2].Value != 0)
                return false;
            
            Control[0].Value = 1; Control[1].Value = 1;
            Input[0].Value = 1; Input[1].Value = 1; Input[2].Value = 0;
            if (Outputs[3][0].Value != 1 || Outputs[3][1].Value != 1 || Outputs[3][2].Value != 0)
                return false;
            
            Control[0].Value = 1; Control[1].Value = 0;
            Input[0].Value = 1; Input[1].Value = 1; Input[2].Value = 0;
            if (Outputs[1][0].Value != 1 || Outputs[1][1].Value != 1 || Outputs[1][2].Value != 0)
                return false;
            
            Control[0].Value = 0; Control[1].Value = 1;
            Input[0].Value = 1; Input[1].Value = 1; Input[2].Value = 1;
            if ( Outputs[2][0].Value != 1 || Outputs[2][1].Value != 1 || Outputs[2][2].Value != 1)
                return false;
            
            return true;
        }
    }
}