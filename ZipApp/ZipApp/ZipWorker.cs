using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ZipApp
{
    class ZipWorker
    {
        public ReadWriteController controller;
        public Error error;
        object block = new object();

        public ZipWorker(string[] args, Error err)
        {
            // Start the controller
            controller = new ReadWriteController(args[1], args[2]);
            error = err;

            // Starting work
            QueueWork(args[0]);

        }

        void QueueWork(string operation)
        {
            // Starting threads
            Thread[] Threads = new Thread[Environment.ProcessorCount];


            if (operation == Enum.GetName(typeof(Operations), 0))
            {
                for (int i = 0; i < Threads.Count(); i++)
                {
                    Threads[i] = new Thread(Compress);
                    Threads[i].Start();
                }
            }
            else
            {
                for (int i = 0; i < Threads.Count(); i++)
                {
                    Threads[i] = new Thread(Decompress);
                    Threads[i].Start();
                }
            }
        }

        void Compress()
        {
            lock (block)
            {
                // Get OriginalDataBlock
            }

            // Compress DataBlock

            lock (block)
            {
                // Send CompressDataBlock
            }
        }

        void Decompress()
        {
            lock (block)
            {
                // Get CompressDataBlock
            }

            // Decompress DataBlock

            lock (block)
            {
                // Send OriginalDataBlock
            }
        }
    }

    class ReadWriteController
    {
        string SourceFile, TargetFile;
        long sourceFilePosition, sourceFileLength;

        public ReadWriteController(string source, string target)
        {
            SourceFile = source;
            TargetFile = target;
            sourceFilePosition = 0;
        }

        
    }
}
