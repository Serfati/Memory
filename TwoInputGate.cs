using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //this is the base class for all boolean gates with two input wires
    abstract class TwoInputGate : Gate
    {
        public Wire Input1 { get; protected set; }
        public Wire Input2 { get; protected set; }
        public Wire Output { get; protected set; }

        public TwoInputGate()
        {
            Input1 = new Wire();
            Input2 = new Wire();
            Output = new Wire();
        }

        public void ConnectInput1(Wire wInput)
        {
            Input1.ConnectInput(wInput);
        }
        public void ConnectInput2(Wire wInput)
        {
            Input2.ConnectInput(wInput);
        }

    }
}
