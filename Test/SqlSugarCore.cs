using SqlSugar;

namespace Test
{
    public class SqlSugarCore
    {
        /// <summary>
        /// 创建实体类
        /// </summary>
        /// <param name="conn">数据库连接字符串</param>
        /// <param name="url">绝对路径</param>
        public static void CreateModel(string conn, string url)
        {
            // 创建数据库对象
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = conn,
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true
            },
            db =>
            {
                //5.1.3.24统一了语法和SqlSugarScope一样，老版本AOP可以写外面

                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    Console.WriteLine(sql);//输出sql,查看执行sql 性能无影响

                    //获取原生SQL推荐 5.1.4.63  性能OK
                    //UtilMethods.GetNativeSql(sql,pars)

                    //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                    //UtilMethods.GetSqlString(DbType.SqlServer,sql,pars)
                };

                //注意多租户 有几个设置几个
                //db.GetConnection(i).Aop
            });

            db.DbFirst.IsCreateDefaultValue().CreateClassFile(url);
        }
    }
}