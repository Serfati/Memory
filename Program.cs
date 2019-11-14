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
            
            BitwiseMultiwayDemux bwMwDemux = new BitwiseMultiwayDemux(3,3);
            if (bwMwDemux.TestGate())
                Console.WriteLine("done bwMwDemux");
            
            Console.WriteLine("FINISH HIM");
        }
    }
}
