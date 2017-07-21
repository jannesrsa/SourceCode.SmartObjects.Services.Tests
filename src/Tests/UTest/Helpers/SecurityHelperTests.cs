using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Security.Principal;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class SecurityHelperTests
    {
        [TestMethod()]
        [ExpectedException(typeof(Win32Exception))]
        public void InvokeAsUser_InvalidUser()
        {
            // Arrange
            Action action = () => { };

            // Action
            SecurityHelper.InvokeAsUser(action, Guid.NewGuid().ToString(), Guid.NewGuid().ToString());
        }

        [TestMethod()]
        public void InvokeAsUser_CurrentUser()
        {
            // Arrange
            Action action = () => { };

            // Action
            SecurityHelper.InvokeAsUser(action, WindowsIdentity.GetCurrent().Name, string.Empty);
        }

        [TestMethod()]
        public void InvokeAsUser_GetCurrentUser()
        {
            // Arrange
            Action action = () => { };

            // Action
            SecurityHelper.InvokeAsUser(action, ConnectionHelper.GetCurrentUser(), string.Empty);
        }
    }
}