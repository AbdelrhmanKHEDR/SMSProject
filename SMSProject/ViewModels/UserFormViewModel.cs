using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMSProject.ViewModels
{
    public class UserFormViewModel
    {
        public string Id { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(100)]
        public string Grade { get; set; } = null!;
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public string? Parents { get; set; }/*        public DateTime? LastUpdatedOn { get; set; }
*/
   /*     public string? ParentsDetails { get; set; }
        public string? ProfilePicture { get; set; }*/
        [Display(Name = "File")]
        [NotMapped] // Mark this property as not mapped to the database
        public IFormFile? File { get; set; }

        public string? FileAttachment { get; set; }

        public string? FilePath { get; set; }
        public string? FileName { get; set; }
        //public string? Password { get; set; }
        public string? PhoneNumber { get; set; }


    }
}
