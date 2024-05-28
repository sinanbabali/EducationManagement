using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CourseName { get; set; }

        [MaxLength(255)]
        public string Description { get; set; }

        [Required]
        public bool IsMandatory { get; set; }

        [Required]
        [Range(0, 6)]
        public int Credits { get; set; }


        public int TeacherId { get; set; }
        public User Teacher { get; set; }

        public ICollection<Selection> Selections { get; set; }
    }
}
