using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Models;

namespace LibraryManagement.Services
{
    public interface IBookService : IBaseService<BookDTO>
    {
        List<BookDTO> FindBooks(BookFindCriteria criteria);
        BookDTO Find(int bookID);
    }
}
