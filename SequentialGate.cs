using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Base class for all sequential components - you should not modify this class
    abstract class SequentialGate
    {
        public SequentialGate()
        {
            Clock.RegisterSequentialGate(this);
        }
        public abstract void OnClockUp();
        public abstract void OnClockDown();

        public abstract bool TestGate();
    }
}
