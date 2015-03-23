
using System.Collections.Generic;
using System.IO;
using System.Linq;
using common;
using compressDecompress;
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

            Assert.AreEqual("71.029905,67.851409\r\n", stringData);
        }

        [TestMethod]
        public void test_first_decompression_attempt_1()
        {
            const string compressedTestString = "71|29905|67|851409";

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedTestString);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            Assert.AreEqual("71.029905,67.851409\r\n", stringData);
        }

        [TestMethod]
        public void test_first_decompression_attempt_2()
        {
            const string compressedTestString = "71|29905|67|851409|1|-1";

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedTestString);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            Assert.AreEqual("71.029905,67.851409\r\n71.029906,67.851408\r\n", stringData);
        }

        [TestMethod]
        public void test_first_decompression_attempt_3()
        {
            const string compressedTestString = "71|29905|67|851409|1|-1|-1|1";

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedTestString);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            Assert.AreEqual("71.029905,67.851409\r\n71.029906,67.851408\r\n71.029905,67.851409\r\n", stringData);
        }

        [TestMethod]
        public void test_first_decompression_attempt_4()
        {
            const string compressedTestString = "71|29905|67|851409|0|0|0|0";

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedTestString);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            Assert.AreEqual("71.029905,67.851409\r\n71.029905,67.851409\r\n71.029905,67.851409\r\n", stringData);
        }

        [TestMethod]
        public void test_first_decompression_attempt_5()
        {
            const string compressedTestString = "71|29905|67|851409|0|0|la1|0|0";

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedTestString);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            Assert.AreEqual("71.029905,67.851409\r\n71.029905,67.851409\r\n72.029905,67.851409\r\n", stringData);
        }

        [TestMethod]
        public void test_first_decompression_attempt_6()
        {
            const string compressedTestString = "71|29905|67|851409|la-1|lo-1|0|0|la1|lo1|0|0";

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedTestString);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            Assert.AreEqual("71.029905,67.851409\r\n70.029905,66.851409\r\n71.029905,67.851409\r\n", stringData);
        }

        [TestMethod]
        public void test_decompressed_saved_file_2()
        {
            var compressedData1 = File.ReadAllLines("out2.txt");

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedData1[0]);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            File.WriteAllText(@"Data2.decompressed.txt", stringData);

            Assert.IsTrue(FileEquals("Data2.txt", "Data2.decompressed.txt"));
        }

        [TestMethod]
        [Ignore]
        public void test_decompressed_saved_file_1()
        {
            var compressedData1 = File.ReadAllLines("out.txt");

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedData1[0]);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            File.WriteAllText(@"Data1.decompressed.txt", stringData);

            Assert.IsTrue(FileEquals("Data1.txt", "Data1.decompressed.txt"));
        }

        [TestMethod]
        public void test_compress_decompress_1()
        {

            var data1File = File.ReadAllLines("generatedCoords_4steps_randDirection.txt");

            var points = FileManager.ConvertDataIntoPoints(data1File);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            File.WriteAllText(@"generatedCoords_4steps_randDirection.decompressed.txt", compressedString);

            var compressedData1 = File.ReadAllLines("generatedCoords_4steps_randDirection.decompressed.txt");

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedData1[0]);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            File.WriteAllText(@"generatedCoords_4steps_randDirection.compressed.txt", stringData);

            Assert.IsTrue(FileEquals("generatedCoords_4steps_randDirection.txt", "generatedCoords_4steps_randDirection.compressed.txt"));


            byte[] file1 = File.ReadAllBytes("generatedCoords_4steps_randDirection.txt");
            byte[] file2 = File.ReadAllBytes("generatedCoords_4steps_randDirection.decompressed.txt");

            Assert.Fail("Value original:{0} result:{1}  ratio:{2}", file1.Length, file2.Length, (double)file1.Length / file2.Length);
        }

     
        [TestMethod]
        public void test_compress_decompress_2()
        {

            var data1File = File.ReadAllLines("generatedCoords_constStep_constDirection.txt");

            var points = FileManager.ConvertDataIntoPoints(data1File);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            File.WriteAllText(@"generatedCoords_constStep_constDirection.decompressed.txt", compressedString);

            var compressedData1 = File.ReadAllLines("generatedCoords_constStep_constDirection.decompressed.txt");

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedData1[0]);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            File.WriteAllText(@"generatedCoords_constStep_constDirection.compressed.txt", stringData);

            Assert.IsTrue(FileEquals("generatedCoords_constStep_constDirection.txt", "generatedCoords_constStep_constDirection.compressed.txt"));


            byte[] file1 = File.ReadAllBytes("generatedCoords_constStep_constDirection.txt");
            byte[] file2 = File.ReadAllBytes("generatedCoords_constStep_constDirection.decompressed.txt");

            Assert.Fail("Value original:{0} result:{1}  ratio:{2}", file1.Length, file2.Length, (double)file1.Length / file2.Length);
        }

        [TestMethod]
        public void test_compress_decompress_3()
        {

            var data1File = File.ReadAllLines("generatedCoords_constStep_randDirection.txt");

            var points = FileManager.ConvertDataIntoPoints(data1File);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            File.WriteAllText(@"generatedCoords_constStep_randDirection.decompressed.txt", compressedString);

            var compressedData1 = File.ReadAllLines("generatedCoords_constStep_randDirection.decompressed.txt");

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedData1[0]);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            File.WriteAllText(@"generatedCoords_constStep_randDirection.compressed.txt", stringData);

            Assert.IsTrue(FileEquals("generatedCoords_constStep_randDirection.txt", "generatedCoords_constStep_randDirection.compressed.txt"));

            byte[] file1 = File.ReadAllBytes("generatedCoords_constStep_randDirection.txt");
            byte[] file2 = File.ReadAllBytes("generatedCoords_constStep_randDirection.decompressed.txt");

            Assert.Fail("Value original:{0} result:{1}  ratio:{2}", file1.Length, file2.Length, (double)file1.Length / file2.Length);
        }

        [TestMethod]
        public void test_compress_decompress_4()
        {

            var data1File = File.ReadAllLines("generatedCoords_constStep_randDirection_50mb.txt");

            var points = FileManager.ConvertDataIntoPoints(data1File);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            File.WriteAllText(@"generatedCoords_constStep_randDirection_50mb.decompressed.txt", compressedString);

            var compressedData1 = File.ReadAllLines("generatedCoords_constStep_randDirection_50mb.decompressed.txt");

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedData1[0]);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            File.WriteAllText(@"generatedCoords_constStep_randDirection_50mb.compressed.txt", stringData);

            Assert.IsTrue(FileEquals("generatedCoords_constStep_randDirection_50mb.txt", "generatedCoords_constStep_randDirection_50mb.compressed.txt"));

            byte[] file1 = File.ReadAllBytes("generatedCoords_constStep_randDirection_50mb.txt");
            byte[] file2 = File.ReadAllBytes("generatedCoords_constStep_randDirection_50mb.decompressed.txt");

            Assert.Fail("Value original:{0} result:{1}  ratio:{2}", file1.Length, file2.Length, (double)file1.Length / file2.Length);
        }

        [TestMethod]
        public void test_compress_decompress_5()
        {

            var data1File = File.ReadAllLines("generatedCoords_randStep_randDirection.txt");

            var points = FileManager.ConvertDataIntoPoints(data1File);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            File.WriteAllText(@"generatedCoords_randStep_randDirection.decompressed.txt", compressedString);

            var compressedData1 = File.ReadAllLines("generatedCoords_randStep_randDirection.decompressed.txt");

            var decompressedLines = new DeltaAlgorithm(new DeltaPointAlgorithm()).Decompress(compressedData1[0]);

            var stringData = FileManager.ConvertPointsIntoData(decompressedLines);

            File.WriteAllText(@"generatedCoords_randStep_randDirection.compressed.txt", stringData);

            Assert.IsTrue(FileEquals("generatedCoords_randStep_randDirection.txt", "generatedCoords_randStep_randDirection.compressed.txt"));

            byte[] file1 = File.ReadAllBytes("generatedCoords_randStep_randDirection.txt");
            byte[] file2 = File.ReadAllBytes("generatedCoords_randStep_randDirection.decompressed.txt");

            Assert.Fail("Value original:{0} result:{1}  ratio:{2}", file1.Length, file2.Length, (double)file1.Length / file2.Length);

        }


        static bool FileEquals(string path1, string path2)
        {
            byte[] file1 = File.ReadAllBytes(path1);
            byte[] file2 = File.ReadAllBytes(path2);
            if (file1.Length == file2.Length)
            {
                return !file1.AsParallel().Where((t, i) => t != file2[i]).AsParallel().Any();
            }
            return false;
        }

    }
}
