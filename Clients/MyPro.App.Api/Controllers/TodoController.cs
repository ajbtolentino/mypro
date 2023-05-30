using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyPro.Todo.Infrastructure.Contracts.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyPro.App.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodoController : ControllerBase
    {
        private ITodoService todoService;

        public TodoController(ITodoService todoService)
        {
            this.todoService = todoService;
        }

        [HttpGet(nameof(TodoController.Add))]
        public IActionResult Add(string text)
        {
            this.todoService.Add(text);

            return new JsonResult(todoService.Count());
        }

        [HttpGet(nameof(TodoController.Count))]
        public IActionResult Count()
        {
            return new JsonResult(todoService.Count());
        }
    }
}

