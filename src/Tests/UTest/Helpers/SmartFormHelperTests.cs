using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.EnvironmentSettings.Client;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class SmartFormHelperTests
    {
        private MockWrapperFactory _mockWrapperFactory;

        [TestMethod()]
        public void GetFormHttpResponse_DefaultValues()
        {
            // Arrange
            var uri = "https://www.k2.com";
            var mockEnvironmentField = Mock.Of<EnvironmentField>();
            mockEnvironmentField.Value = uri;

            _mockWrapperFactory.EnvironmentSettingsManager
                .Setup(x => x.GetItemByName(It.IsAny<string>()))
                .Returns(mockEnvironmentField);

            var expected = Guid.NewGuid().ToString();

            // Action
            SmartFormHelper.GetFormHttpResponse(expected);
        }

        [TestMethod()]
        public void GetViewHttpResponse_DefaultValues()
        {
            // Arrange
            var uri = "https://www.k2.com";
            var mockEnvironmentField = Mock.Of<EnvironmentField>();
            mockEnvironmentField.Value = uri;

            _mockWrapperFactory.EnvironmentSettingsManager
                .Setup(x => x.GetItemByName(It.IsAny<string>()))
                .Returns(mockEnvironmentField);

            var expected = Guid.NewGuid().ToString();

            // Action
            SmartFormHelper.GetViewHttpResponse(expected);
        }

        [TestInitialize()]
        public void TestInit()
        {
            _mockWrapperFactory = new MockWrapperFactory();
        }
    }
}