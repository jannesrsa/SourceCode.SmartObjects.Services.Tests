using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.Helpers;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenGetCurrentUserCalledOnConnectionHelper
    {
        [TestMethod()]
        public void GetCurrentUserTest()
        {
            // Action
            var currentuser = ConnectionHelper.GetCurrentUser();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(currentuser));
        }
    }
}