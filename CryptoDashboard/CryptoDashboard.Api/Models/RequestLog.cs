using System;
using System.ComponentModel.DataAnnotations;

namespace CryptoDashboard.Api.Models
{
    public class RequestLog
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime BeginDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public DateTime RequestDate { get; set; }
    }
}
