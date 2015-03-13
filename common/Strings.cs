using System;
using System.Linq;

namespace common
{
    public static class Strings
    {

        public static long ComputeStringsLength(String[] stringLines)
        {
            return stringLines.Sum(line => line.Length);
        }

        public static string PadWithZeroes(string stringToPad)
        {
            return stringToPad.PadLeft(GPS_Point.LengthOfRightSide, '0');
        }
    }
}
