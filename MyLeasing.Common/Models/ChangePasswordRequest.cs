using System.ComponentModel.DataAnnotations;

namespace MyLeasing.Common.Models
{
    public class ChangePasswordRequest
    {
        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string NewPassword { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}