using LibraryManagement.Common.Validation;
using LibraryManagement.Models;
using LibraryManagement.Services;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LibraryManagement.Controllers
{
    [RoutePrefix("Borrowing/Borrow")]
    public class BorrowController : BaseController
    {
        private readonly IBorrowService _borrowService;

        public BorrowController(IBorrowService borrowService) : base()
        {
            _borrowService = borrowService;
        }

        [HttpGet]
        [Route("")]
        public List<BorrowDTO> Get([FromUri]BorrowFindCriteria filter)
        {
            if (filter == null)
            {
                // allow null criteria and take defaults
                filter = new BorrowFindCriteria();
            }

            return _borrowService.FindBorrows(filter);
        }

        [HttpGet]
        [Route("{bookID:int:min(1)}/{studentID:int:min(1)}")]
        public BorrowDTO Get(int bookID, int studentID)
        {
           
            BorrowDTO Borrow = _borrowService.Find(bookID, studentID);

            if (Borrow == null)
            {
                throw new DataException(string.Format(ValidationConstants.SDataNotFoundWithValue, "Borrow", string.Format("book: {0} student: {1}", bookID, studentID)));
            }

            return Borrow;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody]BorrowDTO borrow)
        {
            if (borrow == null)
            {
                throw new ModelValidationResultsException(string.Format(ValidationConstants.SRequiredProperty, "borrow"));
            }

            if (borrow.BookID == 0 || borrow.StudentID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _borrowService.Add(borrow);    

            return Request.CreateResponse(HttpStatusCode.Created);
           
        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Put([FromBody]BorrowDTO borrow)
        {
            if (borrow == null)
            {
                throw new ModelValidationResultsException(string.Format(ValidationConstants.SRequiredProperty, "borrow"));
            }

            if (borrow.BookID == 0 || borrow.StudentID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _borrowService.Update(borrow);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
