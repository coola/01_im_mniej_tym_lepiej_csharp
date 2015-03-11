using System;
using System.Collections.Generic;

namespace compress
{
    public class Compressor
    {

        private CompressorAlgorithm Algorithm {get;set;}

        public Compressor(CompressorAlgorithm algorithm) {

            Algorithm = algorithm;
        
        }

        public object Compress(List<GPS_Point> points)
        {

            var compressedResult = String.Empty;

            foreach (var point in points)
            {
                compressedResult += Algorithm.CompressPoint(point);
            }

            return compressedResult ;
        }
    }
}
