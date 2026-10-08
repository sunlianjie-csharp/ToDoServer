using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using ToDoServer.Model;
using ToDoServer.Model.DTO;

namespace ToDoServer.Controllers
{

    [ApiController]
    [Route("/Api/[Controller]")]
    public class ToDoController:ControllerBase
    {
        private ISqlSugarClient _db;
        public ToDoController(ISqlSugarClient db)
        {
            _db = db;
        }


        // 查询接口
        [HttpGet("GetTodo")]
        public async Task<ActionResult> GetTodo([FromQuery] TodoSearch condition)
        {

            try
            {
                var exp = Expressionable.Create<TodoView>();// 创建查询条件对象
                exp.AndIF(!string.IsNullOrEmpty(condition.Title), it => it.Title.Contains(condition.Title));
                exp.AndIF(condition.isComplted.HasValue, it => it.isCompelte==condition.isComplted);

                int skip = ((condition.Page ?? 1) - 1) * (condition.PageSize ?? 10); //跳过多少条数
                int take = condition.PageSize ?? 10; //取出10条

                List<TodoView> list = await _db.Queryable<TodoView>().Where(exp.ToExpression()).OrderByDescending(it => it.TodoId)
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();

                int total =  _db.Queryable<TodoView>().Where(exp.ToExpression()).ToList().Count;
                Console.WriteLine(total + "ssssssssssssssssss");

                return Ok(new
                {
                    code = 200,
                    Msg = "查询成功",
                    Data = list, 
                    TotalPage =  total%take==0?total/take: total/take+1

                });

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost("AddTodo")]
        //ActionResult 服务响应的类
        public async  Task<ActionResult> AddTodo([FromBody] Todo model)
        {
            try
            {
                model.CreateTime  = DateTime.Now;
                model.CreateId = 1;
                long id =   await _db.Insertable(model).ExecuteReturnBigIdentityAsync();
                return Ok(new { Code = 200, Msg = "添加成功" });   

            }
            catch (Exception ex)
            {

                return BadRequest(new { Code = 400, Msg =ex.Message });
            }
        }

        //编辑接口
        [HttpPost("EditTodo")]
        public async Task<ActionResult> EditTodo([FromBody] Todo model)
        {
            try
            {

                 await _db.Updateable<Todo>(model).ExecuteCommandAsync();


                return Ok(new { Code = 200, Msg = "更新数据成功" });

            }
            catch (Exception ex)
            {

                return BadRequest(new { Code = 400, Msg = ex.Message });
            }
        }

        //删除的接口
        [HttpDelete("DeleteTodo/{id}")]
        public async Task<ActionResult> DeleteTodo(int id)
        {
            try
            {

                await _db.Deleteable<Todo>().In(id).ExecuteCommandAsync();
                return Ok(new { Code = 200, Msg = "删除成功" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Code = 400, Msg = ex.Message });
            }
        }
    }
    //查询条件类
    public  class TodoSearch
    {
         public string? Title {  get; set; }
         public bool? isComplted {  get; set; }

        public int? Page {  get; set; }

        public int? PageSize { get; set; }
    }


}
