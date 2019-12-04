﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements a register that can maintain 1 bit.
    class SingleBitRegister : Gate
    {
        public Wire Input { get; private set; }
        public Wire Output { get; private set; }
        //A bit setting the register operation to read or write
        public Wire Load { get; private set; }

        public SingleBitRegister()
        {
            
            Input = new Wire();
            Load = new Wire();
            //your code here 
            
            var gMux = new MuxGate();
            var gFlipFlop = new DFlipFlopGate();
            gMux.ConnectInput1(Input);
            gMux.ConnectControl(Load);
            gMux.ConnectInput2(gFlipFlop.Output);
            gFlipFlop.ConnectInput(gMux.Output);
            Output = gFlipFlop.Output;

        }

        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }

      

        public void ConnectLoad(Wire wLoad)
        {
            Load.ConnectInput(wLoad);
        }


        public override bool TestGate()
        {
            Input.Value = 0;
            Load.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            if (Output.Value != 0)
                return false;
            return true;
        }
    }
}
