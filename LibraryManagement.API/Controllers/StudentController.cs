using LibraryManagement.Common.Validation;
using LibraryManagement.Models;
using LibraryManagement.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryManagement.Controllers
{
    [RoutePrefix("Students/Student")]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService) : base()
        {
            _studentService = studentService;
        }

        [HttpGet]
        [Route("")]
        public List<StudentDTO> Get([FromUri]StudentFindCriteria filter)
        {
            if (filter == null)
            {
                // allow null criteria and take defaults
                filter = new StudentFindCriteria();
            }

            return _studentService.FindStudents(filter);
        }

        [HttpGet]
        [Route("{studentID:int:min(1)}")]
        public StudentDTO Get(int studentID)
        {
          
            StudentDTO student = _studentService.Find(studentID);

            if (student == null)
            {
                throw new DataException(string.Format(ValidationConstants.SDataNotFoundWithValue, "Student", studentID));
            }

            return student;
        }


        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody]StudentDTO student)
        {
            if (student == null)
            {
                throw new ModelValidationResultsException(string.Format(ValidationConstants.SRequiredProperty, "student"));
            }

            if (student.StudentID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _studentService.Add(student);

            return Request.CreateResponse(HttpStatusCode.Created);

        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Put([FromBody]StudentDTO student)
        {
            if (student == null)
            {
                throw new ModelValidationResultsException(string.Format(ValidationConstants.SRequiredProperty, "student"));
            }

            if (student.StudentID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _studentService.Update(student);

            return Request.CreateResponse(HttpStatusCode.OK);
        }

    }
}
