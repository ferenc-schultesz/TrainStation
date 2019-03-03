using NUnit.Framework;
using System.Collections.Generic;
using TrainStation.Services;
using TrainStation.Test.TestUtils;
using TrainStation.Utils;
using Moq;
using System.Linq;

namespace TrainStation.Test.Services.Tests
{
    
    [TestFixture]
    public class ListSuggestorServiceTests
    {
        private ListSuggestorService service;

        private readonly string testFileName;

        private Mock<IFileHandler> fileHandler;

        public ListSuggestorServiceTests()
        {
            this.testFileName = "ListSuggestorService_UnitTestFile.txt";
        }

        [OneTimeSetUp]
        public void Init()
        {
            fileHandler = new Mock<IFileHandler>();
        }

        [Test]
        [TestCase("a")]
        [TestCase("Aq")]
        [TestCase("")]
        public void ListSuggestorService_WhenDataFileExists_ShouldReturnSuggestions(string input)
        {
            // Arrange
            var content = new List<string> { "AQ",};
            fileHandler.Setup(fh => fh.ReadTextFileLines(It.IsAny<string>())).Returns(content);
            this.service = new ListSuggestorService(testFileName, fileHandler.Object);

            // Act
            var result = this.service.GetSuggestions(input);

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ListSuggestorService_WhenEmptyStringPassed_ShouldReturnAllStations()
        {
            // Arrange
            var content = new List<string> { "A", "AA", "AB", "AC", "AD" };
            fileHandler.Setup(fh => fh.ReadTextFileLines(It.IsAny<string>())).Returns(content);
            this.service = new ListSuggestorService(testFileName, fileHandler.Object);

            List<string> expectedStations = new List<string> { "A", "AA", "AB", "AC", "AD" };


            // Act
            var result = this.service.GetSuggestions("");

            // Assert
            Assert.AreEqual(result.Stations.Count, 5);
            foreach (string expectedStation in expectedStations)
            {
                Assert.Contains(expectedStation, result.Stations);
            }
        }

        [Test]
        [TestCase("a", new string[] { "A", "AA", "AB", "AC", "AAA", "AAC", "ABB" })]
        [TestCase("A", new string[] { "A", "AA", "AB", "AC", "AAA", "AAC", "ABB" })]
        [TestCase("Aa", new string[] { "AA", "AAA", "AAC" })]
        public void ListSuggestorService_WhenStationsWithUserInputPrefixExist_ShouldReturnTheCorrectStations(string input, string[] expected)
        {
            // Arrange
            var content = new List<string> { "A", "AA", "AB", "AC", "AAA", "AAC", "ABB", "B", "BA" };
            fileHandler.Setup(fh => fh.ReadTextFileLines(It.IsAny<string>())).Returns(content);
            this.service = new ListSuggestorService(testFileName, fileHandler.Object);

            List<string> expectedStations = expected.ToList();

            // Act
            var result = this.service.GetSuggestions(input);

            // Assert
            Assert.AreEqual(expectedStations.Count, result.Stations.Count);
            foreach (string expectedStation in expectedStations)
            {
                Assert.Contains(expectedStation, result.Stations);
            }
        }

        [Test]
        public void ListSuggestorService_WhenStationsWithUserInputPrefixDoesNotExist_ShouldReturnEmptyListForStations()
        {
            // Arrange
            var content = new List<string> { "A", "AA", "AB", "AC", "AAA", "AAC", "ABB", "B", "BA" };
            fileHandler.Setup(fh => fh.ReadTextFileLines(It.IsAny<string>())).Returns(content);
            this.service = new ListSuggestorService(testFileName, fileHandler.Object);

            List<string> expectedStations = new List<string> { };

            // Act
            var result = this.service.GetSuggestions("azx");

            // Assert
            Assert.AreEqual(expectedStations.Count, result.Stations.Count);
            Assert.AreEqual(result.Stations.Count, 0);
        }

        [Test]
        [TestCase("a", new char[] { 'A', 'B', 'C' })]
        [TestCase("A", new char[] { 'A', 'B', 'C' })]
        [TestCase("Aa", new char[] { 'A', 'C' })]
        public void ListSuggestorService_WhenFurtherStationsWithUserInputPrefixExist_ShouldReturnTheCorrectNextLetters(string input, char[] expected)
        {
            // Arrange
            var content = new List<string> { "A", "AA", "AB", "AC", "AAA", "AAC", "ABB", "B", "BA" };
            fileHandler.Setup(fh => fh.ReadTextFileLines(It.IsAny<string>())).Returns(content);
            this.service = new ListSuggestorService(testFileName, fileHandler.Object);

            List<char> expectedNextLetters = expected.ToList();

            // Act
            var result = this.service.GetSuggestions(input);

            // Assert
            Assert.AreEqual(expectedNextLetters.Count, result.NextLetters.Count);
            foreach (char nextLetter in expectedNextLetters)
            {
                Assert.Contains(nextLetter, result.NextLetters);
            }
        }

        [Test]
        public void ListSuggestorService_WhenFurtherStationsWithUserInputPrefixDoesNotExist_ShouldNotReturnNextLetters()
        {
            // Arrange
            var content = new List<string> { "A", "AA", "AB", "AC", "AAA", "AAC", "ABB", "B", "BA" };
            fileHandler.Setup(fh => fh.ReadTextFileLines(It.IsAny<string>())).Returns(content);
            this.service = new ListSuggestorService(testFileName, fileHandler.Object);

            List<char> expectedNextLetters = new List<char> {};

            // Act
            var result = this.service.GetSuggestions("aaa");

            // Assert
            Assert.AreEqual(expectedNextLetters.Count, result.NextLetters.Count);
            Assert.AreEqual(result.NextLetters.Count, 0);

        }
    }
}
