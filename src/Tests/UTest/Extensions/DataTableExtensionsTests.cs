using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SourceCode.SmartObjects.Services.Tests.Extensions.Tests
{
    [TestClass()]
    public class DataTableExtensionsTests
    {
        [TestMethod()]
        public void GenerateGetAssertHasValue_WithAssertHasValue()
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
        public void GenerateGetAssertHasValue_WithDataTableNull()
        {
            //Arrange
            DataTable dataTable = null;

            // Act
            DataTableExtensions.GenerateGetAssertHasValue(dataTable);
        }

        [TestMethod()]
        public void GenerateGetAssertHasValue_WithEmptyValue()
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
        public void GenerateGetAssertHasValue_WithRowNull()
        {
            //Arrange
            var dataTable = new DataTable();

            // Act
            DataTableExtensions.GenerateGetAssertHasValue(dataTable);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void GetCondition_WithDataTableNull()
        {
            //Arrange
            DataTable dataTable = null;
            int pageNumber = 0;
            int pageSize = 0;

            // Act
            DataTableExtensions.GetCondition(dataTable, pageNumber, pageSize);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetCondition_WithPageNumberIntMinValue()
        {
            //Arrange
            var dataTable = new DataTable();
            int pageNumber = int.MinValue;
            int pageSize = 0;

            // Act
            DataTableExtensions.GetCondition(dataTable, pageNumber, pageSize);
        }

        [TestMethod()]
        public void GetCondition_WithReturnFalseValue()
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

            int pageNumber = 2;
            int pageSize = 2;

            // Act
            var actual = DataTableExtensions.GetCondition(dataTable, pageNumber, pageSize);

            // Assert
            Assert.IsFalse(actual);
        }

        [TestMethod()]
        public void GetCondition_WithReturnTrueValue()
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

            int pageNumber = 0;
            int pageSize = 0;

            // Act
            var actual = DataTableExtensions.GetCondition(dataTable, pageNumber, pageSize);

            // Assert
            Assert.IsTrue(actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPagedResult_WithDataTableNull()
        {
            //Arrange
            DataTable dataTable = null;
            int pageNumber = 0;
            int pageSize = 0;

            // Act
            DataTableExtensions.GetPagedResult(dataTable, pageNumber, pageSize);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void GetPagedResult_WithPageNumberIntMinValue()
        {
            //Arrange
            var dataTable = new DataTable();
            int pageNumber = int.MinValue;
            int pageSize = 0;

            // Act
            DataTableExtensions.GetPagedResult(dataTable, pageNumber, pageSize);
        }

        [TestMethod()]
        public void GetPagedResult_WithReturnOneRow()
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

            int pageNumber = 1;
            int pageSize = 1;

            // Act
            var actual = DataTableExtensions.GetPagedResult(dataTable, pageNumber, pageSize);

            // Assert
            Assert.IsTrue(actual.Rows.Count == 1);
        }

        [TestMethod()]
        public void GetPagedResult_WithReturnZeroRows()
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

            int pageNumber = 2;
            int pageSize = 2;

            // Act
            var actual = DataTableExtensions.GetPagedResult(dataTable, pageNumber, pageSize);

            // Assert
            Assert.IsTrue(actual.Rows.Count == 0);
        }
    }
}