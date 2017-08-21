using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Data.Entities;
using LibraryManagement.Models;

namespace LibraryManagement.Data.Contracts
{
    public interface IStudentRepository : IRepository<Student>
    {
        IEnumerable<Student> FindByCriteria(StudentFindCriteria criteria);
        Student Find(int studentID);
    }
}
