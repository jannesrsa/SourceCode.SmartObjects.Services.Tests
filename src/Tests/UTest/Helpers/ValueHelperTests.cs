using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Client;

namespace SourceCode.SmartObjects.Services.Tests.Helpers.Tests
{
    [TestClass()]
    public class ValueHelperTests
    {
        [TestMethod()]
        public void GetDefaultValue_ReferenceType()
        {
            // Arrange
            var type = typeof(string);

            // Action
            var actual = ValueHelper.GetDefaultValue(type);

            // Assert
            Assert.AreEqual(default(string), actual);
        }

        [TestMethod()]
        public void GetDefaultValue_ValueType()
        {
            // Arrange
            var type = typeof(int);

            // Action
            var actual = ValueHelper.GetDefaultValue(type);

            // Assert
            Assert.AreEqual(default(int), actual);
        }

        [TestMethod()]
        public void GetDotNetType_AllTypes()
        {
            // Arrange
            var propertyTypes = Enum.GetValues(typeof(PropertyType));

            foreach (PropertyType propertyType in propertyTypes)
            {
                // Action
                var type = ValueHelper.GetDotNetType(propertyType);

                // Assert
                Assert.IsNotNull(type);
            }
        }
    }
}