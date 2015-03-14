
using System;
using System.IO;
using compressDecompress;

namespace compress
{
    public static class CompressProgram
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0) { throw new Exception("File name cannot be empty"); }
                if (args.Length != 1) { throw new Exception("There should be only one argument - filename"); }

                var nameOfFileToCompress = args[0];

                var dataFromFile = File.ReadAllLines(nameOfFileToCompress);

                var compressedString = CompressDecompress.Compress(dataFromFile);

                var fileName = nameOfFileToCompress.Split('.');

                File.WriteAllText(string.Format("{0}_compress", fileName[0]), compressedString);

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }

        }
    }
}
