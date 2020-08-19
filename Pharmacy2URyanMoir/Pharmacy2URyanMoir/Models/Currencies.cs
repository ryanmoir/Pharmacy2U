using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy2URyanMoir.Models
{
    public partial class Currencies
    {
        [Key]
        public int Id { get; set; }
        public int Exponent { get; set; }
        [Required]
        [StringLength(10)]
        public string Symbol { get; set; }
        [Required]
        [Column(TypeName = "ntext")]
        public string Name { get; set; }
    }
}
