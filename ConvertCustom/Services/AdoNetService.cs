using ConvertCustom.IServices;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ConvertCustom.Services
{
    /// <summary>
    /// 数据库访问类
    /// </summary>
    public class AdoNetService : IAdoNet
    {
        private static AdoNetService instance = null;
        private static readonly object padlock = new object();
        private readonly string conn = "server={0};uid={1};pwd={2};database={3}";

        /// <summary>
        /// 设置数据库连接
        /// </summary>
        /// <param name="conn"></param>
        /// <returns></returns>
        private AdoNetService(string conn)
        {
            this.conn = conn;
        }

        /// <summary>
        /// 设置数据库连接
        /// </summary>
        /// <param name="server">服务器名称</param>
        /// <param name="uid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <param name="database">数据库</param>
        /// <returns></returns>
        private AdoNetService(string server, string uid, string pwd, string database)
        {
            conn = string.Format(conn, server, uid, pwd, database);
        }

        public static AdoNetService GetInstance(string conn)
        {
            if (instance is null)
            {
                lock (padlock)
                {
                    if (instance is null)
                    {
                        instance = new AdoNetService(conn);
                    }
                }
            }
            return instance;
        }

        public static AdoNetService GetInstance(string server, string uid, string pwd, string database)
        {
            if (instance is null)
            {
                lock (padlock)
                {
                    if (instance is null)
                    {
                        instance = new AdoNetService(server, uid, pwd, database);
                    }
                }
            }
            return instance;
        }

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

        public bool ExecuteTrans(Hashtable hashtable)
        {
            using (SqlConnection conn = new SqlConnection(this.conn))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        foreach (DictionaryEntry item in hashtable)
                        {
                            cmd.CommandText = item.Key.ToString();
                            if (item.Value is SqlParameter[])
                            {
                                cmd.Parameters.AddRange(item.Value as SqlParameter[]);
                            }
                            cmd.Transaction = trans;
                            var c = cmd.ExecuteNonQuery();
                            if (c <= 0)
                            {
                                trans.Rollback();
                                return false;
                            }
                        }
                        trans.Commit();
                    }
                }
            }
            return true;
        }

        public bool ExecuteTrans(HashSet<string> hash)
        {
            var hashtable = new Hashtable();
            foreach (var item in hash)
            {
                hashtable.Add(item, new SqlParameter[] { });
            }
            return ExecuteTrans(hashtable);
        }
    }
}