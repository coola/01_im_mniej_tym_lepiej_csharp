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

    }
}
