using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)

    class MultiBitOrGate : MultiBitGate
    {
        private OrGate[] OR;

        public MultiBitOrGate(int iInputCount)
            : base(iInputCount)
        {
            OR = new OrGate[iInputCount - 1];
            for (int i = 1; i < iInputCount; i++)
            {
                if (i == 1)
                {
                    OR[0] = new OrGate();
                    OR[0].ConnectInput1(m_wsInput[0]);
                    OR[0].ConnectInput2(m_wsInput[1]);
                }
                else
                {
                    OR[i - 1] = new OrGate();
                    OR[i - 1].ConnectInput1(OR[i - 2].Output);
                    OR[i - 1].ConnectInput2(m_wsInput[i]);
                }
            }
            Output.ConnectInput(OR[iInputCount-2].Output);

        }

        public override bool TestGate()
        {
            m_wsInput.SetValue(10);
            Console.WriteLine("MultiOr..Is it true?    : " + this.m_wsInput.ToString()+"  --> "+Output);
            return true;
        }
    }
}
