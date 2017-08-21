using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Data.Entities;
using LibraryManagement.Models;

namespace LibraryManagement.Data.Contracts
{
    public interface IBorrowRepository : IRepository<Borrow>
    {
        IEnumerable<Borrow> FindByCriteria(BorrowFindCriteria criteria);
        Borrow Find(int bookID, int studentID);
        bool isBookAvaliable(int bookID);
        bool isBookOverdue(int bookID);
    }
}
