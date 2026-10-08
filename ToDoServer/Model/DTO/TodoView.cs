using SqlSugar;
using System.ComponentModel.DataAnnotations;

namespace ToDoServer.Model.DTO
{
   
    public class TodoView
    {
        //展示的列
        public int TodoId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Detail { get; set; } = string.Empty;
        public bool isCompelte { get; set; }
        public int CreateId { get; set; }
        public string CreateName { get; set; }=string.Empty;
        public DateTime CreateTime { get; set; } = DateTime.Now;

    }
}
