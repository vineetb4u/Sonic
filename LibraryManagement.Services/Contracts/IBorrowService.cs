using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IBorrowService
    {
        List<BorrowDTO> FindBorrows(BorrowFindCriteria criteria);
        void Add(BorrowDTO borrow);
        void Update(BorrowDTO borrow);
        void Delete(int bookID, int studentID);
        BorrowDTO Find(int bookID, int studentID);
    }
}
