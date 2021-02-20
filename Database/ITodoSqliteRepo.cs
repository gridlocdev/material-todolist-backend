namespace Todo.Data
{
    using System.Collections.Generic;
    using Todo.Models;
    public interface ITodoSqliteRepo
    {
        // An interface is a contract that allows both developers and the compiler to know what should be inside of a class.

        void CreateTodo(TodoItem todo);

        List<TodoItem> ReadTodoList();

        void UpdateTodo(TodoItem todo);

        void DeleteTodo(TodoItem todo);

    }

}
