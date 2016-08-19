using concurrent_queues.Models;
using concurrent_queues.Services;
using Microsoft.AspNetCore.Mvc;
using Hangfire;
using Microsoft.Extensions.Configuration;

namespace concurrent_queues.Controllers
{
  [Route("api/[controller]")]
  public class TodoItemsController : Controller
  {
    private IConfigurationRoot _configuration;

    public TodoItemsController(IConfigurationRoot configuration)
    {
      _configuration = configuration;
    }

    // GET api/values
    [HttpPost]
    public IActionResult Post([FromBody] TodoItem todoItem)
    {
      BackgroundJob.Enqueue<TodoItemService>(service => service.CreateTodoItem(todoItem));
      return new StatusCodeResult(202);
    }

  }
}
