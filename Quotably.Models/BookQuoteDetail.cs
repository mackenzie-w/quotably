using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotably.Models
{
    public class BookQuoteDetail
    {
        public int BookQuoteID { get; set; }
        public string Content { get; set; }
        public int BookID { get; set; }
        public int AuthorID { get; set; }
        [Display(Name ="Created")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name ="Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; }
        public override string ToString() => $"{BookQuoteID} {Content}";
    }
}
