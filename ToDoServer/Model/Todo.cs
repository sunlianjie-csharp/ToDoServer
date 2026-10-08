using SqlSugar;
using System.ComponentModel.DataAnnotations;
namespace ToDoServer.Model

{
    [SugarTable("Todo")]
    public class Todo
    {
        [SugarColumn(IsPrimaryKey =true,IsIdentity =true)]
        public int TodoId {  get; set; }

        [Required]
        public string Title {  get; set; } = string.Empty;

        [Required]
        public string Detail { get; set; } = string.Empty;


        [Required]
        public bool isCompelte { get; set; }

        [Required]
        public int CreateId { get; set; }

        [Required]
        public DateTime CreateTime { get; set; } = DateTime.Now;
    }
}
