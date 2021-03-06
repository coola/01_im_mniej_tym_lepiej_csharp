﻿using System;
using System.IO;
using compressDecompress;

namespace decompress
{
    static class DecompressProgram
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0) { throw new Exception("File name cannot be empty"); }
                if (args.Length != 1) { throw new Exception("There should be only one argument - filename"); }

                var nameOfFileToDecompress = args[0];

                var dataFromFile = File.ReadAllLines(nameOfFileToDecompress);

                var decompressedString = CompressDecompress.Decompress(dataFromFile[0]);

                var fileName = nameOfFileToDecompress.Split('_');

                File.WriteAllText(string.Format("{0}_decompress", fileName[0]), decompressedString);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }

        }
    }
}
