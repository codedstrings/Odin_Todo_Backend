using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Odin_Todo_Backend.Data;
using Odin_Todo_Backend.Models;
using System.Text.Json.Nodes;

namespace Odin_Todo_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly TodoDbContext _context;
        public ProjectController(TodoDbContext context)
        {
            _context = context;
        }

        // GET: api/projects/{userId}
        [HttpGet("{userId}")]
        public IActionResult GetProjectsByUserId(int userId)
        {
            var user = _context.Users.
                Include(u => u.Projects)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound($"User with ID {userId} not found");
            }

            var projects = user.Projects.FirstOrDefault();
            if (projects == null)
            {
               return Ok("[]");
            }

            return Ok(projects.JsonData);
        }

        //post
        [HttpPost("{userId}")]
        public async Task<ActionResult> SaveUserProjects(int userId, [FromBody] object jsonData)
        {
            var user = _context.Users.
                Include(u => u.Projects)
                .FirstOrDefault(u => u.UserId == userId);

            if (user == null)
            {
                return NotFound($"User with ID {userId} not found");
            }

            string jsonString = System.Text.Json.JsonSerializer.Serialize(jsonData);

            var project = user.Projects.FirstOrDefault();
            if(project == null)
            {
                //create new project
                project = new Project
                {
                    UserId = userId,
                    JsonData = jsonString,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Projects.Add(project);
            }
            else
            {
                project.JsonData = jsonString;
                project.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return Ok($"user {user.Username}'s data updated successfully");
        }

        [HttpPut("{userId}")]
        public async Task<ActionResult> UpdateUserProjects(int userId, [FromBody] object jsonData)
        {
            return await SaveUserProjects(userId, jsonData);
        }

    }
}
