using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Data
{
    public class TodoContext : DbContext
    {
        // Storing a custom timestamp for every row's update
        public static readonly string RowVersion = nameof(RowVersion);
        // Storing the name of the database
        public static readonly string TodoDb = nameof(TodoDb).ToLower();

        // Initializing the constructor, which inherits an options value from DBContextOptions 
        // via dependency injection

        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options) { }


        // Shows EF what model we want to create a database for 
        public DbSet<TodoItem> Todos { get; set; }

        // Overrides (explicitly lets you modify) a hidden inherited method.
        // In this override, we pass in a modelBuilder to 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>() /** For TodoItem's entity context**/
            .Property<byte[]>(RowVersion) /** Add a custom property called Row Version **/
            .IsRowVersion(); /** And specify what populates that row version property **/

            // Pass our new builder back to the base class so it is able to use those methods
            base.OnModelCreating(modelBuilder);
        }
    }
}