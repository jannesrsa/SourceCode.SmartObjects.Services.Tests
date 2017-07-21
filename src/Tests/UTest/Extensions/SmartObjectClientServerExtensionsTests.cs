using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Tests.Managers;
using SourceCode.SmartObjects.Services.Tests.UTest.Factories;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;
using SourceCode.SmartObjects.Services.Tests.UTest.Properties;

namespace SourceCode.SmartObjects.Services.Tests.Extensions.Tests
{
    [TestClass()]
    public class SmartObjectClientServerExtensionsTests
    {
        [TestMethod()]
        public void Deserialize_With_ProcessInfo()
        {
            //Arrange
            var settings = new Mock<ServiceInstanceSettings>();
            settings.SetupGet(i => i.Name).Returns("K2_Management");

            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);
            var expected = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            var mockWrapperFactory = new MockWrapperFactory();
            mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<SearchProperty>(),
                    It.IsAny<SearchOperator>(),
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            mockWrapperFactory.SmartObjectClientServer
                .Setup(x => x.GetSmartObject(
                    It.IsAny<string>()))
                .Returns(expected);

            // Act
            var actual = SmartObjectClientServerExtensions.Deserialize(
                null,
                Guid.NewGuid().ToString(),
                settings.Object,
                Guid.NewGuid().ToString());

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DeserializeTypedArray_With_ProcessInfo()
        {
            //Arrange
            var settings = new Mock<ServiceInstanceSettings>();
            settings.SetupGet(i => i.Name).Returns("K2_Management");

            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            var mockWrapperFactory = new MockWrapperFactory();
            mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<SearchProperty>(),
                    It.IsAny<SearchOperator>(),
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            mockWrapperFactory.SmartObjectClientServer
                .Setup(x => x.GetSmartObject(
                    It.IsAny<string>()))
                .Returns(smartObject);

            var expected = new DataTable();
            mockWrapperFactory.SmartObjectClientServer
                .Setup(x => x.ExecuteListDataTable(
                    It.IsAny<SmartObject>(),
                    It.IsAny<ExecuteListOptions>()))
                .Returns(expected);

            // Act
            var actual = SmartObjectClientServerExtensions.DeserializeTypedArray(
                null,
                Guid.NewGuid().ToString(),
                settings.Object,
                Guid.NewGuid().ToString());

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SerializeAddItemToArray_With_ProcessInfo()
        {
            //Arrange
            var settings = new Mock<ServiceInstanceSettings>();
            settings.SetupGet(i => i.Name).Returns("K2_Management");

            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            var mockWrapperFactory = new MockWrapperFactory();
            mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<SearchProperty>(),
                    It.IsAny<SearchOperator>(),
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            mockWrapperFactory.SmartObjectClientServer
                .Setup(x => x.GetSmartObject(It.IsAny<string>()))
                .Returns(smartObject);

            mockWrapperFactory.SmartObjectClientServer
                .Setup(x => x.ExecuteScalar(It.IsAny<SmartObject>()))
                .Returns(smartObject);

            var expected = "[]";

            // Act
            var actual = SmartObjectClientServerExtensions.SerializeAddItemToArray(
                null,
                Guid.NewGuid().ToString(),
                expected,
                settings.Object,
                (SmartObject i) => { });

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Serialize_With_ProcessInfo()
        {
            //Arrange
            var serviceObjectName = Guid.NewGuid().ToString();
            var settings = new Mock<ServiceInstanceSettings>();
            settings.SetupGet(i => i.Name).Returns("K2_Management");

            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            var mockWrapperFactory = new MockWrapperFactory();
            mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<SearchProperty>(),
                    It.IsAny<SearchOperator>(),
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            mockWrapperFactory.SmartObjectClientServer
                .Setup(x => x.GetSmartObject(It.IsAny<string>()))
                .Returns(smartObject);

            mockWrapperFactory.SmartObjectClientServer
                .Setup(x => x.ExecuteScalar(It.IsAny<SmartObject>()))
                .Returns(smartObject);

            Action<SmartObject> action = (SmartObject i) => { };

            // Act
            var actual = SmartObjectClientServerExtensions.Serialize(
                null,
                serviceObjectName,
                settings.Object,
                action);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void SerializeItemToArray_With_ProcessInfo()
        {
            //Arrange
            var serviceObjectName = Guid.NewGuid().ToString();
            var settings = new Mock<ServiceInstanceSettings>();
            settings.SetupGet(i => i.Name).Returns("K2_Management");

            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            var mockWrapperFactory = new MockWrapperFactory();

            mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<SearchProperty>(),
                    It.IsAny<SearchOperator>(),
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            mockWrapperFactory.SmartObjectClientServer
                .Setup(x => x.GetSmartObject(It.IsAny<string>()))
                .Returns(smartObject);

            mockWrapperFactory.SmartObjectClientServer
                .Setup(x => x.ExecuteScalar(It.IsAny<SmartObject>()))
                .Returns(smartObject);

            Action<SmartObject> action = (SmartObject i) => { };

            // Act
            var actual = SmartObjectClientServerExtensions.SerializeItemToArray(
                null,
                serviceObjectName,
                settings.Object,
                action);

            // Assert
            Assert.IsNull(actual);
        }
    }
}