using System;
using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SourceCode.SmartObjects.Services.Tests.Extensions;

namespace SourceCode.SmartObjects.Services.Tests.UTest
{
    [TestClass()]
    public class WhenAssertHasValueCalledOnDataRowExtensions
    {
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
            DataRowExtensions.AssertHasValue(dataRow, columnName);
        }
    }
}