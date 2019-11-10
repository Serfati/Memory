using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A demux has 2 outputs. There is a single input and a control bit, selecting whether the input should be directed to the first or second output.
    class Demux : Gate
    {
        public Wire Output1 { get; private set; }
        public Wire Output2 { get; private set; }
        public Wire Input { get; private set; }
        public Wire Control { get; private set; }

        //your code here

        public Demux()
        {
            Input = new Wire();
            
            var mGAnd1 = new AndGate();
            var mGAnd2 = new AndGate();
            var mGNot1 = new NotGate();
            Control = new Wire();
            
            mGNot1.ConnectInput(Control);
            
            mGAnd2.ConnectInput1(Input);
            mGAnd2.ConnectInput2(Control);
            
            mGAnd1.ConnectInput1(Input);
            mGAnd1.ConnectInput2(mGNot1.Output);
            
            Output1 = mGAnd1.Output;
            Output2 = mGAnd2.Output;
        }

        public void ConnectControl(Wire wControl)
        {
            Control.ConnectInput(wControl);
        }
        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }
        
        public override bool TestGate()
        {
            Input.Value = 1;
            Control.Value = 1;
            if ((Output1.Value != 0) || (Output2.Value != 1)) 
                return false;
            Input.Value = 0;
            Control.Value = 0;
            if ((Output1.Value != 0) ||(Output2.Value!=0))
                return false;
            Input.Value = 1;
            Control.Value = 0;
            if ((Output1.Value != 1) || (Output2.Value != 0)) 
                return false;
            Input.Value = 0;
            Control.Value = 1;
            if ((Output1.Value != 0) || (Output2.Value != 0)) 
                return false;
            return true;
        }
    }
}