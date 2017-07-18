using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.Extensions;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenAssertHasValueOfTypeCalledOnDataRowExtensions
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

            // Act
            DataRowExtensions.AssertHasValue<string>(dataRow, columnName);
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
            // Act
            DataRowExtensions.AssertHasValue<string>(dataRow, columnName);
        }

        [TestMethod()]
        [ExpectedException(typeof(ArgumentException))]
        public void WithDataRowNull()
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
        public void WithNullValue()
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
        public void WithValue()
        {
            //Arrange
            var dataTable = new DataTable();

            string columnName = "Column1";
            var dataColumn = new DataColumn(columnName);
            dataTable.Columns.Add(dataColumn);

            var dataRow = dataTable.NewRow();

            dataRow[dataColumn] = Guid.NewGuid().ToString();

            // Act
            DataRowExtensions.AssertHasValue<string>(dataRow, columnName);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void WitNonMatchingTypeValue()
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
    }
}