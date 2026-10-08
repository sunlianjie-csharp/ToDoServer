using SqlSugar;

namespace ToDoServer.Helper
{
    public class SqlHelper
    {
        // IConfiguration con 配置文件服务
        //IServiceCollection  服务集合 获取服务
        public static void AddSqlSugar( IServiceCollection services, IConfiguration con)
        {
           var db=  new SqlSugarScope(new ConnectionConfig()
            {
                DbType = DbType.SqlServer,
                IsAutoCloseConnection = true,
                ConnectionString = con.GetConnectionString("connectString")
            });

            services.AddSingleton <ISqlSugarClient>(db); //把db添加服务集合对象里面
        }
    }
}
