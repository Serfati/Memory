using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a mux with k input, each input with n wires. The output also has n wires.

    class BitwiseMultiwayMux : Gate
    {
        //Word size - number of bits in each output
        public int Size { get; private set; }

        //The number of control bits needed for k outputs
        public int ControlBits { get; private set; }

        public WireSet Output { get; private set; }
        public WireSet Control { get; private set; }
        public WireSet[] Inputs { get; private set; }

        //your code here

        public BitwiseMultiwayMux(int iSize, int cControlBits)
        {
            Size = iSize;
            Output = new WireSet(Size);
            Control = new WireSet(cControlBits);
            Inputs = new WireSet[(int)Math.Pow(2, cControlBits)];
            
            WireSet[] wires = new WireSet[Inputs.Length];
            BitwiseMux[] mux = new BitwiseMux[Inputs.Length-1];
            int muxIn = 0;
            for (int i = wires.Length - 1; i >= 0; i--)
            {
                Inputs[i] = new WireSet(Size);
                wires[i] = new WireSet(Size);
                wires[i] = Inputs[i];
            }
            
            int currentControl = 0;
            for (int wiresLength = wires.Length; wiresLength >= 2 ; wiresLength /= 2)
            {
                int outIN = 0;
                for (int i = 0; i < wiresLength; i += 2)
                {
                    mux[muxIn] = new BitwiseMux(Size);
                    mux[muxIn].ConnectInput1(wires[i]);
                    mux[muxIn].ConnectInput2(wires[i + 1]);
                    mux[muxIn].ConnectControl(Control[currentControl]);
                    wires[outIN] = mux[muxIn].Output;
                    outIN++; muxIn++;
                }
                currentControl++;
            }
            Output.ConnectInput(mux[muxIn - 1].Output);
        }
        public void ConnectInput(int i, WireSet wsInput)
        {
            Inputs[i].ConnectInput(wsInput);
        }
        public void ConnectControl(WireSet wsControl)
        {
            Control.ConnectInput(wsControl);
        }
        
        public override bool TestGate()
        {
            Console.WriteLine("TESTING MULTIMUX..Is it true?    : " + this.Output.ToString());
            return true;
        }
    }
}