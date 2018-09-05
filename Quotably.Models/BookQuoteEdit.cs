using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotably.Models
{
    public class BookQuoteEdit
    {
        public int BookQuoteID { get; set; }
        public string Content { get; set; }
        public int BookID { get; set; }
        public int AuthorID { get; set; }
    }
}
