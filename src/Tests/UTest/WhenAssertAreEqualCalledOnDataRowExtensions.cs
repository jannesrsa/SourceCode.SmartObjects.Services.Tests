using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.Extensions;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenAssertAreEqualCalledOnDataRowExtensions
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithColumnNameNull()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = null;
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();

            string expectedValue = Guid.NewGuid().ToString();

            // Act
            DataRowExtensions.AssertAreEqual(dataRow, columnName, expectedValue);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithColumnNameStringEmpty()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = string.Empty;
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();

            string expectedValue = Guid.NewGuid().ToString();

            // Act
            DataRowExtensions.AssertAreEqual(dataRow, columnName, expectedValue);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithDataRowNull()
        {
            //Arrange
            DataRow dataRow = null;
            string columnName = "Column1";
            string expectedValue = Guid.NewGuid().ToString();

            // Act
            DataRowExtensions.AssertAreEqual(dataRow, columnName, expectedValue);
        }

        [TestMethod()]
        public void WithEqualValues()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = "Column1";
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();

            var expectedValue = Guid.NewGuid().ToString();

            dataRow[columnName] = expectedValue;

            // Act
            DataRowExtensions.AssertAreEqual(dataRow, columnName, expectedValue);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void WithNonEqualValues()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = "Column1";
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();

            var expectedValue = Guid.NewGuid().ToString();

            dataRow[columnName] = Guid.NewGuid().ToString();

            // Act
            DataRowExtensions.AssertAreEqual(dataRow, columnName, expectedValue);
        }
    }
}