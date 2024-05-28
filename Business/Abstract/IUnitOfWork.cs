using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUnitOfWork
    {
        IUserService Users { get; }
        ILessonService Lessons { get; }
        IEnrollmentService Enrollments { get; }
        void SaveChanges();
        void Rollback();
        Task SaveChangesAsync();
        Task RollbackAsync();
    }
}
