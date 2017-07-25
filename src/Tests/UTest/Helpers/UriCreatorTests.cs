using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class UriCreatorTests
    {
        [TestMethod()]
        public void CreateSanitizedPathUri_WithDefaultValues()
        {
            // Arrange
            var expected = new Uri("http://www.k2.com");

            // Action
            var actual = UriCreator.CreateSanitizedPathUri(UriKind.Absolute, expected.ToString());

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void CreateSanitizedPathUri_WithEmptyString()
        {
            // Arrange
            var pathSegment1 = string.Empty;

            // Action
            var actual = UriCreator.CreateSanitizedPathUri(UriKind.Absolute, pathSegment1);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void CreateSanitizedPathUri_WithEmptyStringArray()
        {
            // Arrange
            var pathSegment1 = new string[] { };

            // Action
            var actual = UriCreator.CreateSanitizedPathUri(UriKind.Absolute, pathSegment1);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void CreateSanitizedPathUri_WithNoParams()
        {
            // Action
            var actual = UriCreator.CreateSanitizedPathUri(UriKind.Absolute);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void CreateSanitizedPathUri_WithNullPathSegments()
        {
            // Arrange
            string[] pathSegment1 = null;

            // Action
            var actual = UriCreator.CreateSanitizedPathUri(UriKind.Absolute, pathSegment1);

            // Assert
            Assert.IsNull(actual);
        }
    }
}