using Quotably.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quotably.Models
{
    public class BookCreate
    {
        [Required]
        public string Title { get; set; }
        public int AuthorID { get; set; }
        [Required]
        public string Description { get; set; }

        public virtual Author Author { get; set; }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
