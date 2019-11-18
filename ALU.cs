using System;

namespace Components
{
    //This class is used to implement the ALU
    //The specification can be found at https://b1391bd6-da3d-477d-8c01-38cdf774495a.filesusr.com/ugd/56440f_2e6113c60ec34ed0bc2035c9d1313066.pdf slides 48,49
    class ALU : Gate
    {
        //The word size = number of bit in the input and output
        public int Size { get; }

        //Input and output n bit numbers
        public WireSet InputX { get; }
        public WireSet InputY { get; }
        public WireSet Output { get; }

        //Control bit 
        public Wire ZeroX { get; }
        public Wire ZeroY { get; }
        public Wire NotX { get; }
        public Wire NotY { get; }
        public Wire F { get; }
        public Wire NotOutput { get; }

        //Bit outputs
        public Wire Zero { get; }
        public Wire Negative { get; }

        public ALU(int iSize)
        {
            Size = iSize;
            InputX = new WireSet(Size);
            InputY = new WireSet(Size);
            ZeroX = new Wire();
            ZeroY = new Wire();
            NotX = new Wire();
            NotY = new Wire();
            F = new Wire();
            NotOutput = new Wire();
            Negative = new Wire();            
            Zero = new Wire();
            Output = new WireSet(Size);

            
            var fx = new BitwiseMux(Size);
            var fAdder = new MultiBitAdder(Size);
            var fAnd = new BitwiseAndGate(Size);
            
            var notX = new BitwiseNotGate(Size);
            var notY = new BitwiseNotGate(Size);
            
            var notCheckzero = new BitwiseNotGate(Size);
            var zeroCheck = new MultiBitAndGate(Size);
            var nOut = new BitwiseMux(Size);
            var notOut = new BitwiseNotGate(Size);

            var nx = new BitwiseMux(Size);
            var ny = new BitwiseMux(Size);
            var zx = new BitwiseMux(Size);
            var zy = new BitwiseMux(Size);

            var wireX0 = new WireSet(iSize);
            var wireY0 = new WireSet(iSize);
             wireX0.Set2sComplement(0);
             wireY0.Set2sComplement(0);
             
            try
            {
                //zx//
                var zeroXWire = new WireSet(Size);
                zeroXWire.Set2sComplement(0);
                zx.ConnectInput1(InputX);
                zx.ConnectInput2(zeroXWire);
                zx.ConnectControl(ZeroX);
            }
            catch (Exception e)
            {
                Console.WriteLine("zx exp" + e.Message);
            }

            try
            {
                //nx//
                nx.ConnectInput1(zx.Output);
                notX.ConnectInput(zx.Output);
                nx.ConnectInput2(notX.Output);
                nx.ConnectControl(NotX);
            }
            catch (Exception e)
            {
                Console.WriteLine("nx exp" + e.Message);
            }

            try
            {
                //zy//
                var zeroYWire = new WireSet(Size);
                zeroYWire.Set2sComplement(0);
                zy.ConnectInput1(InputY);
                zy.ConnectInput2(zeroYWire);
                zy.ConnectControl(ZeroY);
            }
            catch (Exception e)
            {
                Console.WriteLine("zy exp" + e.Message);
            }

            try
            {
                //ny//
                ny.ConnectInput1(zy.Output);
                notY.ConnectInput(zy.Output);
                ny.ConnectInput2(notY.Output);
                ny.ConnectControl(NotY);
            }
            catch (Exception e)
            {
                Console.WriteLine("ny exp" + e.Message);
            }

            try
            {
                //f//
                fAdder.ConnectInput1(nx.Output);
                fAdder.ConnectInput2(ny.Output);
                
                fAnd.ConnectInput1(nx.Output);
                fAnd.ConnectInput2(ny.Output);

                fx.ConnectInput1(fAnd.Output);
                fx.ConnectInput2(fAdder.Output);
                
                fx.ConnectControl(F);
            }
            catch (Exception e)
            {
                Console.WriteLine("f exp" + e.Message);
                throw;
            }

            try
            {
                notOut.ConnectInput(fx.Output);
                nOut.ConnectInput1(fx.Output);
                nOut.ConnectInput2(notOut.Output);
                nOut.ConnectControl(NotOutput);
            
                /*********************************/
                Output = nOut.Output;
                /*********************************/
            }
            catch (Exception e)
            {
                Console.WriteLine("not Output exp" + e.Message);
            }

            try
            {
                //negative number check
                Negative = Output[Size - 1];

                //Zero number check
                notCheckzero.ConnectInput(nOut.Output);
                zeroCheck.ConnectInput(notCheckzero.Output);
                Zero.ConnectInput(zeroCheck.Output);
            }
            catch (Exception e)
            {
                Console.WriteLine("Output info exp" + e.Message);
                throw;
            }
        }

        public override bool TestGate()
        {
            InputX.Set2sComplement(5);
            InputY.Set2sComplement(5);
            ZeroX.Value = 1;
            NotX.Value = 1;
            ZeroY.Value = 0;
            NotY.Value = 0;
            F.Value = 0;
            NotOutput.Value = 1;
            Console.WriteLine("Case 1: " + Output.Get2sComplement());
            Console.WriteLine("Negative?: " + Negative.Value);
            Console.WriteLine("Zero?: " + Zero.Value);
            Console.WriteLine("");
            
            InputX.Set2sComplement(5);
            InputY.Set2sComplement(5);
            ZeroX.Value = 0;
            NotX.Value = 0;
            ZeroY.Value = 1;
            NotY.Value = 1;
            F.Value = 0;
            NotOutput.Value = 0;
            Console.WriteLine("Case 2: " + Output.Get2sComplement());
            Console.WriteLine("Negative?: " + Negative.Value);
            Console.WriteLine("Zero?: " + Zero.Value);
            Console.WriteLine("");
            
            InputX.SetValue(6);
            InputY.SetValue(5);
            
            ZeroX.Value = 0;
            NotX.Value = 0;
            ZeroY.Value = 1;
            NotY.Value = 1;
            F.Value = 0;
            NotOutput.Value = 1;
            if (Output.Get2sComplement() != -7 || Zero.Value != 0 || Negative.Value != 1)
                return false;
            
            ZeroX.Value = 1; 
            ZeroY.Value = 1;
            NotX.Value = 0;
            NotY.Value = 0;
            F.Value = 1;
            NotOutput.Value = 0;
            if (Output.Get2sComplement() != 0 || Zero.Value != 1 || Negative.Value != 0)
                return false;
            
            return true;
        }
    }
}