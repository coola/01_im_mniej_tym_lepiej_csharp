using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using compress;
using System.IO;
using System.Reflection;

namespace tests
{
    [TestClass]
    public class UnitTest1
    {
        private string assemblyPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        [TestMethod]
        public void test_if_there_is_file_manager()
        {

            FileManager fileManager = new FileManager();
            Assert.IsNotNull(fileManager);

        }

        [TestMethod]
        public void load_test_file_1()
        {
            var data1File = File.ReadAllLines(assemblyPath + "/Data1.txt");
            Assert.IsNotNull(data1File);
        }

        [TestMethod]
        public void load_test_file_2()
        {
            var data1File = File.ReadAllLines(assemblyPath + "/Data2.txt");
            Assert.IsNotNull(data1File);
        }

        [TestMethod]
        public void load_test_file_3()
        {
            var data1File = File.ReadAllLines(assemblyPath + "/Data3.txt");
            Assert.IsNotNull(data1File);
        }

        [TestMethod]
        public void load_test_file_4()
        {
            var data1File = File.ReadAllLines(assemblyPath + "/Data4.txt");
            Assert.IsNotNull(data1File);
        }

        [TestMethod]
        public void test_convert_data_into_points_1()
        {
            var data4File = File.ReadAllLines(assemblyPath + "/Data4.txt");

            var points = FileManager.convertDataIntoPoints(data4File);

            Assert.AreEqual(1, points.Count);
        }

        [TestMethod]
        public void test_convert_data_into_points_2()
        {
            var data = new string [] {"71.029905,67.851409"};

            var points = FileManager.convertDataIntoPoints(data);

            Assert.AreEqual(1, points.Count);
            Assert.AreEqual("71.029905", points[0].Latitude);
            Assert.AreEqual("67.851409", points[0].Longitude);            
        }
    }
}
