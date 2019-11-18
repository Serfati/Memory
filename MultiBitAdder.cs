using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    //This class implements an adder, receving as input two n bit numbers, and outputing the sum of the two numbers
    class MultiBitAdder : Gate
    {
        //Word size - number of bits in each input
        public int Size { get; }

        public WireSet Input1 { get; }
        public WireSet Input2 { get; }
        public WireSet Output { get; }
        //An overflow bit for the summation computation
        public Wire Overflow { get;}

        public MultiBitAdder(int iSize)
        {
            Size = iSize;
            Overflow = new Wire();
            Input1 = new WireSet(iSize);
            Input2 = new WireSet(iSize);
            Output = new WireSet(iSize);
            var fullAdders = new FullAdder[iSize];

            try
            {
                for (int i = 0; i < iSize; i++)
                {
                    if (i == 0)
                    {
                        fullAdders[i] = new FullAdder();
                        fullAdders[i].CarryInput.Value = 0;
                        fullAdders[i].ConnectInput1(Input1[i]);
                        fullAdders[i].ConnectInput2(Input2[i]);
                        Output[i].ConnectInput(fullAdders[i].Output);
                        continue;
                    }

                    fullAdders[i] = new FullAdder();
                    fullAdders[i].ConnectInput1(Input1[i]);
                    fullAdders[i].ConnectInput2(Input2[i]);
                    fullAdders[i].CarryInput.ConnectInput(fullAdders[i - 1].CarryOutput);
                    Output[i].ConnectInput(fullAdders[i].Output);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                Overflow = fullAdders[iSize - 1].CarryOutput;
            }

        }

        public override string ToString()
        {
            return Input1 + "(" + Input1.Get2sComplement() + ")" + " + " + Input2 + "(" + Input2.Get2sComplement() + ")" + " = " + Output + "(" + Output.Get2sComplement() + ")";
        }

        public void ConnectInput1(WireSet wInput)
        {
            Input1.ConnectInput(wInput);
        }
        public void ConnectInput2(WireSet wInput)
        {
            Input2.ConnectInput(wInput);
        }


        public override bool TestGate()
        {
            Input1[0].Value = 1; Input1[1].Value = 0; Input1[2].Value = 1;
            Input2[0].Value = 1; Input2[1].Value = 1; Input2[2].Value = 0;
            if ((Output[0].Value != 0) || (Output[1].Value != 0) || (Output[2].Value != 0) || (Overflow.Value != 1))
                return false;
            Input1[0].Value = 1; Input1[1].Value = 0; Input1[2].Value = 1;
            Input2[0].Value = 0; Input2[1].Value = 0; Input2[2].Value = 1;
            if ((Output[0].Value != 1) || (Output[1].Value != 0) || (Output[2].Value != 0) || (Overflow.Value != 1))
                return false;
            Input1[0].Value = 0; Input1[1].Value = 1; Input1[2].Value = 1;
            Input2[0].Value = 0; Input2[1].Value = 1; Input2[2].Value = 0;
            if ((Output[0].Value != 0) || (Output[1].Value != 0) || (Output[2].Value != 0) || (Overflow.Value != 1))
                return false;
            Input1[0].Value = 1; Input1[1].Value = 1; Input1[2].Value = 1;
            Input2[0].Value = 1; Input2[1].Value = 1; Input2[2].Value = 1;
            if ((Output[0].Value != 0) || (Output[1].Value != 1) || (Output[2].Value != 1) || (Overflow.Value != 1))
                return false;
            return true;
        }
    }
}