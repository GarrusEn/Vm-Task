using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ZipApp
{
    class ReadWriteManager
    {
        int MaxSize = 1024000;


        FileStream _sourceFS;
        long _sourceFSPosition = 0;

        FileStream _targetFS;
        long _targetFSPosition = 0;

        public ReadWriteManager(string SourceFile, string TargetFile)
        {
            _sourceFS = new FileStream(SourceFile, FileMode.Open);
            _targetFS = new FileStream(TargetFile, FileMode.Open);
        }

        public CompressDataBlock GetDataBlockForDecompress()
        {
            CompressDataBlock data;
            BinaryFormatter formatter = new BinaryFormatter();
            data = (CompressDataBlock)formatter.Deserialize(_sourceFS);
            return data;
        }

        public OriginalDataBlock GetDataBlockForCompress()
        {
            long position = _sourceFSPosition;

            // refactoring..

            //long sizeBlock = fs.Length - fs.Position;
            //if (sizeBlock < MaxSize)
            //{
            //    buffer = new byte[sizeBlock];
            //    fs.Read(buffer, 0, (int)sizeBlock);
            //    data.setSize((int)sizeBlock);
            //    data.CompressData = buffer;
            //    float proc = ((float)fs.Position / (float)fs.Length) * 100;
            //    Console.Write("\rУпаковка: {0:0.0}% ", proc);
            //    fs.Close();
            //}
            //else
            //{
            //    buffer = new byte[MaxSize];
            //    fs.Read(buffer, 0, MaxSize);
            //    data.setSize(MaxSize);
            //    data.CompressData = buffer;
            //    inFilePosition = fs.Position;
            //    float proc = ((float)fs.Position / (float)fs.Length) * 100;
            //    Console.Write("\rУпаковка: {0:0.0}% ", proc);
            //    if (fs.Position != fs.Length)
            //    {
            //        fs.Close();
            //        // Разблокируем доступ к файлу для других потоков
            //        IsNotTheEnd = true;
            //    }
            //    else if (stop || fs.Position == fs.Length)
            //    {
            //        WorkISEnded = true;
            //        fs.Close();
            //    }

            //}


        }

        public void WriteBlockTargetFile(byte[] data)
        {
            _targetFS.Write(data, 0, data.Length);
        }
    }

    class OriginalDataBlock
    {
        byte[] _data;
        long _position;

        public OriginalDataBlock(byte[] Data, long Position)
        {
            _data = Data;
            _position = Position;
        }

        public byte[] GetData()
        {
            return _data;
        }

        public long GetPosition()
        {
            return _position;
        }
    }
}
