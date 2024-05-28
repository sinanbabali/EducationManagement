using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs.EnrollmentDTOs
{
    public class EnrollmentModel
    {
        public int StudentId { get; set; }
        public int SemesterId { get; set; }
        public List<int> LessonIds { get; set; }
    }
}
