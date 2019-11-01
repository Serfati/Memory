using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //Multibit gates take as input k bits, and compute a function over all bits - z=f(x_0,x_1,...,x_k)

    class MultiBitOrGate : MultiBitGate
    {
        public MultiBitOrGate(int iInputCount)
            : base(iInputCount)
        {
            var orArray = new OrGate[iInputCount - 1];
            for (int i = 0; i < orArray.Length; i++)
            {
                orArray[i] = new OrGate();
                if (i != 0)
                {
                    orArray[i].ConnectInput1(orArray[i - 1].Output);
                    orArray[i].ConnectInput2(m_wsInput[i + 1]);
                }
                else
                {
                    orArray[0].ConnectInput1(m_wsInput[0]);
                    orArray[0].ConnectInput2(m_wsInput[1]);
                }
            }
            Output = orArray[orArray.Length - 1].Output;
        }

        public override bool TestGate()
        {
            m_wsInput.SetValue(1);
            Console.WriteLine("MultiOr..Is it true? : " + m_wsInput+"--> "+Output);
            return true;
        }
    }
}
