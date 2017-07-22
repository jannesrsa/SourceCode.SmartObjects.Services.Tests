using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.EnvironmentSettings.Client;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class EnvironmentHelperTests
    {
        private MockWrapperFactory _mockWrapperFactory;

        [TestMethod()]
        public void GetEnvironmentFieldByName_DefaultValue()
        {
            // Arrange
            var expected = Guid.NewGuid().ToString();
            var mockEnvironmentField = Mock.Of<EnvironmentField>();
            mockEnvironmentField.Value = expected;

            _mockWrapperFactory.EnvironmentSettingsManager
                .Setup(x => x.GetItemByName(It.IsAny<string>()))
                .Returns(mockEnvironmentField);

            // Action
            var actual = EnvironmentHelper.GetEnvironmentFieldByName(Guid.NewGuid().ToString());

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetEnvironmentFieldByName_UseCache()
        {
            // Arrange
            var expected = Guid.NewGuid().ToString();
            var mockEnvironmentField = Mock.Of<EnvironmentField>();
            mockEnvironmentField.Value = expected;

            _mockWrapperFactory.EnvironmentSettingsManager
                .Setup(x => x.GetItemByName(It.IsAny<string>()))
                .Returns(mockEnvironmentField);

            var name = Guid.NewGuid().ToString();

            // Action
            EnvironmentHelper.GetEnvironmentFieldByName(name);
            var actual = EnvironmentHelper.GetEnvironmentFieldByName(name);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestInitialize()]
        public void TestInit()
        {
            _mockWrapperFactory = new MockWrapperFactory();
        }
    }
}