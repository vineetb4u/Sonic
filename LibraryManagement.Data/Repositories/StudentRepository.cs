using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Data.Contracts;
using LibraryManagement.Data.Entities;
using LibraryManagement.Models;

namespace LibraryManagement.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private ConcurrentBag<Student> _inMemoryDb;
        private static StudentRepository _instance;
        private static readonly object lockObj = new object();

        public StudentRepository()
        {
            _inMemoryDb = new ConcurrentBag<Student>();
        }

        public static StudentRepository Database
        {
            get
            {
                lock (lockObj)
                {
                    if (_instance == null)
                    {
                        _instance = new StudentRepository();
                    }
                }

                return _instance;
            }
        }

        private IQueryable<Student> Query()
        {
            return _inMemoryDb.AsQueryable();
        }

        public void Add(Student student)
        {
            _inMemoryDb.Add(student);
        }

        public void Update(Student student)
        {
            var rec = Query().Where(s => s.StudentID == student.StudentID).FirstOrDefault();

            rec.StudentID = student.StudentID;
            rec.FirstName = student.FirstName;
            rec.LastName = student.LastName;
        }

        public void Delete(Student student)
        {
            _inMemoryDb.TryTake(out student);
        }

        public IEnumerable<Student> FindByCriteria(StudentFindCriteria criteria)
        {
            var query = Query();

            if (criteria.StudentID.HasValue)
                query = query.Where(student => student.StudentID == criteria.StudentID);

            return query.ToList();
        }

        
        public Student Find(int studentID)
        {
            var query = Query();
            return query.Where(student => student.StudentID == studentID).FirstOrDefault();
        }
    }
}
