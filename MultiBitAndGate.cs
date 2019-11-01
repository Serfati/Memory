using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)
    class MultiBitAndGate : MultiBitGate
    {
        public MultiBitAndGate(int iInputCount)
            : base(iInputCount)
        {
            var and = new AndGate[iInputCount - 1];
            for (int i = 0; i < and.Length; i++)
            {
                and[i] = new AndGate();
                if (i != 0)
                {
                    and[i].ConnectInput1(and[i - 1].Output);
                    and[i].ConnectInput2(m_wsInput[i + 1]);
                }
                else
                {
                    and[0].ConnectInput1(m_wsInput[0]);
                    and[0].ConnectInput2(m_wsInput[1]);
                }
            }
            Output = and[and.Length - 1].Output;
        }


        public override bool TestGate()
        {
            m_wsInput.SetValue(14);
            Console.WriteLine("MultiAnd..Is it true? : " + m_wsInput+"--> "+Output);
            return true;
        }
    }
}
