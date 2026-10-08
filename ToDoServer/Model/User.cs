using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace ToDoServer.Model
{
    [SugarTable("User")]
    public class User
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int UserId {  get; set; }

        [Required]
        public string Account { get; set; }=string.Empty;

        [Required]
        public string Pwd { get; set; } = string.Empty;
    }
}
