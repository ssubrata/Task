using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class VmTodo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Location { get; set; }
        public string NotifyTime { get; set; }
        public string NotifyBy { get; set; }
        public string Teal { get; set; }
        public int UserId { get; set; }
    }
}
