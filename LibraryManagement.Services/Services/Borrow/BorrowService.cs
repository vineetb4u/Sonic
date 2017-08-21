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
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRepository _borrowRepository;

        public BorrowService(IBorrowRepository BorrowRepository)
        {
            _borrowRepository = BorrowRepository;
        }

        public void Add(BorrowDTO recDTO)
        {
            if (_borrowRepository.isBookAvaliable(recDTO.BookID))
            {
                Borrow Borrow = BorrowMapper.Map(recDTO);
                _borrowRepository.Add(Borrow);
            }
            else
            {
                throw new DataException("This Book is already borrowed");
            }

        }

        public void Delete(int bookID, int studentID)
        {
            if (bookID == 0)
            {
                throw new ArgumentException("bookID");
            }

            if (studentID == 0)
            {
                throw new ArgumentException("studentID");
            }

            Borrow Borrow = _borrowRepository.Find(bookID, studentID);
            _borrowRepository.Delete(Borrow);
        }

        public BorrowDTO Find(int bookID, int studentID)
        {
            Borrow borrow = _borrowRepository.Find(bookID, studentID);

            if (borrow == null)
            {
                return null;
            }

            BorrowDTO recDTO = BorrowMapper.Map(borrow);

            return recDTO;
        }
    
        public List<BorrowDTO> FindBorrows(BorrowFindCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException("criteria");
            }

            var Borrows = _borrowRepository.FindByCriteria(criteria);
            List<BorrowDTO> result = new List<BorrowDTO>();

            foreach (var Borrow in Borrows)
            {
                result.Add(BorrowMapper.Map(Borrow));
            }

            return result;
        }

        public void Update(BorrowDTO recDTO)
        {
            Borrow borrow = _borrowRepository.Find(recDTO.BookID, recDTO.StudentID);

            if (borrow == null)
            {
                throw new DataException(string.Format(ValidationConstants.SDataNotFoundWithValue, "Borrow", string.Format("book: {0} student: {1}", recDTO.BookID, recDTO.StudentID)));
            }

            borrow = BorrowMapper.Map(recDTO);
            _borrowRepository.Update(borrow);
        }
    }
}
