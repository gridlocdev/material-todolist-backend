namespace Todo.Data
{
    using System.Linq;
    using Todo.Models;
    using Todo.Data;
    using System.Collections.Generic;
    using System;

    public class TodoSqliteRepo : ITodoSqliteRepo
    {
        // In here, I define database connection methods.
        private TodoContext _context;

        public TodoSqliteRepo(TodoContext context)
        {
            _context = context;
        }


        // Create
        public void CreateTodo(TodoItem todo)
        {
            try
            {
                _context.Todos.Add(todo);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Read
        public List<TodoItem> ReadTodoList()
        {
            try
            {
                List<TodoItem> result = _context.Todos.ToList();
                // _context.SaveChanges();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        // Update
        public void UpdateTodo(TodoItem todo)
        {
            try
            {
                TodoItem todoToUpdate = _context.Todos.Find(todo.id);
                Console.WriteLine("Input id: " + todo.id);
                Console.WriteLine("Input text: " + todo.text);
                Console.WriteLine("Input completed: " + todo.completed);
                
                todoToUpdate.text = todo.text;
                todoToUpdate.completed = todo.completed;

                Console.WriteLine("Output id: " + todoToUpdate.id);
                Console.WriteLine("Output text: " + todoToUpdate.text);
                Console.WriteLine("Output completed: " + todoToUpdate.completed);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        // Delete
        public void DeleteTodo(TodoItem todo)
        {
            try
            {
                TodoItem todoToDelete = _context.Todos.Find(todo.id);
                _context.Todos.Remove(todoToDelete);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}