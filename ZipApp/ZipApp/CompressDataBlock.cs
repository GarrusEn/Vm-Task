using System;
using System.IO;
using System.IO.Compression;

namespace ZipApp
{
    [Serializable]
    class CompressDataBlock
    {
        long _position;
        int _originalSize;
        int _compressSize;
        byte[] _data;

        public long Position()
        {
            return _position;
        }

        public int GetCompressSize()
        {
            return _compressSize;
        }

        public CompressDataBlock()
        {

        }

        public CompressDataBlock(long Position)
        {
            _position = Position;
        }      

        public void SetDataForCompress(byte[] data)
        {
            _originalSize = data.Length;
            Compress(data);
        }

        void Compress(byte[] data)
        {
            using (MemoryStream output = new MemoryStream())
            {
                using (GZipStream cs = new GZipStream(output, CompressionMode.Compress))
                {
                    cs.Write(data, 0, data.Length);
                }

                byte[] secondBuff;
                secondBuff = output.ToArray();
                _compressSize = secondBuff.Length;
                _data = new byte[_compressSize];
                Array.Copy(secondBuff, _data, _compressSize);
            }
        }

        public byte[] GetDataFromCompress()
        {
            return Decompress();
        }

        byte[] Decompress()
        {
            byte[] targetBuff = new byte[_originalSize];

            using (MemoryStream output = new MemoryStream(_data))
            {
                using (GZipStream ds = new GZipStream(output, CompressionMode.Decompress))
                {
                    ds.Read(targetBuff, 0, targetBuff.Length);
                }
            }
            return targetBuff;
        }
    }
}
