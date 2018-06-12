using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Module3.Models;
using Module3.Contexts;

namespace Module3.Controllers
{
    [Route("api/[controller]")]
    public class TasksController : Controller
    {
        AppDbContext _context;
        public TasksController(AppDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public IEnumerable<TaskModel> Get()
        {
            return _context.Tasks.ToList();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            TaskModel task = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
                return NotFound();
            return new ObjectResult(task);
        }

        [HttpPost]
        public IActionResult Post([FromBody]TaskModel task)
        {
            if (task == null)
            {
                return BadRequest();
            }

            _context.Tasks.Add(task);
            _context.SaveChanges();
            return Ok(task);
        }


        [HttpPut("{id}")]
        public IActionResult Put([FromBody]TaskModel task)
        {
            if (task == null)
            {
                return BadRequest();
            }
            if (!_context.Tasks.Any(x => x.Id == task.Id))
            {
                return NotFound();
            }

            _context.Update(task);
            _context.SaveChanges();
            return Ok(task);
        }
        [HttpPost("{id}/check")]
        public IActionResult Check(int id)
        {
            TaskModel task = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            task.IsDone = !task.IsDone;
            _context.SaveChanges();
            return Ok(task);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            TaskModel task = _context.Tasks.FirstOrDefault(x => x.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok(task);
        }
    }
}