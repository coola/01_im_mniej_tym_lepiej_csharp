using common;

namespace compressDecompress
{
    public static class CompressDecompress
    {

        public static string Compress(string[] stringsToCompress)
        {
            var points = FileManager.ConvertDataIntoPoints(stringsToCompress);

            return new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);
        }

        public static string Decompress(string stringToDecompress)
        {
            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(stringToDecompress);

            return FileManager.ConvertPointsIntoData(decompressedLines);
        }

    }
}
