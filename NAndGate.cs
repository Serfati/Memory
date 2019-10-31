using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //this gate is given to you fully implemented - you only have to use it in your code
    class NAndGate : TwoInputGate, Component
    {
        public static int NAND_GATES = 0;
        public static int NAND_COMPUTE = 0;

        public static bool Corrupt = false;

        public NAndGate()
        {
            NAND_GATES++;
            Input1.ConnectOutput(this);
            Input2.ConnectOutput(this);
        }

        public override string ToString()
        {
            return "NAnd " + Input1.Value + "," + Input2.Value + " -> " + Output.Value;
        }


        public override bool TestGate()
        {
            Input1.Value = 0;
            Input2.Value = 0;
            
            if (Output.Value != 1)
                return false;
            Input1.Value = 0;
            Input2.Value = 1;
            
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 0;
            
            if (Output.Value != 1)
                return false;
            Input1.Value = 1;
            Input2.Value = 1;
            
            if (Output.Value != 0)
                return false;
            return true;
        }

        #region Component Members

        public void Compute()
        {
            NAND_COMPUTE++;
            if (Corrupt) //this is for testing the implementation of all gates built using nands
                Output.Value = 0;
            else
            {
                if (Input1.Value == 1 && Input2.Value == 1)
                    Output.Value = 0;
                else
                    Output.Value = 1;
            }
        }

        #endregion
    }
}
