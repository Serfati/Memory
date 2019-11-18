using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Components
{
    class Program
    {
        static void Main(string[] args)
        {
            //This is an example of a testing code that you should run for all the gates that you create
//
//            //Create a gate
//            AndGate and = new AndGate();
//            //Test that the unit testing works properly
//            if (!and.TestGate())
//                Console.WriteLine("bugbug");
//
//            //test or gate
//            OrGate or = new OrGate();
//            if (or.TestGate())
//                Console.WriteLine("done or");
//
            //test xor gate
//            XorGate xor = new XorGate();
//            if (xor.TestGate())
//                Console.WriteLine("done xor");
//            
//            MultiBitAndGate mbaGate = new MultiBitAndGate(4);
//            if (mbaGate.TestGate())
//                Console.WriteLine("done mba");
//            
//            MultiBitOrGate mboGate = new MultiBitOrGate(4);
//            if (mboGate.TestGate())
//                Console.WriteLine("done mbo");
//            
//            MuxGate mux = new MuxGate();
//            if (mux.TestGate())
//                Console.WriteLine("done mux");
//            
//            Demux demux = new Demux();
//            if (demux.TestGate())
//                Console.WriteLine("done demux");
//            
//            BitwiseAndGate bwAg = new BitwiseAndGate(4);
//            if (bwAg.TestGate())
//                Console.WriteLine("done bwAg");
//            
//            BitwiseNotGate bwNg = new BitwiseNotGate(4);
//            if (bwNg.TestGate())
//                Console.WriteLine("done bwNg");
////            
//            BitwiseOrGate bwOg = new BitwiseOrGate(4);
//            if (bwOg.TestGate())
//                Console.WriteLine("done bwOg");
//            
//            
//            WireSet ws = new WireSet(4);
//            ws.SetValue(8);
//            Console.WriteLine(ws.ToString());
////            
//            BitwiseMux bwMux = new BitwiseMux(4);
//            if (bwMux.TestGate())
//                Console.WriteLine("done bwMux");
//            
//            BitwiseDemux bwDemux = new BitwiseDemux(4);
//            if (bwDemux.TestGate())
//                Console.WriteLine("done bwDemux");
//
//            BitwiseMultiwayMux bwMwMux = new BitwiseMultiwayMux(3,3);
//            if (bwMwMux.TestGate())
//                Console.WriteLine("done bwMwMux");
            

            OrGate or = new OrGate();
            XorGate xor = new XorGate();
            AndGate and = new AndGate();
            MuxGate mux = new MuxGate();
            Demux demux = new Demux();
            HalfAdder halfAdder = new HalfAdder();
            FullAdder fullAdder = new FullAdder();
            WireSet wireSet = new WireSet(9);
            BitwiseAndGate bwag = new BitwiseAndGate(2);
            BitwiseNotGate bwng = new BitwiseNotGate(3);
            BitwiseOrGate bwog = new BitwiseOrGate(2);
            BitwiseMux bwm = new BitwiseMux(2);
            BitwiseDemux bwd = new BitwiseDemux(2);
            MultiBitAndGate mbag = new MultiBitAndGate(4);
            MultiBitAdder mba = new MultiBitAdder(3);
            BitwiseMultiwayMux bwmwm = new BitwiseMultiwayMux(5, 4);
            BitwiseMultiwayDemux bwmwd = new BitwiseMultiwayDemux(4, 4);
            SingleBitRegister sbr = new SingleBitRegister();
            MultiBitRegister mbr = new MultiBitRegister(4);
            wireSet.SetValue(137);
            wireSet.Set2sComplement(-32);
            if (halfAdder.TestGate())
                Console.WriteLine("HAbugbug");
            if (fullAdder.TestGate())
                Console.WriteLine("FAbugbug");
            if (mba.TestGate())
                Console.WriteLine("MultiBitAdderbugbug");
            ALU alu = new ALU(16);
            if (alu.TestGate())
                Console.WriteLine("ALU bugbug");

            Console.WriteLine("FINISH HIM");
        }
    }
}
