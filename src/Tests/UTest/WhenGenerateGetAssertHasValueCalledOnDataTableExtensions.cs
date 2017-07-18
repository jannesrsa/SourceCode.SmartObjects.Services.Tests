using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.Extensions;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenGenerateGetAssertHasValueCalledOnDataTableExtensions
    {
        [TestMethod()]
        public void WithAssertHasValue()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = "Column1";
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();
            dataTable.Rows.Add(dataRow);

            var expectedValue = Guid.NewGuid().ToString();
            dataRow[columnName] = expectedValue;

            // Act
            var actual = DataTableExtensions.GenerateGetAssertHasValue(dataTable);

            // Assert
            Assert.AreEqual($"row.AssertHasValue<String>(\"{columnName}\");\r\n", actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithDataTableNull()
        {
            //Arrange
            DataTable dataTable = null;

            // Act
            DataTableExtensions.GenerateGetAssertHasValue(dataTable);
        }

        [TestMethod()]
        public void WithEmptyValue()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = "Column1";
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();
            dataTable.Rows.Add(dataRow);

            // Act
            var actual = DataTableExtensions.GenerateGetAssertHasValue(dataTable);

            // Assert
            Assert.AreEqual($"// Empty values\r\n//row.AssertHasValue<String>(\"{columnName}\");\r\n", actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithRowNull()
        {
            //Arrange
            var dataTable = new DataTable();

            // Act
            DataTableExtensions.GenerateGetAssertHasValue(dataTable);
        }
    }
}