using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class ConnectionHelperTests
    {
        [TestMethod()]
        public void GetCurrentUserTest()
        {
            try
            {
                // Action
                var currentuser = ConnectionHelper.GetCurrentUser();

                // Assert
                Assert.IsFalse(string.IsNullOrWhiteSpace(currentuser));
            }
            catch (System.IO.FileNotFoundException ex)
            {
                Console.WriteLine($"Fusionlog: {ex.FusionLog}");
                Console.WriteLine($"FileName: {ex.FileName}");
                Console.WriteLine($"ToString: {ex.ToString()}");

                throw;
            }
        }
    }
}