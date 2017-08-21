using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Models;
using LibraryManagement.Data.Entities;

namespace LibraryManagement.Services
{
    public static class StudentMapper
    {
        public static StudentDTO Map(Student source, StudentDTO target = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (target == null)
            {
                target = new StudentDTO();
            }

            target.StudentID = source.StudentID;
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;

            return target;
        }


        public static Student Map(StudentDTO source, Student target = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (target == null)
            {
                target = new Student();
            }

            target.StudentID = source.StudentID;
            target.FirstName = source.FirstName;
            target.LastName = source.LastName;

            return target;
        }

    }
}
