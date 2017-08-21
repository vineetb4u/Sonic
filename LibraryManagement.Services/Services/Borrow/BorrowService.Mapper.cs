using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagement.Models;
using LibraryManagement.Data.Entities;

namespace LibraryManagement.Services
{
    public static class BorrowMapper
    {
        public static BorrowDTO Map(Borrow source, BorrowDTO target = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (target == null)
            {
                target = new BorrowDTO();
            }

            target.BookID = source.BookID;
            target.StudentID = source.StudentID;
            target.DueDate = source.DueDate;

            if (source.ReturnDate.HasValue)
                target.ReturnDate = source.ReturnDate.Value;

            return target;
        }


        public static Borrow Map(BorrowDTO source, Borrow target = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (target == null)
            {
                target = new Borrow();
            }

            target.BookID = source.BookID;
            target.StudentID = source.StudentID;
            target.DueDate = source.DueDate;

            if (source.ReturnDate.HasValue)
                target.ReturnDate = source.ReturnDate.Value;

            return target;
        }

    }
}
