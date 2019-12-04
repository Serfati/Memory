using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class represents an n bit register that can maintain an n bit number
    class MultiBitRegister : Gate
    {
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //A bit setting the register operation to read or write
        public Wire Load { get; private set; }

        //Word size - number of bits in the register
        public int Size { get; private set; }


        public MultiBitRegister(int iSize)
        {
            Size = iSize;
            Input = new WireSet(Size);
            Output = new WireSet(Size);
            Load = new Wire();
            //your code here
            
            SingleBitRegister[] bitRegisters=new SingleBitRegister[iSize];
            
            for(int i = 0; i < Size; i++){
                bitRegisters[i] = new SingleBitRegister();
                bitRegisters[i].ConnectInput(Input[i]);
                bitRegisters[i].ConnectLoad(Load);
                Output[i].ConnectInput(bitRegisters[i].Output);
            }

        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }

        
        public override string ToString()
        {
            return Output.ToString();
        }


        public override bool TestGate()
        {
            Input.Set2sComplement(5);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.Get2sComplement() != 5)
                return false;
            return true;
        }
    }
}
