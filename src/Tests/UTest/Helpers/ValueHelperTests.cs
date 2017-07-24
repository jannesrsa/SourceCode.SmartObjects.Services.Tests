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

        [TestMethod()]
        public void TryConvert_Convert_True()
        {
            // Action
            var actual = ValueHelper.TryConvert(typeof(int), "1", out object outValue);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void TryConvert_Decimal_False()
        {
            // Action
            var actual = ValueHelper.TryConvert(typeof(decimal), "test", out object outValue);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void TryConvert_Decimal_True()
        {
            // Action
            var actual = ValueHelper.TryConvert(typeof(decimal), "1,0", out object outValue);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void TryConvert_False()
        {
            // Action
            var actual = ValueHelper.TryConvert(typeof(int), "test", out object outValue);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void TryConvert_True()
        {
            // Action
            var actual = ValueHelper.TryConvert(typeof(int), 1, out object outValue);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        public void TryConvert_WithDbNull()
        {
            // Action
            var actual = ValueHelper.TryConvert(typeof(int), DBNull.Value, out object outValue);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void TryConvert_WithNull()
        {
            // Action
            var actual = ValueHelper.TryConvert(typeof(int), null, out object outValue);

            // Assert
            Assert.IsFalse(actual);
        }
    }
}