using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Reflection;
using compress;


namespace tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly string _assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        [TestMethod]
        public void test_if_there_is_file_manager()
        {

            var fileManager = new FileManager();
            Assert.IsNotNull(fileManager);

        }

        [TestMethod]
        public void load_test_file_1()
        {
            var data1File = File.ReadAllLines(_assemblyPath + "/Data1.txt");
            Assert.IsNotNull(data1File);
        }

        [TestMethod]
        public void load_test_file_2()
        {
            var data1File = File.ReadAllLines(_assemblyPath + "/Data2.txt");
            Assert.IsNotNull(data1File);
        }

        [TestMethod]
        public void load_test_file_3()
        {
            var data1File = File.ReadAllLines(_assemblyPath + "/Data3.txt");
            Assert.IsNotNull(data1File);
        }

        [TestMethod]
        public void load_test_file_4()
        {
            var data1File = File.ReadAllLines(_assemblyPath + "/Data4.txt");
            Assert.IsNotNull(data1File);
        }

        [TestMethod]
        public void test_convert_data_into_points_1()
        {
            var data4File = File.ReadAllLines(_assemblyPath + "/Data4.txt");

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

            var compressedString = new Compressor(new SimpleCompressor()).Compress(points);

            Assert.AreEqual("7102990567851409", compressedString);

        }

        [TestMethod]
        public void test_first_compression_attempt_2()
        {

            var data = new [] { "71.029905,67.851409", "71.029905,67.851409" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new Compressor(new SimpleCompressor()).Compress(points);

            Assert.AreEqual("71029905678514097102990567851409", compressedString);

        }

        [TestMethod]
        public void test_first_compression_attempt_3()
        {

            var data = new [] { "71.029905,67.851409", "71.029905,67.851309" };

            var points = FileManager.ConvertDataIntoPoints(data);

            var compressedString = new Compressor(new SimpleCompressor()).Compress(points);

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

    }
}
