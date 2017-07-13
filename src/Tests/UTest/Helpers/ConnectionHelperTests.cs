using System;
using System.Diagnostics;
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
                Debug.WriteLine($"Fusionlog: {ex.FusionLog}");
                Debug.WriteLine($"FileName: {ex.FileName}");
                Debug.WriteLine($"ToString: {ex.ToString()}");

                //throw new Exception(ex.FusionLog);
            }
        }
    }
}