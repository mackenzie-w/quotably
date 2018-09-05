using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotably.Data
{
    public class BookQuote
    {
        [Key]
        public int BookQuoteID { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int BookID { get; set; }
        public int AuthorID { get; set; }
        [Required]
        public Guid OwnerID { get; set; }
        [Required]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }

    }
}
