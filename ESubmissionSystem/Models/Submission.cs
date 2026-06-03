using System;
using System.ComponentModel.DataAnnotations;

namespace ESubmissionSystem.Models
{
    public class Submission
    {
        public int Id { get; set; }
        public string UserEmail { get; set; }

        [Required]
        public string ClaimType { get; set; } // e.g., Medical, Leave

        [Required]
        public string Description { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string Status { get; set; } // Pending, Approved, Rejected

        public DateTime CreatedDate { get; set; }

        public Submission()
        {
            CreatedDate = DateTime.Now;
            Status = "Pending";
        }
    }
}