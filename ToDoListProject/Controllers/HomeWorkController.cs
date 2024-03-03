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
    [Authorize]
    public class HomeWorkController : ControllerBase
    {
        private readonly IHomeWorkService service;

        public HomeWorkController(IHomeWorkService service)
        {
            this.service = service;
        }
        [HttpGet("ReadAllWork")]
        public ActionResult<List<HomeWork>> ReadAll()
        {
            return Ok(service.ReadAll());
        }
        [HttpGet("ReadOneWork")]
        public ActionResult<HomeWork> ReadOne(int id)
        {
            return Ok(service.ReadOne(id));
        }
        [HttpPost("CreateWork")]
        public ActionResult<int> Create(HomeWorkDTO homeWorkDTO)
        {
            return Ok(service.Create(homeWorkDTO));
        }
        [HttpPut("UpdateWork")]
        public ActionResult<int> Update(int id, HomeWorkDTO homeWorkDTO)
        {
            return Ok(service.Update(id, homeWorkDTO));
        }
        [HttpDelete("DeleteWork")]
        public ActionResult<int> Delete(int id)
        {
            return Ok(service.Delete(id));
        }
    }
}
