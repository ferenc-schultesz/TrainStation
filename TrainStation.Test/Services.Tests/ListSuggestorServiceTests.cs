using NUnit.Framework;
using System.Collections.Generic;
using TrainStation.Services;
using TrainStation.Test.TestUtils;

namespace TrainStation.Test.Services.Tests
{
    
    [TestFixture]
    public class ListSuggestorServiceTests
    {
        private ListSuggestorService service;

        private readonly string testFileName;

        public ListSuggestorServiceTests()
        {
            this.testFileName = "ListSuggestorService_UnitTestFile.txt";
        }

        [Test]
        public void ListSuggestorService_WhenDataFileExists_ShouldReturnSuggestions()
        {
            // Arrange
            var content = new List<string> { "aq",};
            TestDataHelper.CreateTestFile(this.testFileName, content);
            this.service = new ListSuggestorService(testFileName);

            // Act
            var result = this.service.GetSuggestions("a");

            // Assert
            Assert.IsNotNull(result);
        }

        [Test]
        public void ListSuggestorService_WhenEmptyStringPassed_ShouldReturnAllStations()
        {
            // Arrange
            var content = new List<string> { "a", "aa", "ab", "ac", "ad" };
            TestDataHelper.CreateTestFile(this.testFileName, content);
            this.service = new ListSuggestorService(testFileName);

            List<string> expectedStations = new List<string> { "a", "aa", "ab", "ac", "ad" };


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
        public void ListSuggestorService_WhenStationsWithUserInputPrefixExist_ShouldReturnTheCorrectStations()
        {
            // Arrange
            var content = new List<string> { "a", "aa", "ab", "ac", "aaa", "aac", "abb", "b", "ba" };
            TestDataHelper.CreateTestFile(this.testFileName, content);
            this.service = new ListSuggestorService(testFileName);

            List<string> expectedStations = new List<string> { "a", "aa", "ab", "ac", "aaa", "aac", "abb" };

            // Act
            var result = this.service.GetSuggestions("a");

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
            var content = new List<string> { "a", "aa", "ab", "ac", "aaa", "aac", "abb", "b", "ba" };
            TestDataHelper.CreateTestFile(this.testFileName, content);
            this.service = new ListSuggestorService(testFileName);

            List<string> expectedStations = new List<string> { };

            // Act
            var result = this.service.GetSuggestions("azx");

            // Assert
            Assert.AreEqual(expectedStations.Count, result.Stations.Count);
            Assert.AreEqual(result.Stations.Count, 0);
        }

        [Test]
        public void ListSuggestorService_WhenFurtherStationsWithUserInputPrefixExist_ShouldReturnTheCorrectNextLetters()
        {
            // Arrange
            var content = new List<string> { "a", "aa", "ab", "ac", "aaa", "aac", "abb", "b", "ba" };
            TestDataHelper.CreateTestFile(this.testFileName, content);
            this.service = new ListSuggestorService(testFileName);

            List<char> expectedNextLetters = new List<char> { 'a', 'b', 'c' };

            // Act
            var result = this.service.GetSuggestions("a");

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
            var content = new List<string> { "a", "aa", "ab", "ac", "aaa", "aac", "abb", "b", "ba" };
            TestDataHelper.CreateTestFile(this.testFileName, content);
            this.service = new ListSuggestorService(testFileName);

            List<char> expectedNextLetters = new List<char> {};

            // Act
            var result = this.service.GetSuggestions("aaa");

            // Assert
            Assert.AreEqual(expectedNextLetters.Count, result.NextLetters.Count);
            Assert.AreEqual(result.NextLetters.Count, 0);

        }
    }
}
