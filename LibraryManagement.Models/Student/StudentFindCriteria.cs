using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagement.Models
{
    public class StudentFindCriteria
    {
        public StudentFindCriteria()
        {
            StudentID = null;
        }

        public int? StudentID { get; set; }
    }
}
