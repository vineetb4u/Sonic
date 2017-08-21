using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class BorrowFindCriteria
    {
        public BorrowFindCriteria()
        {
            BookID = null;
            StudentID = null;
            ReturnDate = null;
        }

        public int? BookID { get; set; }
        public int? StudentID { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
