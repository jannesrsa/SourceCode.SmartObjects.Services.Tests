using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SourceCode.SmartObjects.Client;
using SourceCode.SmartObjects.Client.Filters;
using SourceCode.SmartObjects.Services.Tests.UTest.Factories;

namespace SourceCode.SmartObjects.Services.Tests.Extensions.Tests
{
    [TestClass()]
    public class SmartObjectExtensionsTests
    {
        [TestMethod()]
        public void AddFirstPropertyOrderBy_WithValidSmartObject()
        {
            //Arrange
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);
            smartObject.MethodToExecute = smartObject.ListMethods[0].Name;

            // Act
            SmartObjectExtensions.AddFirstPropertyOrderBy(smartObject);
        }

        [TestMethod()]
        public void AddPropertyOrderBy_WithValidSmartObject()
        {
            //Arrange
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);
            smartObject.MethodToExecute = smartObject.ListMethods[0].Name;

            // Act
            SmartObjectExtensions.AddPropertyOrderBy(smartObject, smartObject.ListMethods[0].ReturnProperties[0].Name);
        }

        [TestMethod()]
        public void GetPropertyValue_EmptyValue()
        {
            //Arrange
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);
            smartObject.MethodToExecute = smartObject.ListMethods[0].Name;

            // Act
            var actual = SmartObjectExtensions.GetPropertyValue<int>(smartObject, smartObject.ListMethods[0].ReturnProperties[0].Name);

            // Assert
            Assert.AreEqual(0, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void GetPropertyValue_InvalidProperty()
        {
            //Arrange
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);
            smartObject.MethodToExecute = smartObject.ListMethods[0].Name;

            // Act
            SmartObjectExtensions.GetPropertyValue<int>(smartObject, Guid.NewGuid().ToString());
        }

        [TestMethod()]
        public void GetPropertyValue_ValidCast()
        {
            //Arrange
            var expected = Guid.NewGuid().ToString();
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);
            smartObject.MethodToExecute = smartObject.ListMethods[0].Name;
            smartObject.Properties[smartObject.ListMethods[0].ReturnProperties[0].Name].Value = expected;

            // Act
            var actual = SmartObjectExtensions.GetPropertyValue<string>(smartObject, smartObject.ListMethods[0].ReturnProperties[0].Name);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void GetReturnProperties_NullMethod()
        {
            //Arrange
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);

            // Act
            var actual = SmartObjectExtensions.GetReturnProperties(smartObject);
        }

        [TestMethod()]
        public void GetReturnPropertyValue_WithValidSmartObject()
        {
            //Arrange
            var expected = Guid.NewGuid().ToString();
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);
            smartObject.MethodToExecute = smartObject.ListMethods[0].Name;
            smartObject.Properties[smartObject.ListMethods[0].ReturnProperties[0].Name].Value = expected;

            // Act
            var actual = SmartObjectExtensions.GetReturnPropertyValue<string>(smartObject, smartObject.ListMethods[0].ReturnProperties[0].Name);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SetFilter_WithValidSmartObject()
        {
            //Arrange
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);
            smartObject.MethodToExecute = smartObject.ListMethods[0].Name;

            var filter = Mock.Of<Equals>();

            // Act
            SmartObjectExtensions.SetFilter(smartObject, filter);
        }

        [TestMethod()]
        [ExpectedException(typeof(NullReferenceException))]
        public void SetInputPropertyValue_InvalidInputProperty()
        {
            //Arrange
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);
            var propertyName = Guid.NewGuid().ToString();
            var value = Guid.NewGuid().ToString();
            smartObject.MethodToExecute = smartObject.ListMethods[0].Name;

            // Act
            SmartObjectExtensions.SetInputPropertyValue(smartObject, propertyName, value);
        }

        [TestMethod()]
        public void SetNewMethod_WithValidSmartObject()
        {
            //Arrange
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.Users_and_Groups);
            smartObject.MethodToExecute = smartObject.ListMethods[0].Name;

            // Act
            SmartObjectExtensions.SetNewMethod(smartObject, smartObject.ListMethods[0].Name, false, true);
        }

        [TestMethod()]
        public void ToList_WithValidSmartObjectList()
        {
            //Arrange
            var smartObject = SmartObjectFactory.GetSmartObject(SmartObjectOption.ProcessInfo);
            smartObject.MethodToExecute = smartObject.ListMethods[0].Name;

            var smartObjectList = new SmartObjectList();
            smartObjectList.SmartObjectsList.Add(smartObject);

            // Act
            var actual = SmartObjectExtensions.ToList(smartObjectList, smartObject.MethodToExecute);

            // Assert
            Assert.AreEqual(1, actual.Count());
        }
    }
}