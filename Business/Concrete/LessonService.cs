using Business.Abstract;
using DTO.DTOs.LessonDTOs;
using Entity;
using Entity.Concrete;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class LessonService : GenericRepository<Lesson>, ILessonService
    {
        private IUnitOfWork _unitOfWork;
        public LessonService(IUnitOfWork unitOfWork, EduContext context) : base(context)
        {
            _unitOfWork = unitOfWork;
        }

        public LessonService(EduContext context) : base(context)
        {//stage'den gönder 23

        }

        public IActionResult AddLesson(LessonModel lesson)
        {
            if (lesson.Credits > 6)
            {
                return new BadRequestObjectResult("Ders kredisi 6'dan büyük olamaz.");
            }

            var newLesson = new Lesson
            {
                Name = lesson.Name,
                Description = lesson.Description,
                IsMandatory = lesson.IsMandatory,
                Credits = lesson.Credits
            };

            _context.Lessons.Add(newLesson);
            _context.SaveChanges();

            return new OkObjectResult("Ders başarıyla oluşturuldu.");
        }

        public IActionResult EditLesson(int id, LessonModel lesson)
        {
            var EditLesson = _unitOfWork.Lessons.GetById(id);

            if (EditLesson == null)
            {
                AddLesson(lesson);
            }
            else
            {
                if (lesson.Credits > 6)
                {
                    return new BadRequestObjectResult("Ders kredisi 6'dan büyük olamaz.");
                }

                EditLesson.Name = lesson.Name;
                EditLesson.Description = lesson.Description;
                EditLesson.IsMandatory = lesson.IsMandatory;
                EditLesson.Credits = lesson.Credits;

                _unitOfWork.Lessons.Update(EditLesson);
                _context.SaveChanges();
            }

            return new OkObjectResult("Ders başarıyla güncellendi.");
        }

        public List<Lesson> GetLessons()
        {
            return _unitOfWork.Lessons.GetAll().ToList();
        }
    }
}
