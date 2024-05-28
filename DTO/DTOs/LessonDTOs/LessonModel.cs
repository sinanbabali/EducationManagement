using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.DTOs.LessonDTOs
{
    public class LessonModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsMandatory { get; set; }
        public int Credits { get; set; }
    }
}
