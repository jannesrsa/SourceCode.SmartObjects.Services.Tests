using System;
using System.Data;

namespace SourceCode.SmartObjects.Services.Tests.UTest.Factories
{
    internal static class DataTableFactory
    {
        public static DataTable GetDataTableWitheOneColumnAndOneRow()
        {
            var dataTable1 = new DataTable();
            var dataColumn = new DataColumn("Column1");

            dataTable1.Columns.Add(dataColumn);
            var dataRow = dataTable1.NewRow();
            dataTable1.Rows.Add(dataRow);

            dataRow[dataColumn] = Guid.NewGuid().ToString();
            return dataTable1;
        }
    }
}