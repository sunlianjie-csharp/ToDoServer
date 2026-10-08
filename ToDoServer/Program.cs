using SqlSugar;
using ToDoServer.Helper;
using ToDoServer.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
SqlHelper.AddSqlSugar(builder.Services,builder.Configuration); //1 注册服务

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

using (var scope = app.Services.CreateScope())
{
    // db操作数据库对象
    var db   = scope.ServiceProvider.GetRequiredService<ISqlSugarClient>();  //2 获取服务 必须保证先注册 再sqlhelper.cs 进行注册ISqlSugarClient

    // 使用代码创建数据库
    db.DbMaintenance.CreateDatabase();
    //创建表
    db.CodeFirst.InitTables(typeof(User), typeof(Todo));

}



app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json","任务列表API");
    c.RoutePrefix=string.Empty;
});

app.Urls.Add("http://0.0.0.0:5117");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
