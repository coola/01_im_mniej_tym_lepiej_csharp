
using System.Collections.Generic;
using System.IO;
using common;
using compress;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests
{
    [TestClass]
    public class DecompressTests
    {

        [TestMethod]
        public void test_convert_points_into_data()
        {
            var testPoints = new List<GPS_Point>
            {
                new GPS_Point {Latitude = "71.029905", Longitude = "67.851409"}
            };

            var stringData = FileManager.ConvertPointsIntoData(testPoints);

            Assert.AreEqual("71.029905,67.851409", stringData);
        }

        [TestMethod]
        public void test_first_decompression_attempt_1()
        {
            const string compressedTestString = "71|29905|67|851409";

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedTestString);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            Assert.AreEqual("71.029905,67.851409", stringData);
        }

        [TestMethod]
        public void test_first_decompression_attempt_2()
        {
            const string compressedTestString = "71|29905|67|851409|1|-1";

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedTestString);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            Assert.AreEqual("71.029905,67.851409\n71.029906,67.851408", stringData);
        }

        [TestMethod]
        public void test_first_decompression_attempt_3()
        {
            const string compressedTestString = "71|29905|67|851409|1|-1|-1|1";

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedTestString);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            Assert.AreEqual("71.029905,67.851409\n71.029906,67.851408\n71.029905,67.851409", stringData);
        }

    }
}
