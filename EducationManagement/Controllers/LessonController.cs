using Business.Abstract;
using DTO.DTOs.LessonDTOs;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Http;

namespace EducationManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private ILessonService _lessonService; //şimdi gönder 22
        public LessonController(ILessonService lessonService)
        {
            _lessonService = lessonService;
        }

        [CustomAuthorize("Teacher")]
        [HttpPost("New")]
        public IActionResult New([FromBody] LessonModel lesson)
        {
            return _lessonService.AddLesson(lesson);
        }

        [CustomAuthorize("Teacher")]
        [HttpPut("{id}")]
        public IActionResult Edit(int id, [FromBody] LessonModel lesson)
        {
            return _lessonService.EditLesson(id,lesson);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_lessonService.GetLessons());
        }
    }
}
