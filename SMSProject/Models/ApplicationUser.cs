using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SMSProject.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(100)]
        public string FullName { get; set; } = null!;

        [MaxLength(500)]
        public string? Address { get; set; } 
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(100)]
        public string Grade { get; set; } = null!;

        public bool IsDeleted {  get; set; }
        public string? CreatedById { get; set; }

        public DateTime CreatedOn {  get; set; } = DateTime.Now;

        public string? LastUpdatedById { get; set; }

        public DateTime? LastUpdatedOn {  get; set; } 

    }
}
