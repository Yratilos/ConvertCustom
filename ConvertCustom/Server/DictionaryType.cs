using System;
using System.Collections.Generic;
using System.Data;

namespace ConvertCustom.Server
{
    /// <summary>
    /// 转换字典
    /// </summary>
    public static class DictionaryType
    {
        public static Dictionary<K, V> Parse<K, V>(DataRow dr)
        {
            Dictionary<K, V> dic = new Dictionary<K, V>();
            foreach (DataColumn dc in dr.Table.Columns)
            {
                object value = ((dr[dc.ColumnName] == DBNull.Value) ? "" : dr[dc.ColumnName]);
                dic.Add((K)(object)dc.ColumnName, (V)value);
            }
            return dic;
        }

        public static List<Dictionary<K, V>> ParseList<K, V>(DataTable table)
        {
            List<Dictionary<K, V>> lstDic = new List<Dictionary<K, V>>();
            foreach (DataRow dr in table.Rows)
            {
                lstDic.Add(Parse<K, V>(dr));
            }
            return lstDic;
        }
    }
}