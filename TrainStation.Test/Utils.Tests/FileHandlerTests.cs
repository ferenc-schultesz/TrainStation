using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using TrainStation.Test.TestUtils;
using TrainStation.Utils;

namespace TrainStation.Test.Utils.Tests
{
    [TestFixture]
    public class FileHandlerTests
    {
        private readonly string testFileName;

        private readonly string fakeTestfileName;

        private FileHandler fileHandler;

        public FileHandlerTests()
        {
            this.testFileName = "Data\\FileHandler_UnitTestFile.txt";

            this.fakeTestfileName = "Fake.path";
        }

        [TearDown]
        public void TearDown()
        {
            TestDataHelper.DeleteTestFile(this.testFileName);
        }

        [Test]
        public void ReadTextFileLines_WhenFileExists_ShouldReturnTheLines()
        {
            // create file
            var content = new List<string> { "Line 1", "Line 2", "Line 3" };
            TestDataHelper.CreateTestFile(this.testFileName, content);
            this.fileHandler = new FileHandler(testFileName);

            var result = fileHandler.ReadTextFileLines();

            Assert.AreEqual(result.Count, 3);
        }

        [Test]
        public void ReadTextFileLines_WhenFileDoesNotExist_ShouldThrowException()
        {
            this.fileHandler = new FileHandler(fakeTestfileName);
            var ex = Assert.Throws<Exception>(() => fileHandler.ReadTextFileLines());
            Assert.That(ex.Message, Is.EqualTo($"File not found at {fakeTestfileName}"));
        }

        [Test]
        public void ReadTextFileCommaSeparated_WhenFileExists_ShouldReturnTheLines()
        {
            // create file
            var content = new List<string> { "Word 1, Word 2, Word 3" };
            TestDataHelper.CreateTestFile(this.testFileName, content);
            this.fileHandler = new FileHandler(testFileName);

            var result = fileHandler.ReadTextFileCommaSeparated();

            Assert.AreEqual(result.Count, 3);
        }

        [Test]
        public void ReadTextFileCommaSeparated_WhenFileDoesNotExist_ShouldThrowException()
        {
            this.fileHandler = new FileHandler(fakeTestfileName);
            var ex = Assert.Throws<Exception>(() => fileHandler.ReadTextFileCommaSeparated());
            Assert.That(ex.Message, Is.EqualTo($"File not found at {fakeTestfileName}"));
        }
    }
}
