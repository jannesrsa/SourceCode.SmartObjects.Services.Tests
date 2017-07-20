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

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenSerializeCalledOnSmartObjectClientServerExtensions
    {
        [TestMethod()]
        public void With_ProcessInfo()
        {
            //Arrange
            var serviceObjectName = Guid.NewGuid().ToString();
            var settings = new Mock<ServiceInstanceSettings>();
            settings.SetupGet(i => i.Name).Returns("K2_Management");

            var mockSmartObjectClientServer = new Mock<ISmartObjectClientServer>();
            var mockConnectionProvider = new Mock<IConnectionProvider>();
            var mockSmartObjectManagementServer = new Mock<ISmartObjectManagementServer>();

            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);

            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            mockSmartObjectManagementServer.Setup(i => i.GetSmartObjects(It.IsAny<SearchProperty>(), It.IsAny<SearchOperator>(), It.IsAny<string>())).Returns(mockSmartObjectExplorer);
            mockConnectionProvider.Setup(x => x.GetServer<ISmartObjectManagementServer>()).Returns(mockSmartObjectManagementServer.Object);
            mockSmartObjectClientServer.Setup(x => x.GetSmartObject(It.IsAny<string>())).Returns(smartObject);
            mockSmartObjectClientServer.Setup(x => x.ExecuteScalar(It.IsAny<SmartObject>())).Returns(smartObject);

            ConnectionHelper.UpdateConnectionProvider(mockConnectionProvider.Object);

            Action<SmartObject> action = (SmartObject i) => { };

            // Act
            var actual = SmartObjectClientServerExtensions.Serialize(mockSmartObjectClientServer.Object, serviceObjectName, settings.Object, action);

            // Assert
            Assert.IsNull(actual);
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
            Action<SmartObject> action = (SmartObject i) => { };

            // Act
            SmartObjectClientServerExtensions.Serialize(server, serviceObjectName, settings.Object, action);
        }
    }
}