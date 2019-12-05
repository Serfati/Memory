using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a memory unit, containing k registers, each of size n bits.
    class Memory : SequentialGate
    {
        //The address size determines the number of registers
        public int AddressSize { get; private set; }
        //The word size determines the number of bits in each register
        public int WordSize { get; private set; }

        //Data input and output - a number with n bits
        public WireSet Input { get; private set; }
        public WireSet Output { get; private set; }
        //The address of the active register
        public WireSet Address { get; private set; }
        //A bit setting the memory operation to read or write
        public Wire Load { get; private set; }

        //your code here

        public Memory(int iAddressSize, int iWordSize)
        {
            AddressSize = iAddressSize;
            WordSize = iWordSize;

            Input = new WireSet(WordSize);
            Output = new WireSet(WordSize);
            Address = new WireSet(AddressSize);
            Load = new Wire();
            
            int numOfbits = (int) Math.Pow(2, AddressSize);
            var mBitwiseMultiwayDemux = new BitwiseMultiwayDemux(1, AddressSize);
            var mBitwiseMultiwayMux = new BitwiseMultiwayMux(WordSize, AddressSize);
            var mRegisters = new MultiBitRegister[numOfbits];
            mRegisters[0] = new MultiBitRegister(iWordSize);
            for (int i = 0; i < numOfbits; i++)
            {
                if (i == 0)
                {
                     mBitwiseMultiwayDemux.Input[0].ConnectInput(Load);
                     mBitwiseMultiwayDemux.ConnectControl(Address);
                     
                     mRegisters[0].ConnectInput(Input);
                     mRegisters[0].Load.ConnectInput(mBitwiseMultiwayDemux.Outputs[0][0]);
                     mBitwiseMultiwayMux.ConnectInput(0, mRegisters[0].Output);
                     continue;
                }
                mRegisters[i] = new MultiBitRegister(iWordSize);
                mRegisters[i].ConnectInput(Input);
                mRegisters[i].Load.ConnectInput(mBitwiseMultiwayDemux.Outputs[i][0]);
                mBitwiseMultiwayMux.ConnectInput(i, mRegisters[i].Output);
            }
            mBitwiseMultiwayMux.ConnectControl(Address);
            Output = mBitwiseMultiwayMux.Output;
        }

        public void ConnectInput(WireSet wsInput)
        {
            Input.ConnectInput(wsInput);
        }
        public void ConnectAddress(WireSet wsAddress)
        {
            Address.ConnectInput(wsAddress);
        }


        public override void OnClockUp()
        {
        }

        public override void OnClockDown()
        {
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public override bool TestGate()
        {
            Input.Set2sComplement(22);
            Address.Set2sComplement(4);
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Load.Value=0;
            if (Output.Get2sComplement() != 22)
                return false;
            return true;
        }
    }
}
