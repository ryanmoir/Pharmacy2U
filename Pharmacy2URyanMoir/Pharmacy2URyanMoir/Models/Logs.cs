using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy2URyanMoir.Models
{
    public partial class Logs
    {
        [Key]
        public int Id { get; set; }
        public int ExchangeId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DateTime { get; set; }
    }
}
