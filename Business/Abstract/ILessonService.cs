using Business.Interface;
using DTO.DTOs.LessonDTOs;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Http;

namespace Business.Abstract
{
    public interface ILessonService : IGenericService<Lesson>
    {
        IActionResult AddLesson(LessonModel lesson);
        IActionResult EditLesson(int id, LessonModel lesson);
        List<Lesson> GetLessons();
    }
}
