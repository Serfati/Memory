using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //A mux has 2 inputs. There is a single output and a control bit, selecting which of the 2 inpust should be directed to the output.
    class MuxGate : TwoInputGate
    {
        public Wire ControlInput { get; private set; }

        public MuxGate()
        {
            var mGAnd1 = new AndGate();
            var mGAnd2 = new AndGate();
            var mGNot1 = new NotGate();
            var mGOr = new OrGate();
            ControlInput = new Wire();
            mGNot1.ConnectInput(ControlInput);
            
            mGAnd2.ConnectInput1(Input2);
            mGAnd2.ConnectInput2(ControlInput);
            
            mGAnd1.ConnectInput1(Input1);
            mGAnd1.ConnectInput2(mGNot1.Output);
                        
            mGOr.ConnectInput1(mGAnd1.Output);
            mGOr.ConnectInput2(mGAnd2.Output);
            
            Output = mGOr.Output;
        }

        public void ConnectControl(Wire wControl)
        {
            ControlInput.ConnectInput(wControl);
        }
    
        public override string ToString()
        {
            return "Mux " + Input1.Value + "," + Input2.Value + ",C" + ControlInput.Value + " -> " + Output.Value;
        }
    
        public override bool TestGate()
        {
            ControlInput.Value = 1;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            Input1.Value = 0;
            Input2.Value = 0;
            if (Output.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 0)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            ControlInput.Value = 0;
            Input1.Value = 1;
            Input2.Value = 1;
            if (Output.Value != 1)
                return false;
            Input1.Value = 0;
            Input2.Value = 0;
            if (Output.Value != 0)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            if (Output.Value != 1)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            if (Output.Value != 0)
                return false;
            return true;
        }
    }
}