using LibraryManagement.Data.Contracts;
using LibraryManagement.Data.Entities;
using LibraryManagement.Models;
using LibraryManagement.Utils.Constants;
using System;
using System.Collections.Generic;
using System.Data;

namespace LibraryManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository StudentRepository)
        {
            _studentRepository = StudentRepository;
        }

        public void Add(StudentDTO recDTO)
        {
            Student student = StudentMapper.Map(recDTO);
            _studentRepository.Add(student);
        }

        public void Delete(int ID)
        {
            if (ID == 0)
            {
                throw new ArgumentException("StudentID");
            }
          
            Student student = _studentRepository.Find(ID);

            if (student == null)
            {
                throw new DataException(string.Format(ValidationConstants.SDataNotFoundWithValue, "Student", ID));
            }


            _studentRepository.Delete(student);
        }

        public StudentDTO Find(int studentID)
        {
            if (studentID == 0)
            {
                throw new ArgumentException("StudentID");
            }

            Student student = _studentRepository.Find(studentID);

            if (student == null)
            {
                return null;
            }

            StudentDTO recDTO = StudentMapper.Map(student);

            return recDTO;
        }

        public List<StudentDTO> FindStudents(StudentFindCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException("criteria");
            }

            var students = _studentRepository.FindByCriteria(criteria);
            List<StudentDTO> result = new List<StudentDTO>();

            foreach (var student in students)
            {
                result.Add(StudentMapper.Map(student));
            }

            return result;
        }

        public void Update(StudentDTO recDTO)
        {
            Student student = _studentRepository.Find(recDTO.StudentID);

            if (student == null)
            {
                throw new DataException(string.Format(ValidationConstants.SDataNotFoundWithValue, "Student", recDTO.StudentID));
            }

            student = StudentMapper.Map(recDTO);
            _studentRepository.Update(student);

        }
    }
}
