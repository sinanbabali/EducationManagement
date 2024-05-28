using Business.Abstract;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EduContext _dbContext;

        public UnitOfWork(EduContext dbContext)
        {
            _dbContext = dbContext;
            Users = new UserService(dbContext);
            Lessons = new LessonService(dbContext);
            Enrollments = new EnrollmentService(dbContext);
        }

        public IUserService Users { get; private set; }
        public ILessonService Lessons { get; private set; }
        public IEnrollmentService Enrollments { get; private set; }

        public void SaveChanges()
            => _dbContext.SaveChanges();


        public async Task SaveChangesAsync()
            => await _dbContext.SaveChangesAsync();


        public void Rollback()
            => _dbContext.Dispose();


        public async Task RollbackAsync()
            => await _dbContext.DisposeAsync();
    }
}
