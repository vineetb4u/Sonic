using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Services
{
    public interface IStudentService : IBaseService<StudentDTO>
    {
        List<StudentDTO> FindStudents(StudentFindCriteria criteria);
        StudentDTO Find(int studentID);
    }
}
