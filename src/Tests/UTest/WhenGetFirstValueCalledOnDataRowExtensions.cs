using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.Extensions;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenGetFirstValueCalledOnDataRowExtensions
    {
        [TestMethod()]
        public void WithMultipleColumnsReturnValue()
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
        public void WithNonExistingColumnName()
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
        public void WithNullColumnNames()
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
        public void WithNullDataRow()
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
        public void WithValidValue()
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