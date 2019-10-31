using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This is the only gate that actually gets the clock ticks
    //It is the base component for all memory components
    //You shouldn't make changes to this class
    class DFlipFlopGate : SequentialGate
    {
        public Wire Input { get; private set; }
        public Wire Output { get; private set; }

        private int m_iState;

        public DFlipFlopGate()
        {
            Input = new Wire();
            Output = new Wire();
        }

        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }

        public override void OnClockUp()
        {
            Output.Value = m_iState;
        }

        public override void OnClockDown()
        {
            m_iState = Input.Value;
        }

        public override bool TestGate()
        {
            Input.Value = 1;
            Clock.ClockDown();
            Clock.ClockUp();
            Input.Value = 0;
            if (Output.Value != 1)
                return false;
            Clock.ClockDown();
            Clock.ClockUp();
            Input.Value = 1;
            if (Output.Value != 0)
                return false;
            return true;
        }
    }
}
