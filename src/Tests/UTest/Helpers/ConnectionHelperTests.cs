using System;
using System.Diagnostics;
using System.IO;
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
                AnotherMethod();
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException)
                {
                    Debug.WriteLine($"Fusionlog: {(ex as FileNotFoundException).FusionLog}");
                    Debug.WriteLine($"FileName: {(ex as FileNotFoundException).FileName}");
                }
                
                Debug.WriteLine($"ToString: {ex.ToString()}");

                throw new Exception(ex.ToString());
            }
        }

        private static void AnotherMethod()
        {
            // Action
            var currentuser = ConnectionHelper.GetCurrentUser();

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(currentuser));
        }
    }
}