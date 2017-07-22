using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class ValueHelperTests
    {
        [TestMethod()]
        public void GetDefaultValue_WithDefaultValues()
        {
            // Arrange
            var type = typeof(string);
            
            // Action
            var actual = ValueHelper.GetDefaultValue(type);

            // Assert
            Assert.AreEqual(default(string), actual);
        }
    }
}