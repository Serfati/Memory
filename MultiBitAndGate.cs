using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)
    class MultiBitAndGate : MultiBitGate
    {
        private AndGate[] and;

        public MultiBitAndGate(int iInputCount)
            : base(iInputCount)
        {
            //your code here
            and = new AndGate[iInputCount - 1];
            and[0] = new AndGate();
            and[0].ConnectInput1(m_wsInput[0]);
            and[0].ConnectInput2(m_wsInput[1]);
            for (int i = 2; i<iInputCount; i++)
            {
                and[i-1] = new AndGate();
                and[i-1].ConnectInput1(and[i-2].Output);
                and[i - 1].ConnectInput2(m_wsInput[i]);
            }
            Output.ConnectInput(and[iInputCount-2].Output);
            
        }


        public override bool TestGate()
        {
            m_wsInput.SetValue(7);
            Console.WriteLine("TESTING MultiAnd..Is it true?    : " + this.m_wsInput.ToString()+"  --> "+Output);
            return true;
        }
    }
}
