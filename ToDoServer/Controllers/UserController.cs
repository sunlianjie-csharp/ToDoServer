using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using ToDoServer.Model;

namespace ToDoServer.Controllers
{
    [ApiController]
    [Route("/Api/[Controller]")]// /Api/User/接口的名称
    public class UserController:ControllerBase
    {
        private ISqlSugarClient _db;
        public UserController(ISqlSugarClient db)
        {
            _db = db;
        }
        // 登录接口
        ///Api/User/login
        [HttpPost("login")]
        public  async Task<ActionResult> Login([FromBody] User model)
        {
            try
            {
              List<User> list =  await _db.Queryable<User>().Where(it => it.Account == model.Account && it.Pwd == model.Pwd).ToListAsync();
                if (list.Count>=1)
                {
                    //匿名类 字段可以随便起
                    return Ok(new  {Code=200,Msg="登录成功"});
                 //  return  Ok( new Status(){Code=200,Msg="成功"});
                }
                else
                {
                    return BadRequest(new { Code = 400, Msg = "登录失败" });
                    
                }
            }
            catch (Exception)
            {

                return BadRequest(new  { Code = 403, Msg = "无法预知的错误" });
            }

        }
        //注册接口
        [HttpPost("Add")]
        public async Task<ActionResult> AddUser([FromBody] User model)
        {
            Console.WriteLine("dddddddddssssssssssssssssssssssssss");
            try
            {
                List<User> list = await _db.Queryable<User>().Where(it => it.Account == model.Account && it.Pwd == model.Pwd).ToListAsync();
                if (list.Count>=1)
                {
                    return Ok(new { Code = 1001, Msg = "当前用户已经存在" });

                }
                else
                {
                   int row =  _db.Insertable<User>(model).ExecuteCommand();
                    if (row>0)
                    {
                        return Ok(new { Code = 200, Msg = "注册成功" });
                    }
                    else
                    {
                        return BadRequest(new { Code = 1002, Msg = "注册失败" });
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("sssssssssssssssssss");
                return BadRequest(new { Code = 1003, Msg ="异常" });

            }
        }

    }
    //public class Status
    //{
    //    public int Code { get; set; }
    //    public string Msg { get; set; }
    //}
}
