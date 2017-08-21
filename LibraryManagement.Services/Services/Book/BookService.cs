using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Models;
using LibraryManagement.Data;
using LibraryManagement.Data.Contracts;
using LibraryManagement.Data.Entities;
using System.Data;
using LibraryManagement.Utils.Constants;

namespace LibraryManagement.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBorrowRepository _borrowRepository;


        public BookService(IBookRepository bookRepository, IBorrowRepository borrowRepository)
        {
            _bookRepository = bookRepository;
            _borrowRepository = borrowRepository;
        }

        public void Add(BookDTO recDTO)
        {
            Book book = BookMapper.Map(recDTO);
            _bookRepository.Add(book);
        }

        public void Delete(int ID)
        {
            if (ID == 0)
            {
                throw new ArgumentException("BookID");
            }

            Book book = _bookRepository.Find(ID);

            if (book == null)
            {
                throw new DataException(string.Format(ValidationConstants.SDataNotFoundWithValue, "Book", ID));
            }

            _bookRepository.Delete(book);
        }

        public BookDTO Find(int bookID)
        {
            if (bookID == 0)
            {
                throw new ArgumentException("bookID");
            }

            Book book = _bookRepository.Find(bookID);

            if (book == null)
            {
                return null;
            }

            BookDTO recDTO = BookMapper.Map(book);
            recDTO.IsAvaliable = _borrowRepository.isBookAvaliable(bookID);

            return recDTO;
        }

        public List<BookDTO> FindBooks(BookFindCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException("criteria");
            }

            var books = _bookRepository.FindByCriteria(criteria);
            List<BookDTO> result = new List<BookDTO>();
            BookDTO recDTO;
            foreach (var book in books)
            {

                recDTO = BookMapper.Map(book);

                if (criteria.IncludeIsAvaliable)
                {
                    recDTO.IsAvaliable = _borrowRepository.isBookAvaliable(recDTO.BookID);
                }

                if (criteria.IsOverdue.HasValue)
                {
                    if (criteria.IsOverdue.Value == true && _borrowRepository.isBookOverdue(recDTO.BookID))
                    {
                        result.Add(recDTO);
                    }
                    else if (criteria.IsOverdue.Value == false && !_borrowRepository.isBookOverdue(recDTO.BookID))
                    {
                        result.Add(recDTO);
                    }
                }
                else
                {
                    result.Add(recDTO);
                }


            }

            return result;
        }

        public void Update(BookDTO recDTO)
        {
            Book book = _bookRepository.Find(recDTO.BookID);

            if (book == null)
            {
                throw new DataException(string.Format(ValidationConstants.SDataNotFoundWithValue, "Book", recDTO.BookID));
            }

            book = BookMapper.Map(recDTO);
            _bookRepository.Update(book);

        }
    }
}
