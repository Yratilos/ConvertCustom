using ConvertCustom.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace ConvertCustom.IServices
{
    public interface IAdoNet
    {
        /// <summary>
        /// 获取数据库中的值
        /// </summary>
        /// <param name="sql">... @value1=field1 and @value2=field2</param>
        /// <param name="param">
        /// SqlParameter[] parameters = new SqlParameter[]{
        ///     new SqlParameter("@field1",value1),
        ///     new SqlParameter("@field2",value2),
        /// }
        /// </param>
        /// <returns>第一行第一列</returns>
        string ExecuteScalar(string sql, params SqlParameter[] param);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sql">... @value1=field1 and @value2=field2</param>
        /// <param name="param">
        /// SqlParameter[] parameters = new SqlParameter[]{
        ///     new SqlParameter("@field1",value1),
        ///     new SqlParameter("@field2",value2),
        /// }
        /// </param>
        /// <returns></returns>
        DataSet Execute(string sql, params SqlParameter[] param);
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="hashtable">
        /// Key:... @value1=field1 and @value2=field2
        /// Value:
        /// SqlParameter[] parameters = new SqlParameter[]{
        ///     new SqlParameter("@field1",value1),
        ///     new SqlParameter("@field2",value2),
        /// }
        /// </param>
        /// <returns>是否成功</returns>
        bool ExecuteTrans(Hashtable hashtable);
        /// <summary>
        /// 执行事务
        /// </summary>
        /// <param name="hash">数据库操作语句</param>
        /// <returns>是否成功</returns>
        bool ExecuteTrans(HashSet<string> hash);
    }
}
