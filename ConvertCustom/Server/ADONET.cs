using System.Data;
using System.Data.SqlClient;

namespace ConvertCustom.Server
{
    /// <summary>
    /// 数据库访问类
    /// </summary>
    public class ADONET
    {
        private static ADONET instance = null;
        private static readonly object padlock = new object();
        private readonly string conn = "server={0};uid={1};pwd={2};database={3}";

        private ADONET(string conn)
        {
            this.conn = conn;
        }

        private ADONET(string server, string uid, string pwd, string database)
        {
            conn = string.Format(conn, server, uid, pwd, database);
        }

        /// <summary>
        /// 设置数据库连接
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        public static ADONET GetInstance(string conn)
        {
            if (instance is null)
            {
                lock (padlock)
                {
                    if (instance is null)
                    {
                        instance = new ADONET(conn);
                    }
                }
            }
            return instance;
        }

        /// <summary>
        /// 设置数据库连接
        /// </summary>
        /// <param name="server">服务器名称</param>
        /// <param name="uid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="database">数据库</param>
        /// <returns></returns>
        public static ADONET GetInstance(string server, string uid, string pwd, string database)
        {
            if (instance is null)
            {
                lock (padlock)
                {
                    if (instance is null)
                    {
                        instance = new ADONET(server, uid, pwd, database);
                    }
                }
            }
            return instance;
        }

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
        public string ExecuteScalar(string sql, params SqlParameter[] param)
        {
            using (SqlConnection conn = new SqlConnection(this.conn))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(param);
                    return cmd.ExecuteScalar().ToString();
                }
            }
        }

        /// <summary>
        /// 获取数据值
        /// </summary>
        /// <param name="sql">... @value1=field1 and @value2=field2</param>
        /// <param name="param">
        /// SqlParameter[] parameters = new SqlParameter[]{
        ///     new SqlParameter("@field1",value1),
        ///     new SqlParameter("@field2",value2),
        /// }
        /// </param>
        /// <returns></returns>
        public DataSet Execute(string sql, params SqlParameter[] param)
        {
            using (SqlConnection conn = new SqlConnection(this.conn))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(param);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        var ds = new DataSet();
                        da.Fill(ds);
                        return ds;
                    }
                }
            }
        }
    }
}