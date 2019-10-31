using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //this gate is given to you fully implemented - you only have to use it in your code
    class NotGate : Gate, Component
    {
        public Wire Input{ get; private set; }
        public Wire Output{ get; private set; }

        public static int NOT_GATES = 0;
        public static int NOT_COMPUTE = 0;

        public NotGate()
        {
            NOT_GATES++;
            Input = new Wire();
            Output = new Wire();
            Input.ConnectOutput(this);

            Input.Value = 0;
        }

        public void ConnectInput(Wire wInput)
        {
            Input.ConnectInput(wInput);
        }

        public override string ToString()
        {
            return "Not " + Input.Value +  " -> " + Output.Value;
        }


        public override bool TestGate()
        {
            Input.Value = 0;
            if (Output.Value != 1)
                return false;
            Input.Value = 1;
            if (Output.Value != 0)
                return false;
            return true;
        }

        #region Component Members

        public void Compute()
        {
            NOT_COMPUTE++;
            if (Input.Value == 0)
                Output.Value = 1;
            else
                Output.Value = 0;
        }

        #endregion
    }
}
