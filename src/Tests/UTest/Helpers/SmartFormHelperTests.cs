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
        [TestMethod()]
        public void GetFormHttpResponse_DefaultValues()
        {
            // Arrange
            var uri = "https://www.k2.com";
            var mockEnvironmentField = Mock.Of<EnvironmentField>();
            mockEnvironmentField.Value = uri;

            MockWrapperFactory.Instance.EnvironmentSettingsManager
                .Setup(x => x.GetItemByName(It.IsAny<string>()))
                .Returns(mockEnvironmentField);

            var expected = Guid.NewGuid().ToString();

            // Action
            var actual = SmartFormHelper.GetFormHttpResponse(expected);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void GetViewHttpResponse_DefaultValues()
        {
            // Arrange
            var uri = "https://www.k2.com";
            var mockEnvironmentField = Mock.Of<EnvironmentField>();
            mockEnvironmentField.Value = uri;

            MockWrapperFactory.Instance.EnvironmentSettingsManager
                .Setup(x => x.GetItemByName(It.IsAny<string>()))
                .Returns(mockEnvironmentField);

            var expected = Guid.NewGuid().ToString();

            // Action
            var actual = SmartFormHelper.GetViewHttpResponse(expected);

            // Assert
            Assert.IsNull(actual);
        }

        [TestInitialize()]
        public void TestInit()
        {
            MockWrapperFactory.MockInstance();
        }
    }
}