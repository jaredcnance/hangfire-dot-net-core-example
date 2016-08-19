using System;
using System.Threading;
using concurrent_queues.Data;
using concurrent_queues.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace concurrent_queues.Services
{
  public class TodoItemService : IDisposable
  {
    private readonly ApplicationDbContext _context;

    public TodoItemService(IConfigurationRoot configuration)
    {
      var dbContextOptionsBuilder = new DbContextOptionsBuilder();
      dbContextOptionsBuilder.UseSqlServer(configuration["Data:DefaultConnection"]);
      _context = new ApplicationDbContext(dbContextOptionsBuilder.Options);
    }

    public void CreateTodoItem(TodoItem todoItem)
    {
      Console.WriteLine("Run started");
      _context.TodoItems.Add(todoItem);
      _context.SaveChanges();
      Thread.Sleep(10000);
      Console.WriteLine("Run complete");
    }

    public void Dispose()
    {
      _context.Dispose();
    }
  }
}