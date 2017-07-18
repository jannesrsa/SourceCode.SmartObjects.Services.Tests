using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.Extensions;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenGetConditionCalledOnDataTableExtensions
    {
        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithDataTableNull()
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
        public void WithPageNumberIntMinValue()
        {
            //Arrange
            var dataTable = new DataTable();
            int pageNumber = int.MinValue;
            int pageSize = 0;

            // Act
            DataTableExtensions.GetCondition(dataTable, pageNumber, pageSize);
        }

        [TestMethod()]
        public void WithReturnFalseValue()
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
        public void WithReturnTrueValue()
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
    }
}