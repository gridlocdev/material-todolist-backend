namespace Todo.Controllers
{
    using System;
    using Todo.Models;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using Todo.Data;

    [ApiController]
    public class TodoController : ControllerBase
    {
        private ITodoSqliteRepo _repository;

        public TodoController(ITodoSqliteRepo repository)
        {
            _repository = repository;
        }

        // Create an object that has 


        [HttpGet("api/todos")]
        public ActionResult Get()
        {
            Console.WriteLine("HttpGet hit.");
            List<TodoItem> result = _repository.ReadTodoList();

            Console.WriteLine("Returning: " + result);

            return Ok(result);
        }

        [HttpPost("api/todos")]
        public ActionResult Post(TodoItem todo)
        {
            Console.WriteLine("HttpPost hit.");
            Console.WriteLine("Adding Todo: " + todo);

            _repository.CreateTodo(todo);

            // TodoList.Add(result);

            return Ok();
        }

        [HttpPut("api/todos/{id}")]
        public ActionResult Put(TodoItem todo)
        {
            Console.WriteLine("HttpPut hit.");
            Console.WriteLine("Updating Todo: " + todo);
            Console.WriteLine(todo.completed.ToString());
            try
            {
                _repository.UpdateTodo(todo);
            }
            catch
            {
                return NotFound();
            }

            return Ok();

        }

        [HttpDelete("api/todos/{id}")]
        public ActionResult Delete(TodoItem todo)
        {
            Console.WriteLine("HttpDelete hit.");
            Console.WriteLine("Deleting Todo: " + todo);
            try
            {
                _repository.DeleteTodo(todo);
            }
            catch
            {
                return NotFound();
            }

            return Ok();

        }

    }
}