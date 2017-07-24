using System;
using System.ComponentModel;
using System.Security.Principal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.UTest.Mocks;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class SecurityHelperTests
    {
        private MockWrapperFactory _mockWrapperFactory;

        [TestMethod()]
        public void InvokeAsUser_CurrentUser()
        {
            // Arrange
            Action action = () => { Assert.AreEqual(string.Empty, string.Empty); };

            // Action
            SecurityHelper.InvokeAsUser(action, WindowsIdentity.GetCurrent().Name, string.Empty);
        }

        [TestMethod()]
        public void InvokeAsUser_GetCurrentUser()
        {
            // Arrange
            Action action = () => { Assert.AreEqual(string.Empty, string.Empty); };

            // Action
            SecurityHelper.InvokeAsUser(action, ConnectionHelper.GetCurrentUser(), string.Empty);

            // Assert
            Console.WriteLine($"User: {ConnectionHelper.GetCurrentUser()}");
        }

        [TestMethod()]
        [ExpectedException(typeof(Win32Exception))]
        public void InvokeAsUser_InvalidUser()
        {
            // Arrange
            Wrappers.WrapperFactory.Instance = new Wrappers.WrapperFactory();
            Action action = () => { Assert.AreEqual(string.Empty, string.Empty); };

            // Action
            SecurityHelper.InvokeAsUser(action, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }

        [TestInitialize()]
        public void TestInit()
        {
            _mockWrapperFactory = new MockWrapperFactory();
        }
    }
}