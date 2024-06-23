using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CryptoDashboard.Api.Models
{
    public class CryptoPair
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string PairName { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Open { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Close { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal High { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Low { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public long Volume { get; set; }

        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal ChangePercentage { get; set; }
    }
}
