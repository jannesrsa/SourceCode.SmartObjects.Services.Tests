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

            var mockWrapperFactory = new MockWrapperFactory();
            mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            // Act
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

            var mockWrapperFactory = new MockWrapperFactory();
            mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            // Act
            SmartObjectManagementServerExtensions.DeleteSmartObject(null, smartObjectInfo.Name);
        }

        [TestMethod()]
        public void DeleteSmartObjects_ReturnTrue()
        {
            // Arrange
            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            var mockWrapperFactory = new MockWrapperFactory();

            mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<Guid>()))
                .Returns(mockSmartObjectExplorer);

            mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            // Act
            SmartObjectManagementServerExtensions.DeleteSmartObjects(null, smartObjectInfo.ServiceInstanceGuid);
        }
    }
}