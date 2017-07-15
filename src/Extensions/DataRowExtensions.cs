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
                            , $"{dataRow.Table.TableName}.{columnName} has an incorrect value.{rowIdentifier ?? string.Empty}");
        }

        public static T AssertHasValue<T>(this DataRow dataRow, string columnName, string rowIdentifier = null)
        {
            dataRow.ThrowIfNull("dataRow");
            columnName.ThrowIfNullOrWhiteSpace("columnName");

            var cellObjectValue = (dataRow.Field<object>(columnName));

            AssertHelper.IsFalse(cellObjectValue == null || string.IsNullOrEmpty(cellObjectValue.ToString()),
                $"[{dataRow.Table.TableName}].[{columnName}] must have a '{typeof(T)}' value. Row Identifier: [{rowIdentifier}]");
            try
            {
                var cellValue = dataRow.Field<T>(columnName);
                return cellValue;
            }
            catch (System.Exception ex)
            {
                AssertHelper.Fail($"[{dataRow.Table.TableName}].[{columnName}] convert to '{typeof(T)}' error. Value: '{cellObjectValue}' Row Identifier: [{rowIdentifier}] Error: '{ex.Message}'");

                throw;
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