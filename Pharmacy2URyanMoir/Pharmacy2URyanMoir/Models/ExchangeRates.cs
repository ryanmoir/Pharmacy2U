using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pharmacy2URyanMoir.Models
{
    public partial class ExchangeRates
    {
        [Key]
        public int Id { get; set; }
        public int BaseCurrecncy { get; set; }
        public int ConvertedCurrency { get; set; }
        public float ExchangeRate { get; set; }
    }
}
