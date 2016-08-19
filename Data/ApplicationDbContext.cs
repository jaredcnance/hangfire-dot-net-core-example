using concurrent_queues.Models;
using Microsoft.EntityFrameworkCore;

namespace concurrent_queues.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    { }

    public virtual DbSet<TodoItem> TodoItems { get; set; }
  }
}