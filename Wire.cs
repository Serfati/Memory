using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class Wire : Component
    {
        //This class represents a wire that can hold a single value (bit) - 0 or 1
        //You should not modify this class
        public bool InputConected { get; private set; }
        private List<Component> Outputs;
        private int m_iValue;

        //Accessor function for reading or writing the value
        public int Value
        {
            get
            {
                return m_iValue;
            }
            set
            {
                if (value != 0 && value != 1)
                    throw new ArgumentOutOfRangeException();
                m_iValue = value;
                foreach (Component c in Outputs)
                {
                    if (c is Wire)

                        ((Wire)c).Value = value;

                    else
                        c.Compute();
                }
            }
        }

        public Wire()
        {
            Outputs = new List<Component>();
            Value = 0;
        }

        //connecting two wires together - you will use this method a lot
        public void ConnectInput(Wire wIn)
        {
            if (InputConected)
                throw new InvalidOperationException("Cannot connect a wire to more than one inputs");
            InputConected = true;
            wIn.Outputs.Add(this);
            Value = wIn.Value;
        }
        public override string ToString()
        {
            return m_iValue + "";
        }

        //connecting a wire to a component (Nand or Not only) - you are not supposed to use this method
        public void ConnectOutput(Component cOut)
        {
            Outputs.Add(cOut);
            cOut.Compute();
        }
         


        #region Component Members

        public void Compute()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
