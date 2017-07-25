using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Services.Tests.Managers;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;

namespace SourceCode.SmartObjects.Services.Tests.Extensions.Tests
{
    [TestClass()]
    public class SmartObjectClientServerExtensionsTests
    {
        [TestMethod()]
        public void Deserialize_With_ProcessInfo()
        {
            //Arrange
            MockWrapperFactory.Instance.WithProcessInstanceSmartObject(out SmartObject expected, out ServiceInstanceSettings settings);

            // Action
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
            MockWrapperFactory.Instance.WithProcessInstanceSmartObject(out SmartObject smartObject, out ServiceInstanceSettings settings);

            var expected = new DataTable();
            MockWrapperFactory.Instance.SmartObjectClientServer
                .Setup(x => x.ExecuteListDataTable(
                    It.IsAny<SmartObject>(),
                    It.IsAny<ExecuteListOptions>()))
                .Returns(expected);

            // Action
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
            MockWrapperFactory.Instance.WithProcessInstanceSmartObject(out SmartObject smartObject, out ServiceInstanceSettings settings);
            var serviceObjectName = Guid.NewGuid().ToString();
            Action<SmartObject> action = (SmartObject i) => { };

            // Action
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
            MockWrapperFactory.Instance.WithProcessInstanceSmartObject(out SmartObject smartObject, out ServiceInstanceSettings settings);

            var expected = "[]";

            // Action
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
            MockWrapperFactory.Instance.WithProcessInstanceSmartObject(out SmartObject smartObject, out ServiceInstanceSettings settings);

            var serviceObjectName = Guid.NewGuid().ToString();
            Action<SmartObject> action = (SmartObject i) => { };

            // Action
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
            MockWrapperFactory.MockInstance();
        }
    }
}