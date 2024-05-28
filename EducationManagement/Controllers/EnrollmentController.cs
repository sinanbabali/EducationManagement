using Business.Abstract;
using DTO.DTOs.EnrollmentDTOs;
using DTO.DTOs.LessonDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using Utilities.Http;

namespace EducationManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private IEnrollmentService _enrollmentService;
        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [CustomAuthorize("Student")]
        [HttpPost("EnrollLesson")]
        [Description("Tanımlı Ders Dönemleri Id bilgileri : <b>1</b> veya <b>2</b>")]
        public IActionResult EnrollToLesson([FromBody] EnrollmentModel model)
        {
            return _enrollmentService.AddEnrollment(model);
        }

        [CustomAuthorize("Teacher", "Manager")]
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(_enrollmentService.GetEnrollments());
        }

    }
}
