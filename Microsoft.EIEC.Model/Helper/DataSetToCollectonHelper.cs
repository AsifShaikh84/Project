using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace Microsoft.EIEC.Model.Helper
{
    public class DataSetToCollectonHelper
    {
        public static DataTable ConvertTo<T>(IList<T> list)
        {
            using (DataTable table = CreateTable<T>())
            {
                Type entityType = typeof(T);
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

                foreach (T item in list)
                {
                    DataRow row = table.NewRow();

                    foreach (PropertyDescriptor prop in properties)
                    {
                        row[prop.Name] = prop.GetValue(item);
                    }

                    table.Rows.Add(row);
                }
                return table;
            }
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            using (DataTable table = new DataTable(entityType.Name))
            {
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

                foreach (PropertyDescriptor prop in properties)
                {
                    table.Columns.Add(prop.Name, prop.PropertyType);
                }
                return table;
            }
        }

        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
                return null;

            List<DataRow> rows = table.Rows.Cast<DataRow>().ToList();

            return ConvertTo<T>(rows);
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);

            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);

                    try
                    {
                        object value = row[column.ColumnName];

                        if (value != DBNull.Value)
                            prop.SetValue(obj, value, null);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }

            return obj;
        }

        public static DataTable TransfromRowsToColumns(DataTable source)
        {
            using (DataTable dest = new DataTable("Pivoted" + source.TableName))
            {
                dest.Columns.Add(" ");

                foreach (DataRow r in source.Rows)
                    dest.Columns.Add(r[0].ToString());

                for (int i = 0; i < source.Columns.Count - 1; i++)
                {
                    dest.Rows.Add(dest.NewRow());
                }

                for (int r = 0; r < dest.Rows.Count; r++)
                {
                    for (int c = 0; c < dest.Columns.Count; c++)
                    {
                        if (c == 0)
                            dest.Rows[r][0] = source.Columns[r + 1].ColumnName;
                        else
                            dest.Rows[r][c] = source.Rows[c - 1][r + 1];
                    }
                }
                dest.AcceptChanges();
                return dest;
            }
        }


    }
}
