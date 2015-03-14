using System;
using System.Collections.Generic;
using System.Text;
using common;

namespace compressDecompress
{
    public class ConcatenateAlgorithm : Algorithm
    {
        public ConcatenateAlgorithm(PointAlgorithm decompressorAlgorithm)
            : base(decompressorAlgorithm)
        {
        }

        public override string Compress(List<GPS_Point> points)
        {
            var compressedResult = new StringBuilder();

            foreach (var point in points)
            {
                compressedResult.Append(InnerAlgorithm.CompressPoint(point));
            }

            return compressedResult.ToString();
        }

        public override List<GPS_Point> Decompress(string compressedString)
        {
            throw new NotImplementedException();
        }
    }
}