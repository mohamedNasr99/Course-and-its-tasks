using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDoListProject.DTOs;
using ToDoListProject.Models;
using ToDoListProject.Services;

namespace ToDoListProject.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService service;

        public CourseController(ICourseService service)
        {
            this.service = service;
        }
        [HttpGet("ReadAll")]
        public ActionResult<List<Course>> ReadAll()
        {
            return Ok(service.ReadAll());
        }
        [HttpGet("ReadOne")]
        public ActionResult<Course> ReadOne(int id)
        {
            return Ok(service.ReadOne(id));
        }
        [HttpPost("Create")]
        public ActionResult<int> Create(string name)
        {
            return Ok(service.Create(name));
        }
        [HttpPut("Update")]
        public ActionResult<int> Update(int id, CourseDTO courseDTO)
        {
            return Ok(service.Update(id, courseDTO));
        }
        [HttpDelete("Delete")]
        public ActionResult<int> Delete(int id)
        {
            return Ok(service.Delete(id));
        }
    }
}
