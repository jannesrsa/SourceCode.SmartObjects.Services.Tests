using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class ConnectionHelperTests
    {
        [TestMethod()]
        public void GetCurrentUser_ReturnValid()
        {
            // Action
            var currentuser = ConnectionHelper.GetCurrentUser();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(currentuser));
        }

        [TestMethod()]
        public void SmartObjectConnectionStringBuilder_Get()
        {
            // Action
            var actual = ConnectionHelper.SmartObjectConnectionStringBuilder.ConnectionString;

            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void WorkflowConnectionStringBuilder_Get()
        {
            // Action
            var actual = ConnectionHelper.WorkflowConnectionStringBuilder.ConnectionString;

            // Assert
            Assert.IsNotNull(actual);
        }

        [TestMethod()]
        public void Invoke_WithServer()
        {
            // Arrange
            var mockWrapperFactory = new MockWrapperFactory();

            // Action
            var function = new Func<bool>(() => { return true; });
            var smartObjectClientServer = new SmartObjectClientServer();
            ConnectionHelper.Invoke(function, ref smartObjectClientServer);
        }

        [TestMethod()]
        public void Invoke_ServerNull()
        {
            // Arrange
            var mockWrapperFactory = new MockWrapperFactory();

            // Action
            var function = new Func<bool>(() => { return true; });
            SmartObjectClientServer smartObjectClientServer = null;
            ConnectionHelper.Invoke(function, ref smartObjectClientServer);
        }
    }
}