using Business.Abstract;
using DTO.DTOs.EnrollmentDTOs;
using DTO.DTOs.LessonDTOs;
using Entity;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EnrollmentService : GenericRepository<Enrollment>, IEnrollmentService
    {
        private IUnitOfWork _unitOfWork;
        public EnrollmentService(IUnitOfWork unitOfWork, EduContext context) : base(context)
        {
            _unitOfWork = unitOfWork;
        }
        public EnrollmentService(EduContext context) : base(context)
        {

        }

        public IActionResult AddEnrollment(EnrollmentModel lesson)
        {
            var ProcessDate = DateTime.Now;
            var semester = _context.Semesters.Where(x => x.Id == lesson.SemesterId).FirstOrDefault();
            var selectedCourses = _unitOfWork.Lessons.Find(l => lesson.LessonIds.Contains(l.Id)).ToList();

            if (selectedCourses.Count != lesson.LessonIds.Count)
            {
                return new BadRequestObjectResult("Geçersiz ders kimliği var.");
            }
            else if (semester == null)
            {
                return new BadRequestObjectResult("Kayıt dönemi eksik.");
            }


            if (semester.StartDate > ProcessDate || semester.EndDate < ProcessDate)
            {
                return new BadRequestObjectResult("Kayıt dönemi dışında.");
            }

            var totalCredits = selectedCourses.Sum(c => c.Credits);

            var mandatoryCourses = selectedCourses.Where(c => c.IsMandatory).ToList();
            var mandatoryCredits = mandatoryCourses.Sum(c => c.Credits);


            if (totalCredits < 30 || totalCredits > 35 || mandatoryCredits < 25)
            {
                return new BadRequestObjectResult("Geçersiz kredi miktarı veya zorunlu ders seçimi.");
            }


            foreach (var item in lesson.LessonIds)
            {
                var enrollment = new Enrollment
                {
                    StudentId = lesson.StudentId,
                    CourseId = item,
                    RecordDate = ProcessDate
                };

                _unitOfWork.Enrollments.Add(enrollment);
            }

            _unitOfWork.SaveChanges();

            return new OkObjectResult("Ders seçimi yapıldı.");
        }

        public List<Enrollment> GetEnrollments()
        {
            return _unitOfWork.Enrollments.GetAll().ToList();
        }
    }
}
