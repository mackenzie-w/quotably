using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotably.Models
{
    public class AuthorListItem
    {
        [Key]
        public int AuthorID { get; set; }
        public string AuthorFirstName { get; set; }
        public string AuthorLastName { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
