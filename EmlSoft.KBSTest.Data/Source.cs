using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlSoft.KBSTest.Data
{
    public class Source
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(250)]
        [Required]
        [Index(IsUnique=true)]
        public string Url { get; set; }
    }
}
