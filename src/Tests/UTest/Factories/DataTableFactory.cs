using System;
using System.Data;

namespace SourceCode.SmartObjects.Services.Tests.UTest.Factories
{
    internal static class DataTableFactory
    {
        public const string DefaultValue = "Testing 123";

        public static DataTable GetDataTableWithOneColumn()
        {
            var dataTable1 = new DataTable();
            var dataColumn = new DataColumn("Column1");

            dataTable1.Columns.Add(dataColumn);
            return dataTable1;
        }

        public static DataTable GetDataTableWithOneColumnAndOneRow(string value = DefaultValue)
        {
            var dataTable1 = GetDataTableWithOneColumn();
            var dataRow = dataTable1.NewRow();
            dataTable1.Rows.Add(dataRow);

            dataRow[dataTable1.Columns[0]] = value;
            return dataTable1;
        }
    }
}