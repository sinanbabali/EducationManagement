using Business.Interface;
using DTO.DTOs.EnrollmentDTOs;
using DTO.DTOs.LessonDTOs;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEnrollmentService : IGenericService<Enrollment>
    {
        IActionResult AddEnrollment(EnrollmentModel model);
        List<Enrollment> GetEnrollments();
    }
}
