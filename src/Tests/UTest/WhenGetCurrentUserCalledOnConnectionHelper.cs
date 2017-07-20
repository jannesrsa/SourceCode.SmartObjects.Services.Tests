using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.Helpers;
using SourceCode.SmartObjects.Services.Tests.Wrappers;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenGetCurrentUserCalledOnConnectionHelper
    {
        [TestMethod()]
        public void GetCurrentUserTest()
        {
            // Arrange
            ConnectionHelper.UpdateConnectionProvider(new DefaultConnectionHelperProvider());

            // Action
            var currentuser = ConnectionHelper.GetCurrentUser();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(currentuser));
        }
    }
}