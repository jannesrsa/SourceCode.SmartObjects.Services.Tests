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
        private MockWrapperFactory _mockWrapperFactory;

        [TestMethod()]
        public void Deserialize_With_ProcessInfo()
        {
            //Arrange
            MockWithProcessInstanceSmartObject(out SmartObject expected, out ServiceInstanceSettings settings);

            // Act
            var actual = SmartObjectClientServerExtensions.Deserialize(
                null,
                Guid.NewGuid().ToString(),
                settings,
                Guid.NewGuid().ToString());

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void DeserializeTypedArray_With_ProcessInfo()
        {
            //Arrange
            MockWithProcessInstanceSmartObject(out SmartObject smartObject, out ServiceInstanceSettings settings);

            var expected = new DataTable();
            _mockWrapperFactory.SmartObjectClientServer
                .Setup(x => x.ExecuteListDataTable(
                    It.IsAny<SmartObject>(),
                    It.IsAny<ExecuteListOptions>()))
                .Returns(expected);

            // Act
            var actual = SmartObjectClientServerExtensions.DeserializeTypedArray(
                null,
                Guid.NewGuid().ToString(),
                settings,
                Guid.NewGuid().ToString());

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void Serialize_With_ProcessInfo()
        {
            //Arrange
            MockWithProcessInstanceSmartObject(out SmartObject smartObject, out ServiceInstanceSettings settings);
            var serviceObjectName = Guid.NewGuid().ToString();
            Action<SmartObject> action = (SmartObject i) => { };

            // Act
            var actual = SmartObjectClientServerExtensions.Serialize(
                null,
                serviceObjectName,
                settings,
                action);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void SerializeAddItemToArray_With_ProcessInfo()
        {
            //Arrange
            MockWithProcessInstanceSmartObject(out SmartObject smartObject, out ServiceInstanceSettings settings);

            var expected = "[]";

            // Act
            var actual = SmartObjectClientServerExtensions.SerializeAddItemToArray(
                null,
                Guid.NewGuid().ToString(),
                expected,
                settings,
                (SmartObject i) => { });

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SerializeItemToArray_With_ProcessInfo()
        {
            //Arrange
            MockWithProcessInstanceSmartObject(out SmartObject smartObject, out ServiceInstanceSettings settings);

            var serviceObjectName = Guid.NewGuid().ToString();
            Action<SmartObject> action = (SmartObject i) => { };

            // Act
            var actual = SmartObjectClientServerExtensions.SerializeItemToArray(
                null,
                serviceObjectName,
                settings,
                action);

            // Assert
            Assert.IsNull(actual);
        }

        [TestInitialize()]
        public void TestInit()
        {
            _mockWrapperFactory = new MockWrapperFactory();
        }

        private void MockWithProcessInstanceSmartObject(out SmartObject smartObject, out ServiceInstanceSettings serviceInstanceSettings)
        {
            smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);
            var settings = new Mock<ServiceInstanceSettings>();
            settings.SetupGet(i => i.Name).Returns("K2_Management");

            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            _mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<SearchProperty>(),
                    It.IsAny<SearchOperator>(),
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            _mockWrapperFactory.SmartObjectClientServer
                .Setup(x => x.GetSmartObject(
                    It.IsAny<string>()))
                .Returns(smartObject);

            _mockWrapperFactory.SmartObjectClientServer
                .Setup(x => x.ExecuteScalar(It.IsAny<SmartObject>()))
                .Returns(smartObject);

            serviceInstanceSettings = settings.Object;
        }
    }
}