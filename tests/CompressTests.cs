using System.IO;
using System.Reflection;
using common;
using compressDecompress;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace tests
{
    [TestClass]
    public class CompressTests
    {
        private static readonly string AssemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        [TestMethod]
        public void test_if_there_is_file_manager()
        {
            var fileManager = new FileManager();
            Assert.IsNotNull(fileManager);
        }

        [TestMethod]
        [Ignore]
        public void load_test_file_1()
        {
            var data1File = File.ReadAllLines(AssemblyPath + "/Data1.txt");
            Assert.IsNotNull(data1File);
        }

        [TestMethod]
        public void load_test_file_2()
        {
            var data1File = File.ReadAllLines(AssemblyPath + "/Data2.txt");
            Assert.IsNotNull(data1File);
        }

        [TestMethod]
        public void load_test_file_3()
        {
            var data1File = File.ReadAllLines(AssemblyPath + "/Data3.txt");
            Assert.IsNotNull(data1File);
        }

        [TestMethod]
        public void load_test_file_4()
        {
            var data1File = File.ReadAllLines(AssemblyPath + "/Data4.txt");
            Assert.IsNotNull(data1File);
        }

        [TestMethod]
        public void test_convert_data_into_points_1()
        {
            var data4File = File.ReadAllLines(AssemblyPath + "/Data4.txt");

            var points = FileManager.ConvertDataIntoPoints(data4File);

            Assert.AreEqual(1, points.Count);
        }

        [TestMethod]
        public void test_convert_data_into_points_2()
        {
            var data = new [] {"71.029905,67.851409"};

            var points = FileManager.ConvertDataIntoPoints(data);

            Assert.AreEqual(1, points.Count);
            Assert.AreEqual("71.029905", points[0].Latitude);
            Assert.AreEqual("67.851409", points[0].Longitude);            
        }

        [TestMethod]
        public void test_first_compression_attempt_1() {

            var data = new [] { "71.029905,67.851409" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new ConcatenateAlgorithm(new SimplePointAlgorithm()).Compress(points);

            Assert.AreEqual("7102990567851409", compressedString);

        }

        [TestMethod]
        public void test_first_compression_attempt_2()
        {

            var data = new [] { "71.029905,67.851409", "71.029905,67.851409" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new ConcatenateAlgorithm(new SimplePointAlgorithm()).Compress(points);

            Assert.AreEqual("71029905678514097102990567851409", compressedString);

        }

        [TestMethod]
        public void test_first_compression_attempt_3()
        {

            var data = new [] { "71.029905,67.851409", "71.029905,67.851309" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new ConcatenateAlgorithm(new SimplePointAlgorithm()).Compress(points);

            Assert.AreEqual("71029905678514097102990567851309", compressedString);

        }

        [TestMethod]
        public void test_strings_length()
        {

            var data = new [] { "7", "89" };

             Assert.AreEqual(3, Strings.ComputeStringsLength(data));
        }

        [TestMethod]
        public void test_sgps_points_split_for_ints()
        {

            var data = new [] { "71.029905,67.851409" };

            var points = FileManager.ConvertDataIntoPoints(data);

            Assert.AreEqual(71, points[0].GetLatitudeLeftSide);

            Assert.AreEqual(029905, points[0].GetLatitudeRightSide);

            Assert.AreEqual(67, points[0].GetLongitudeLeftSide);

            Assert.AreEqual(851409, points[0].GetLongitudeRightSide);
        }

        [TestMethod]
        public void test_first_compression_attempt_4()
        {

            var data = new[] { "71.029905,67.851409"};

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            Assert.AreEqual("71|29905|67|851409", compressedString);

        }

        [TestMethod]
        public void test_first_compression_attempt_5()
        {

            var data = new[] { "71.029905,67.851409", "71.029904,67.851410" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            Assert.AreEqual("71|29905|67|851409|-1|1", compressedString);
        }

        [TestMethod]
        public void test_first_compression_attempt_6()
        {

            var data = new[] { "71.029904,67.851409", "71.029904,67.851409" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            Assert.AreEqual("71|29904|67|851409|0|0", compressedString);
        }

        [TestMethod]
        public void test_first_compression_attempt_7()
        {

            var data = new[] { "71.029904,67.851409", "71.029903,67.851408" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            Assert.AreEqual("71|29904|67|851409|-1|-1", compressedString);
        }

        [TestMethod]
        public void test_first_compression_attempt_8()
        {

            var data = new[] { "71.029904,67.851409", "71.029905,67.851408" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            Assert.AreEqual("71|29904|67|851409|1|-1", compressedString);
        }

        [TestMethod]
        public void test_first_compression_attempt_9()
        {

            var data = new[] { "71.029904,67.851409", "71.029905,67.851408", "71.029904,67.851409" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            Assert.AreEqual("71|29904|67|851409|1|-1|-1|1", compressedString);
        }

        [TestMethod]
        public void test_first_compression_attempt_10()
        {

            var data = new[] { "71.029904,67.851409", "71.029904,67.851409", "71.029904,67.851409" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            Assert.AreEqual("71|29904|67|851409|0|0|0|0", compressedString);
        }

        [TestMethod]
        public void test_first_compression_attempt_11()
        {

            var data = new[] { "71.029904,67.851409", "71.029904,67.851409", "72.029904,67.851409" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            Assert.AreEqual("71|29904|67|851409|0|0|la1|0|0", compressedString);
        }


        [TestMethod]
        public void test_first_compression_attempt_12()
        {

            var data = new[] { "71.029904,67.851409", "71.029904,67.851409", "71.029904,68.851409" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            Assert.AreEqual("71|29904|67|851409|0|0|lo1|0|0", compressedString);
        }

        [TestMethod]
        public void test_first_compression_attempt_13()
        {

            var data = new[] { "71.029904,67.851409", "71.029904,67.851409", "72.029904,68.851409" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            Assert.AreEqual("71|29904|67|851409|0|0|la1|lo1|0|0", compressedString);
        }

        [TestMethod]
        public void test_first_compression_attempt_14()
        {

            var data = new[] { "71.029904,67.851409", "72.029904,68.851409", "71.029904,67.851409" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            Assert.AreEqual("71|29904|67|851409|la1|lo1|0|0|la-1|lo-1|0|0", compressedString);
        }

        [TestMethod]
        [Ignore]
        public void test_first_compression_file_attempt_1()
        {
            CompressWithFile("Data1.txt");
        }
        [TestMethod]
        public void test_first_compression_file_attempt_2()
        {
            CompressWithFile("Data2.txt");
        }
        [TestMethod]
        public void test_first_compression_file_attempt_3()
        {
            CompressWithFile("Data3.txt");
        }
        [TestMethod]
        public void test_first_compression_file_attempt_4()
        {
            CompressWithFile("Data4.txt");
        }

        private static void CompressWithFile(string filename)
        {
            var data1File = File.ReadAllLines(AssemblyPath + "/" + filename);

            var points = FileManager.ConvertDataIntoPoints(data1File);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            var originalLength = data1File.Length;

            var compressedLength = compressedString.Length;

            Assert.IsTrue(originalLength < compressedLength);
        }

        [TestMethod]
        [Ignore]
        public void test_saving_compressed_to_file()
        {
            var data1File = File.ReadAllLines(AssemblyPath + "/" + "Data1.txt");

            var points = FileManager.ConvertDataIntoPoints(data1File);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            File.WriteAllText(@"out.txt", compressedString);

        }

        [TestMethod]
        public void test_saving_compressed_to_file_1()
        {
            var data1File = File.ReadAllLines(AssemblyPath + "/" + "Data2.txt");

            var points = FileManager.ConvertDataIntoPoints(data1File);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            File.WriteAllText(@"out2.txt", compressedString);

        }

        [TestMethod]
        public void test_saving_compressed_to_file_3()
        {
            var data1File = File.ReadAllLines(AssemblyPath + "/" + "Data3.txt");

            var points = FileManager.ConvertDataIntoPoints(data1File);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            File.WriteAllText(@"out3.txt", compressedString);

        }

        [TestMethod]
        public void test_saving_compressed_to_file_4()
        {
            var data1File = File.ReadAllLines(AssemblyPath + "/" + "Data4.txt");

            var points = FileManager.ConvertDataIntoPoints(data1File);

            var compressedString = new DeltaAlgorithm(new DeltaPointAlgorithm()).Compress(points);

            File.WriteAllText(@"out4.txt", compressedString);

        }



    }
}
