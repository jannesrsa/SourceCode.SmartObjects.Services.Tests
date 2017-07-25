using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;
using SourceCode.SmartObjects.Services.Tests.UTest.Properties;

namespace SourceCode.SmartObjects.Services.Tests.Extensions.Tests
{
    [TestClass()]
    public class SmartObjectManagementServerExtensionsTests
    {
        [TestMethod()]
        public void ContainsSmartObject_ReturnTrue()
        {
            // Arrange
            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            MockWrapperFactory.Instance.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            // Action
            var actual = SmartObjectManagementServerExtensions.ContainsSmartObject(null, smartObjectInfo.Name);

            // Assert
            Assert.AreEqual(true, actual);
        }

        [TestMethod()]
        public void DeleteSmartObject_ReturnTrue()
        {
            // Arrange
            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            MockWrapperFactory.Instance.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            // Action
            SmartObjectManagementServerExtensions.DeleteSmartObject(null, smartObjectInfo.Name);
        }

        [TestMethod()]
        public void DeleteSmartObjects_ReturnTrue()
        {
            // Arrange
            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            MockWrapperFactory.Instance.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<Guid>()))
                .Returns(mockSmartObjectExplorer);

            MockWrapperFactory.Instance.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            // Action
            SmartObjectManagementServerExtensions.DeleteSmartObjects(null, smartObjectInfo.ServiceInstanceGuid);
        }

        [TestInitialize()]
        public void TestInit()
        {
            MockWrapperFactory.MockInstance();
        }
    }
}