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
    [RoutePrefix("Books/Book")]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("")]
        public List<BookDTO> Get([FromUri]BookFindCriteria filter)
        {
            if (filter == null)
            {
                // allow null criteria and take defaults
                filter = new BookFindCriteria();
            }

            return _bookService.FindBooks(filter);
        }

        [HttpGet]
        [Route("{bookID:int:min(1)}")]
        public BookDTO Get(int bookID)
        {

            BookDTO book = _bookService.Find(bookID);

            if (book == null)
            {
                throw new DataException(string.Format(ValidationConstants.SDataNotFoundWithValue, "Book", bookID));
            }

            return book;
        }

        [HttpPost]
        [Route("")]
        public HttpResponseMessage Post([FromBody]BookDTO book)
        {
            if (book == null)
            {
                throw new ModelValidationResultsException(string.Format(ValidationConstants.SRequiredProperty, "Book"));
            }

            if (book.BookID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _bookService.Add(book);

            return Request.CreateResponse(HttpStatusCode.Created);

        }

        [HttpPut]
        [Route("")]
        public HttpResponseMessage Put([FromBody]BookDTO book)
        {
            if (book == null)
            {
                throw new ModelValidationResultsException(string.Format(ValidationConstants.SRequiredProperty, "Book"));
            }

            if (book.BookID == 0)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            _bookService.Update(book);

            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
    }
}
