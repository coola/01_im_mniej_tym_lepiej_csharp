using System;
using System.Collections.Generic;
using System.Text;

namespace compress
{
    public class ConcatenateCompressorAlorithm : CompressorAlgorithm
    {
        public ConcatenateCompressorAlorithm(PointCompressorAlgorithm algorithm)
            : base(algorithm)
        {
        }

        public override string Compress(List<GPS_Point> points)
        {
            StringBuilder compressedResult = new StringBuilder();

            foreach (var point in points)
            {
                compressedResult.Append(Algorithm.CompressPoint(point));
            }

            return compressedResult.ToString();
        }
    }
}