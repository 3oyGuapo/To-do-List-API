using Microsoft.EntityFrameworkCore;
using To_do_List;
using Todo.Shared;

namespace To_do_List_API.Data
{
    public class TodoDbContext : DbContext
    {
        //Pass the database context data to the base constructor
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {

        }

        //DbSet refers to a table in the database, it will look for Tasks table when using this property or create a TaskItem table if not exist
        public DbSet<TaskItem> Tasks { get; set; }
    }
}
