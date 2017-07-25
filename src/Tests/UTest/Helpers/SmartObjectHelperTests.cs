using System;
using System.Data;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Authoring;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Management;
using SourceCode.SmartObjects.Services.Tests.Managers;
using SourceCode.SmartObjects.Services.Tests.UTest.Factories;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;
using SourceCode.SmartObjects.Services.Tests.UTest.Properties;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class SmartObjectHelperTests
    {
        private MockWrapperFactory _mockWrapperFactory;

        [TestMethod()]
        public void CompareDataTables()
        {
            // Arrange
            DataTable dataTable1 = DataTableFactory.GetDataTableWitheOneColumnAndOneRow();

            // Action
            SmartObjectHelper.CompareDataTables(dataTable1, dataTable1.DefaultView.ToTable());
        }

        [TestMethod()]
        public void ContainsSmartObject()
        {
            // Arrange
            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);
            SmartObjectManagementServer smartObjectManagementServer = null;
            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            _mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            // Action
            var actual = SmartObjectHelper.ContainsSmartObject(smartObjectManagementServer, smartObjectInfo.Name);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void DeleteSmartObject_SmartObjectExists()
        {
            // Arrange
            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);
            SmartObjectManagementServer smartObjectManagementServer = null;
            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            _mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            // Action
            SmartObjectHelper.DeleteSmartObject(smartObjectManagementServer, smartObjectInfo.Name);
        }

        [TestMethod()]
        public void DeleteSmartObject_SmartObjectNotExists()
        {
            // Arrange
            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);
            SmartObjectManagementServer smartObjectManagementServer = null;
            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            _mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            // Action
            SmartObjectHelper.DeleteSmartObject(smartObjectManagementServer, Guid.NewGuid().ToString());
        }

        [TestMethod()]
        public void ExecuteBulkScalar_DefaultValues()
        {
            // Arrange
            SmartObjectClientServer server = null;
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            // Action
            SmartObjectHelper.ExecuteBulkScalar(server, smartObject, new DataTable());
        }

        [TestMethod()]
        public void ExecuteList_DefaultValues()
        {
            // Arrange
            SmartObjectClientServer server = null;
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            // Action
            SmartObjectHelper.ExecuteList(server, smartObject);
        }

        [TestMethod()]
        public void ExecuteListDataTable_DefaultValues()
        {
            // Arrange
            SmartObjectClientServer server = null;
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            // Action
            SmartObjectHelper.ExecuteListDataTable(server, smartObject);
        }

        [TestMethod()]
        public void ExecuteListReader_DefaultValues()
        {
            // Arrange
            SmartObjectClientServer server = null;
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            // Action
            SmartObjectHelper.ExecuteListReader(server, smartObject);
        }

        [TestMethod()]
        public void ExecuteScalar_DefaultValues()
        {
            // Arrange
            SmartObjectClientServer server = null;
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            // Act
            SmartObjectHelper.ExecuteScalar(server, smartObject);
        }

        [TestMethod()]
        public void ExecuteSQLQueryDataTable_DefaultValues()
        {
            // Arrange
            SmartObjectClientServer server = null;

            // Action
            SmartObjectHelper.ExecuteSQLQueryDataTable(server, "SELECT * FROM REGION");
        }

        [TestMethod()]
        public void GetServiceConfigInfo_DefaultValues()
        {
            // Arrange
            _mockWrapperFactory.ServiceManagementServer
                .Setup(i => i.GetServiceInstanceConfig(
                    It.IsAny<Guid>()))
                .Returns(Resources.ServiceInstanceConfig);

            // Action
            var serviceConfig = SmartObjectHelper.GetServiceConfigInfo(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(serviceConfig);
        }

        [TestMethod()]
        public void GetServiceInstance_IsNull()
        {
            // Action
            var serviceConfig = SmartObjectHelper.GetServiceInstance(Guid.NewGuid());

            // Assert
            Assert.IsNull(serviceConfig);
        }

        [TestMethod()]
        public void GetServiceInstance_WithGuid()
        {
            // Arrange
            _mockWrapperFactory.ServiceManagementServer
               .Setup(i => i.GetServiceInstanceCompact(
                   It.IsAny<Guid>()))
               .Returns(Resources.ServiceInstance_URMService);

            // Action
            var serviceConfig = SmartObjectHelper.GetServiceInstance(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(serviceConfig);
        }

        [TestMethod()]
        public void GetServiceInstance_WithServiceInstanceSettings()
        {
            // Arrange
            _mockWrapperFactory.SmartObjectManagementServer
               .Setup(i => i.GetServiceInstance(
                   It.IsAny<Guid>(),
                   It.IsAny<ServiceExplorerLevel>()))
               .Returns(Resources.ServiceInstance_URMService);

            // Action
            var serviceConfig = SmartObjectHelper.GetServiceInstance(Mock.Of<ServiceInstanceSettings>());

            // Assert
            Assert.IsNotNull(serviceConfig);
        }

        [TestMethod()]
        public void GetServiceObject_DefaultValues()
        {
            // Arrange
            _mockWrapperFactory.SmartObjectManagementServer
               .Setup(i => i.GetServiceInstanceServiceObject(
                   It.IsAny<Guid>(),
                   It.IsAny<string>()))
               .Returns(Resources.ServiceObject_ProcessOverview);

            // Action
            var serviceObject = SmartObjectHelper.GetServiceObject(Mock.Of<ServiceInstanceSettings>(), Guid.NewGuid().ToString());

            // Assert
            Assert.IsNotNull(serviceObject);
        }

        [TestMethod()]
        public void GetServiceType_DefaultValues()
        {
            // Arrange
            _mockWrapperFactory.ServiceManagementServer
               .Setup(i => i.GetServiceType(
                   It.IsAny<Guid>()))
               .Returns(Resources.ServiceType_ADService);

            // Action
            var serviceType = SmartObjectHelper.GetServiceType(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(serviceType);
        }

        [TestMethod()]
        public void GetServiceType_Null()
        {
            // Act
            _mockWrapperFactory.ServiceManagementServer
               .Setup(i => i.GetServiceType(
                   It.IsAny<Guid>()))
               .Returns(string.Empty);

            // Action
            var serviceType = SmartObjectHelper.GetServiceType(Guid.NewGuid());

            // Assert
            Assert.IsNull(serviceType);
        }

        [TestMethod()]
        public void GetServiceTypeInfo_DefaultValues()
        {
            // Act
            _mockWrapperFactory.ServiceManagementServer
               .Setup(i => i.GetServiceType(
                   It.IsAny<Guid>()))
               .Returns(Resources.ServiceType_ADService);

            // Action
            var serviceType = SmartObjectHelper.GetServiceTypeInfo(Guid.NewGuid());

            // Assert
            Assert.IsNotNull(serviceType);
        }

        [TestMethod()]
        public void GetSmartObject_DefaultValues()
        {
            // Arrange
            SmartObjectClientServer server = null;
            _mockWrapperFactory.MockWithProcessInstanceSmartObject(out SmartObject expected, out ServiceInstanceSettings settings);

            // Action
            var actual = SmartObjectHelper.GetSmartObject(server, Guid.NewGuid().ToString(), Mock.Of<ServiceInstanceSettings>());

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetSmartObjectDefinition_DefaultValues()
        {
            // Arrange
            _mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjectDefinition(
                    It.IsAny<string>()))
                .Returns(Resources.SmartObjectDefinition_ProcessInfo);

            // Action
            var actual = SmartObjectHelper.GetSmartObjectDefinition(Guid.NewGuid().ToString());

            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void GetSmartObjects_DefaultValues()
        {
            // Arrange
            var expected = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);

            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(expected);

            _mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<Guid[]>()))
                .Returns(mockSmartObjectExplorer);

            // Action
            var actual = SmartObjectHelper.GetSmartObjects(new Guid[] { });

            // Assert
            Assert.AreEqual(expected, actual.FirstOrDefault());
        }

        [TestMethod()]
        public void PublishSmartObjects_DefaultValues()
        {
            // Arrange
            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);
            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            _mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            var smartObjectDefinitionsPublish = new SmartObjectDefinitionsPublish();
            smartObjectDefinitionsPublish.SmartObjects.Add(new SmartObjectDefinition());

            // Action
            SmartObjectHelper.PublishSmartObjects(smartObjectDefinitionsPublish);
        }

        [TestMethod()]
        public void PublishSmartObjectsFromResources_DefaultValues()
        {
            // Arrange
            var smartObjectInfo = SmartObjectInfo.Create(Resources.SmartObjectDefinition_ProcessInfo);
            var mockSmartObjectExplorer = Mock.Of<SmartObjectExplorer>();
            mockSmartObjectExplorer.SmartObjects.Add(smartObjectInfo);

            _mockWrapperFactory.SmartObjectManagementServer
                .Setup(i => i.GetSmartObjects(
                    It.IsAny<string>()))
                .Returns(mockSmartObjectExplorer);

            var smartObjectDefinitionsPublish = new SmartObjectDefinitionsPublish();
            smartObjectDefinitionsPublish.SmartObjects.Add(new SmartObjectDefinition());

            // Action
            SmartObjectHelper.PublishSmartObjectsFromResources(this.GetType().Assembly, null);
        }

        [TestInitialize()]
        public void TestInit()
        {
            _mockWrapperFactory = new MockWrapperFactory();
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void VerifyAllReturnPropertiesHasValues_WithMethod_NoReturn()
        {
            // Arrange
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            // Action
            SmartObjectHelper.VerifyAllReturnPropertiesHasValues(smartObject.Methods[0]);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void VerifyAllReturnPropertiesHasValues_WithNull()
        {
            // Arrange
            SmartMethodBase method = null;
            // Action
            SmartObjectHelper.VerifyAllReturnPropertiesHasValues(method);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void VerifyAllReturnPropertiesHasValues_WithSmartObject_NoReturn()
        {
            // Arrange
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);
            smartObject.MethodToExecute = smartObject.Methods[0].Name;

            // Action
            SmartObjectHelper.VerifyAllReturnPropertiesHasValues(smartObject);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void VerifyPaging_DefaultValues()
        {
            // Arrange
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);
            smartObject.MethodToExecute = smartObject.Methods[0].Name;

            DataTable dataTable1 = DataTableFactory.GetDataTableWitheOneColumnAndOneRow();

            SmartObjectClientServer server = null;

            _mockWrapperFactory.SmartObjectClientServer
               .Setup(x => x.ExecuteListDataTable(
                   It.IsAny<SmartObject>(),
                   It.IsAny<ExecuteListOptions>()))
               .Returns(dataTable1);

            _mockWrapperFactory.SmartObjectClientServer
              .Setup(x => x.ExecuteListReader(
                  It.IsAny<SmartObject>(),
                  It.IsAny<ExecuteListReaderOptions>()))
              .Returns(default(SmartObjectReader));

            // Action
            SmartObjectHelper.VerifyPaging(server, smartObject, 0);
        }
    }
}