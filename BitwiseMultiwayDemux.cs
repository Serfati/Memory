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
        
        public BitwiseMultiwayDemux(int iSize, int cControlBits)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Outputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            for (int i = 0; i < Outputs.Length; i++)
            {
                Outputs[i] = new WireSet(Size);
            }
            
            BitwiseDemux[] demuxes = new BitwiseDemux[Outputs.Length - 1];
            
            demuxes[0] = new BitwiseDemux(Size);
            demuxes[0].ConnectInput(Input);
            demuxes[0].ConnectControl(Control[cControlBits-1]);
           
            for (int i = 0 ,k = 1, f = 1, currControl = cControlBits - 1; i < demuxes.Length; i++)
            {
                demuxes[i] = new BitwiseDemux(Size);
                if (i == k)
                {
                    currControl--;
                    k = k + f * 2;
                    f = f * 2;
                    demuxes[i].ConnectControl(Control[currControl]);
                    continue;
                }
                demuxes[i].ConnectControl(Control[currControl]);
            }

            try
            {
                for (int position = demuxes.Length / 2, j = 0, i = demuxes.Length / 2; i >= 0; i--)
                {
                    Outputs[j].ConnectInput(demuxes[position].Output1);
                    Outputs[j + 1].ConnectInput(demuxes[position].Output2);
                    j = j + 2;
                    position++;
                }
                
                int  currDemux = demuxes.Length - 1;
                int index0 = demuxes.Length / 2 - 1;
                for (int i =index0; i >= 0; i--)
                {
                    demuxes[currDemux].ConnectInput(demuxes[index0].Output2);
                    demuxes[currDemux - 1].ConnectInput(demuxes[index0].Output1);
                    index0--;
                    currDemux -= 2;
                }

                demuxes[0].ConnectInput(Input);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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
            Input[0].Value = 1; Input[1].Value = 0; Input[2].Value = 1;
            if ( Outputs[0][0].Value != 1 || Outputs[0][1].Value != 0 || Outputs[0][2].Value != 1)
                return false;
            
            Control[0].Value = 1; Control[1].Value = 1;
            Input[0].Value = 0; Input[1].Value = 0; Input[2].Value = 1;
            if (Outputs[3][0].Value != 0 || Outputs[3][1].Value != 0 || Outputs[3][2].Value != 1)
                return false;
            
            Control[0].Value = 1; Control[1].Value = 0;
            Input[0].Value = 0; Input[1].Value = 0; Input[2].Value = 1;
            if (Outputs[1][0].Value != 0 || Outputs[1][1].Value != 0 || Outputs[1][2].Value != 1)
                return false;
            
            Control[0].Value = 0; Control[1].Value = 1;
            Input[0].Value = 0; Input[1].Value = 0; Input[2].Value = 0;
            if ( Outputs[2][0].Value != 0 || Outputs[2][1].Value != 0 || Outputs[2][2].Value != 0)
                return false;
            
            return true;
        }
    }
}