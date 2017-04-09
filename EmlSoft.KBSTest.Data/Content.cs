using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmlSoft.KBSTest.Data
{
    public class Content
    {
        public int Id { get; set; }

        [ForeignKey("Source")]
        public int SourceId { get; set;  }

        public Source Source { get; set; }

        public string Data { get; set; }
    }
}
