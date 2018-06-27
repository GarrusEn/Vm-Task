using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZipApp
{
    class ZipWorker
    {
        delegate void Operation(Controller controller);

        public ZipWorker(string[] args, Error err)
        {
            // Start the controller
            Controller controller = new Controller(args[1], args[2]);
            // Define the operation
            Operation op;
            if (args[0] == Enum.GetName(typeof(Operations), 0))
            {
                op = Compress;
            }
            else
            {
                op = Decompress;
            }
            // Starting work
            QueueWork(op);
        }
        
        void QueueWork(Operation op)
        {
            // Starting threads
            Thread[] treadsPool = new Thread[10];
            for (int i = 0; i < treadsPool.Count(); i++)
            {
                treadsPool[i].Start(op);
            }
        }


        void Compress(Controller controller)
        {
            Console.WriteLine("alalalala");
            Console.WriteLine(controller.SF);
        }

        void Decompress(Controller controller)
        {
            Console.WriteLine("dodododo");
            Console.WriteLine(controller.SF);
        }

    }

    class Controller
    {
        string SourceFile, TargetFile;
        long sourceFilePosition, sourceFileLength;

        public Controller(string source, string target)
        {
            SourceFile = source;
            TargetFile = target;
            sourceFilePosition = 0;
        }

        public string SF
        {
            get { return SourceFile; }
        }


    }
}
