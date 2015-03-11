using System;
using System.Linq;


namespace compress
{
    public static class Strings
    {

        public static long ComputeStringsLength(String[] stringLines)
        {
            return stringLines.Sum(line => line.Length);
        }
    }
}
