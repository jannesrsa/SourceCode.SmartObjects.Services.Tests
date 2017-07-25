using System;
using System.Data;
using SourceCode.SmartObjects.Services.Tests.Helpers;

namespace SourceCode.SmartObjects.Services.Tests.Extensions
{
    public static class DataRowExtensions
    {
        public static void AssertAreEqual<T>(this DataRow dataRow, string columnName, T expectedValue, string rowIdentifier = null)
        {
            dataRow.ThrowIfNull("dataRow");
            columnName.ThrowIfNullOrWhiteSpace("columnName");

            AssertHelper.AreEqual<T>(expectedValue, dataRow.Field<T>(columnName)
                            , string.Format("{0}.{1} has an incorrect value.{2}", dataRow.Table.TableName, columnName, rowIdentifier ?? string.Empty));
        }

        public static T AssertHasValue<T>(this DataRow dataRow, string columnName, string rowIdentifier = null)
        {
            dataRow.ThrowIfNull("dataRow");
            columnName.ThrowIfNullOrWhiteSpace("columnName");

            var cellObjectValue = (dataRow.Field<object>(columnName));

            AssertHelper.IsFalse(cellObjectValue == null || string.IsNullOrEmpty(cellObjectValue.ToString()),
                string.Format("[{0}].[{1}] must have a '{2}' value. Row Identifier: [{3}]",
                    dataRow.Table.TableName, columnName, typeof(T).ToString(), rowIdentifier));
            try
            {
                var cellValue = dataRow.Field<T>(columnName);
                return cellValue;
            }
            catch (System.Exception ex)
            {
                throw new InvalidOperationException(string.Format("[{0}].[{1}] convert to '{2}' error. Value: '{3}' Row Identifier: [{4}] Error: '{5}'",
                     dataRow.Table.TableName, columnName, typeof(T).ToString(), cellObjectValue, rowIdentifier, ex.Message));
            }
        }

        public static string AssertHasValue(this DataRow dataRow, string columnName, string rowIdentifier = null)
        {
            return dataRow.AssertHasValue<string>(columnName, rowIdentifier);
        }

        public static string GetFirstValue(this DataRow dataRow, params string[] columnNames)
        {
            if (dataRow == null ||
                dataRow.Table == null)
            {
                return null;
            }

            columnNames.ThrowIfNull("columnNames");

            foreach (var columnName in columnNames)
            {
                if (dataRow.Table.Columns == null ||
                    !dataRow.Table.Columns.Contains(columnName) ||
                    string.IsNullOrWhiteSpace(dataRow[columnName].ToString()))
                {
                    continue;
                }

                return dataRow[columnName].ToString();
            }

            return null;
        }
    }
}