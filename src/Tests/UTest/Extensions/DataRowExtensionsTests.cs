using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SourceCode.SmartObjects.Services.Tests.Extensions.Tests
{
    [TestClass()]
    public class DataRowExtensionsTests
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AssertAreEqual_WithColumnNameNull()
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
        public void AssertAreEqual_WithColumnNameStringEmpty()
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
        public void AssertAreEqual_WithDataRowNull()
        {
            //Arrange
            DataRow dataRow = null;
            string columnName = "Column1";
            string expectedValue = Guid.NewGuid().ToString();

            // Act
            DataRowExtensions.AssertAreEqual(dataRow, columnName, expectedValue);
        }

        [TestMethod()]
        public void AssertAreEqual_WithEqualValues()
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
        public void AssertAreEqual_WithNonEqualValues()
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

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AssertHasValue_WithColumnNameNull()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = null;
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();

            // Act
            DataRowExtensions.AssertHasValue<string>(dataRow, columnName);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AssertHasValue_WithColumnNameStringEmpty()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = string.Empty;
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();
            // Act
            DataRowExtensions.AssertHasValue<string>(dataRow, columnName);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void AssertHasValue_WithDataRowNull()
        {
            //Arrange
            DataRow dataRow = null;
            string columnName = "Column1";
            string rowIdentifier = Guid.NewGuid().ToString();

            // Act
            DataRowExtensions.AssertHasValue<string>(dataRow, columnName, rowIdentifier);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void AssertHasValue_WithNullValue()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = "Column1";
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();

            dataRow[dataColumn] = null;

            // Act
            DataRowExtensions.AssertHasValue<string>(dataRow, columnName);
        }

        [TestMethod()]
        public void AssertHasValue_WithValue()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = "Column1";
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();

            dataRow[dataColumn] = Guid.NewGuid().ToString();

            // Act
            DataRowExtensions.AssertHasValue(dataRow, columnName);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void AssertHasValue_WitNonMatchingTypeValue()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = "Column1";
            var dataColumn = new DataColumn(columnName, typeof(Guid));
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();

            dataRow[dataColumn] = Guid.NewGuid();

            // Act
            DataRowExtensions.AssertHasValue<string>(dataRow, columnName);
        }

        [TestMethod()]
        public void GetFirstValue_WithMultipleColumnsReturnValue()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = "Column1";
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();
            var expected = Guid.NewGuid().ToString();
            dataRow[dataColumn] = expected;

            // Act
            var actual = DataRowExtensions.GetFirstValue(dataRow, Guid.NewGuid().ToString(), columnName);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void GetFirstValue_WithNonExistingColumnName()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = "Column1";
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();
            var expected = Guid.NewGuid().ToString();
            dataRow[dataColumn] = expected;

            // Act
            var actual = DataRowExtensions.GetFirstValue(dataRow, Guid.NewGuid().ToString());

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFirstValue_WithNullColumnNames()
        {
            //Arrange
            var dataTable = new DataTable();

            string[] columnNames = null;
            var dataColumn = new DataColumn(Guid.NewGuid().ToString());
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();

            // Act
            var actual = DataRowExtensions.GetFirstValue(dataRow, columnNames);
        }

        [TestMethod()]
        public void GetFirstValue_WithNullDataRow()
        {
            //Arrange
            DataRow dataRow = null;
            string columnName = "Column1";

            // Act
            var actual = DataRowExtensions.GetFirstValue(dataRow, columnName);

            // Assert
            Assert.IsNull(actual);
        }

        [TestMethod()]
        public void GetFirstValue_WithValidValue()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = "Column1";
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();
            var expected = Guid.NewGuid().ToString();
            dataRow[dataColumn] = expected;

            // Act
            var actual = DataRowExtensions.GetFirstValue(dataRow, columnName);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}