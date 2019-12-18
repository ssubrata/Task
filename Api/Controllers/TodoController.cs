using System.Linq;
using Api.Entites;
using Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers {

    [Authorize]
    [Route ("api/[controller]")]
    public class TodoController : Controller {
        private DataDbContext context;
        public TodoController (DataDbContext context) => this.context = context;

        /*
         This Method For Get All Todo By Login User;
         Param Id AS User Id;
        */

        [HttpGet ("{id}")]
        public IActionResult Get (int id) => Ok (context.Todo.Where (f => f.UserId == id).ToList ());

        /*
          This Method For Put/Update Todo;
          Param Todo
        */
        [HttpPut]
        public IActionResult Put ([FromBody] VmTodo todo) {
            if (todo == null) return BadRequest ();
            var findTodo = context.Todo.Find (todo.Id);
            if (findTodo == null) return NotFound ();
            /*better to use mapper here for small project it will be time waset*/
            findTodo.Title = todo.Title;
            findTodo.Description = todo.Description;
            findTodo.Date = todo.Date;
            findTodo.From = todo.From;
            findTodo.To = todo.To;
            findTodo.Location = todo.Location;
            findTodo.NotifyBy = todo.NotifyBy;
            findTodo.NotifyTime = todo.NotifyTime;
            findTodo.Teal = todo.Teal;
            context.SaveChanges ();
            return Ok ();

        }

        /*
          This Method For Post/NewTodo Todo By Id
        */
        [HttpPost ()]
        public IActionResult Post (int id, [FromBody] VmTodo todo) {
            if (!ModelState.IsValid) return BadRequest (ModelState.Select (f => f.Value.Errors.ToList ()));
            /*better to use mapper here for small project it will be time waset*/
            var newTodo = new Todo {
                Title = todo.Title,
                Description = todo.Description,
                Date = todo.Date,
                From = todo.From,
                To = todo.To,
                Location = todo.Location,
                NotifyBy = todo.NotifyBy,
                NotifyTime = todo.NotifyTime,
                Teal = todo.Teal,
                UserId = todo.UserId
            };
            context.Todo.Add (newTodo);
            context.SaveChanges ();
            return Ok ();
        }

        /*
           This Method For Delete Todo By Id
           Param {Id}
        */
        [HttpDelete ("{id}")]
        public IActionResult Delete (int id) {
            var findTodo = context.Todo.Find (id);
            if (findTodo == null) return NotFound ();
            context.Todo.Remove (findTodo);
            context.SaveChanges ();
            return Ok ();

        }
    }
}