using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)

    abstract class MultiBitGate : Gate
    {
        protected WireSet m_wsInput;
        public Wire Output { get; protected set; }

        public MultiBitGate(int iInputCount)
        {
            m_wsInput = new WireSet(iInputCount);
            Output = new Wire();
        }

        public void ConnectInput(WireSet ws)
        {
            m_wsInput.ConnectInput(ws);
        }
    }
}
