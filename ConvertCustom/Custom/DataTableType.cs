using NPOI.SS.UserModel;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace ConvertCustom.Custom
{
    /// <summary>
    /// 转换数据表
    /// </summary>
    public static class DataTableType
    {
        /// <summary>
        /// 合并表(不去重)
        /// </summary>
        public static DataTable Merge(DataTable dt1, DataTable dt2)
        {
            // 创建一个新的DataTable用于存储合并后的数据
            DataTable mergedTable = new DataTable();

            // 将dt1的列添加到mergedTable
            foreach (DataColumn column in dt1.Columns)
            {
                mergedTable.Columns.Add(column.ColumnName, column.DataType);
            }

            // 使用流式处理将dt1的数据添加到mergedTable
            foreach (DataRow row in dt1.Rows)
            {
                mergedTable.Rows.Add(row.ItemArray);
            }

            // 使用流式处理将dt2的数据添加到mergedTable
            foreach (DataRow row in dt2.Rows)
            {
                mergedTable.Rows.Add(row.ItemArray);
            }

            return mergedTable;
        }
        
        /// <summary>
        /// 合并表去重
        /// </summary>
        /// <param name="dt1">主表</param>
        /// <param name="dt2">副表</param>
        /// <param name="Id">去重字段</param>
        public static DataTable MergeAndRemoveDuplicates(DataTable dt1, DataTable dt2,string Id) 
        { 
            DataTable dataTable = new DataTable(); 
            foreach (DataColumn column in dt1.Columns) { 
                dataTable.Columns.Add(column.ColumnName, column.DataType); 
                }
            foreach (DataRow row in dt1.Rows)
            {
                dataTable.Rows.Add(row.ItemArray);
            }

            foreach (DataRow row2 in dt2.Rows)
            {
                bool duplicate = false;
                foreach (DataRow existingRow in dataTable.Rows)
                {
                    if (row2[Id].Equals(existingRow[Id]))
                    {
                        duplicate = true;
                        break;
                    }
                }
                if (!duplicate)
                {
                    dataTable.Rows.Add(row2.ItemArray);
                }
            }

            return dataTable;
        }
        /// <summary>
        /// 泛型K不能为空
        /// </summary>
        public static DataTable Parse<K, V>(List<Dictionary<K, V>> listDic)
        {
            DataTable dt = new DataTable();
            if (listDic.Count > 0)
            {
                foreach (K i in listDic[0].Keys)
                {
                    dt.Columns.Add(i.ToString());
                }
                foreach (Dictionary<K, V> dic in listDic)
                {
                    DataRow dr = dt.NewRow();
                    foreach (DataColumn dc in dt.Columns)
                    {
                        dr[dc.ColumnName] = dic[(K)(object)dc.ColumnName];
                    }
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            return dt;
        }

        public static DataRow Parse<K, V>(Dictionary<K, V> dic)
        {
            return Parse(new List<Dictionary<K, V>> { dic }).Rows[0];
        }

        public static DataTable Parse<T>(List<T> list) where T : class
        {
            DataTable dt = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] properties = list[0].GetType().GetProperties();
                foreach (PropertyInfo item in properties)
                {
                    dt.Columns.Add(item.Name);
                }
                {
                    foreach (T entity in list)
                    {
                        DataRow dr = dt.NewRow();
                        properties = entity.GetType().GetProperties();
                        foreach (PropertyInfo c in properties)
                        {
                            dr[c.Name] = c.GetValue(entity);
                        }
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
            }
            return dt;
        }

        public static DataRow Parse<T>(T entity) where T : class
        {
            return Parse(new List<T> { entity }).Rows[0];
        }

        /// <summary>
        /// Excel
        /// NPOI解析
        /// </summary>
        public static List<DataTable> ParseList(IWorkbook workbook)
        {
            List<DataTable> tables = new List<DataTable>();
            for (int s = 0; workbook.NumberOfSheets > s && workbook.GetSheetAt(s).GetRow(0) != null; s++)
            {
                DataTable dt = new DataTable();
                ISheet sheet = workbook.GetSheetAt(s);
                dt.TableName = sheet.SheetName;
                int cell = sheet.GetRow(0).Cells.Count;
                for (int i = 0; sheet.GetRow(i) != null; i++)
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < cell; j++)
                    {
                        ICell value = sheet.GetRow(i).GetCell(j);
                        if (i == 0)
                        {
                            dt.Columns.Add(value.ToString());
                        }
                        else if (value == null)
                        {
                            dr[dt.Columns[j].ToString()] = "";
                        }
                        else
                        {
                            dr[dt.Columns[j].ToString()] = value.ToString();
                        }
                    }
                    if (i > 0)
                    {
                        dt.Rows.Add(dr);
                    }
                }
                tables.Add(dt);
            }
            return tables;
        }
    }
}