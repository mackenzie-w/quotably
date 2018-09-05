using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotably.Models
{
    public class BookQuoteCreate
    {
        [Required]
        [MaxLength(10000)]
        public string Content { get; set; }
        public int BookID { get; set; }
        public int AuthorID { get; set; }

        public override string ToString() => Content;
    }
}
