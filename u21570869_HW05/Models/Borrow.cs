using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21570869_HW05.Models
{
    public class Borrow
    {
        public int ID { get; set; }
        public Student Student { get; set; }
        public Book Book { get; set; }
        public string TakenDate { get; set; }
        public string BroughtDate { get; set; }
    }
}