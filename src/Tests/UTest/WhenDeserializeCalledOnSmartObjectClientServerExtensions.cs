using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Tests.Extensions;
using SourceCode.SmartObjects.Services.Tests.Helpers;
using SourceCode.SmartObjects.Services.Tests.Interfaces;
using SourceCode.SmartObjects.Services.Tests.Managers;
using SourceCode.SmartObjects.Services.Tests.UTest.Factories;
using SourceCode.SmartObjects.Services.Tests.UTest.Properties;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenDeserializeCalledOnSmartObjectClientServerExtensions
    {
        [TestMethod()]
        public void With_ProcessInfo()
        {
            //Arrange
            var serviceObjectName = Guid.NewGuid().ToString();
            var settings = new Mock<ServiceInstanceSettings>();
            settings.SetupGet(i => i.Name).Returns("K2_Management");

            var value = Guid.NewGuid().ToString();

            var mockSmartObjectClientServer = new Mock<SmartObjectClientServerWrapper>();
            var mockConnectionProvider = new Mock<IConnectionProvider>();
            var mockSmartObjectManagementServer = new Mock<SmartObjectManagementServerWrapper>();

            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);

            var expected = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            mockSmartObjectManagementServer.Setup(i => i.GetSmartObjects(It.IsAny<SearchProperty>(), It.IsAny<SearchOperator>(), It.IsAny<string>())).Returns(mockSmartObjectExplorer);
            mockConnectionProvider.Setup(x => x.GetServer<SmartObjectManagementServerWrapper>()).Returns(mockSmartObjectManagementServer.Object);
            mockSmartObjectClientServer.Setup(x => x.GetSmartObject(It.IsAny<string>())).Returns(expected);

            ConnectionHelper.UpdateConnectionProvider(mockConnectionProvider.Object);

            // Act
            var actual = mockSmartObjectClientServer.Object.Deserialize(serviceObjectName, settings.Object, value);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithServerNull()
        {
            //Arrange
            SmartObjectClientServer server = null;
            var serviceObjectName = Guid.NewGuid().ToString();
            var settings = new Mock<ServiceInstanceSettings>();
            var value = Guid.NewGuid().ToString();

            // Act
            SmartObjectClientServerExtensions.Deserialize(server, serviceObjectName, settings.Object, value);
        }
    }
}