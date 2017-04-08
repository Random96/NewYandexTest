using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlSoft.KBSTest.Domain
{
    public class Source
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        [Url]
        public string Url { get; set; }
    }
}
