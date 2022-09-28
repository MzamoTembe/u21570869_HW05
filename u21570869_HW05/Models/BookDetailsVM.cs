using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21570869_HW05.Models
{
    public class BookDetailsVM
    {
        public List<Borrow> Borrows { get; set; }
        public Book myBook { get; set; }
    }
}