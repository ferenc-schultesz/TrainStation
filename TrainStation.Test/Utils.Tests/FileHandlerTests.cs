﻿using NUnit.Framework;
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

        private readonly FileHandler fileHandler;

        public FileHandlerTests()
        {
            this.testFileName = "FileHandler_UnitTestFile.txt";
            this.fileHandler = new FileHandler();
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

            var result = fileHandler.ReadTextFileLines(testFileName);

            Assert.AreEqual(result.Count, 3);
        }

        [Test]
        public void ReadTextFileLines_WhenFileDoesNotExist_ShouldThrowException()
        {
            string fakePath = "DoesNotExist.txt";
            var ex = Assert.Throws<Exception>(() => fileHandler.ReadTextFileLines(fakePath));
            Assert.That(ex.Message, Is.EqualTo($"File not found at {fakePath}"));
        }

        [Test]
        public void ReadTextFileCommaSeparated_WhenFileExists_ShouldReturnTheLines()
        {
            // create file
            var content = new List<string> { "Word 1, Word 2, Word 3" };
            TestDataHelper.CreateTestFile(this.testFileName, content);

            var result = fileHandler.ReadTextFileCommaSeparated(testFileName);

            Assert.AreEqual(result.Count, 3);
        }

        [Test]
        public void ReadTextFileCommaSeparated_WhenFileDoesNotExist_ShouldThrowException()
        {
            string fakePath = "DoesNotExist.txt";
            var ex = Assert.Throws<Exception>(() => fileHandler.ReadTextFileCommaSeparated(fakePath));
            Assert.That(ex.Message, Is.EqualTo($"File not found at {fakePath}"));
        }
    }
}
