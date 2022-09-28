using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21570869_HW05.Models
{
    public class BookStudentsVM
    {
        public List<Student> Students { get; set; }
        public Book myBook { get; set; }

        public List<string> StudentClasses { get; set; }

        public Borrow Borrow { get; set; }
    }
}