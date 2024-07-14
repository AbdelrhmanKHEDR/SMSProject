using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMSProject.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(UserName), IsUnique = true)]
    public class ApplicationUser : IdentityUser
    {
      
        public string ?FullName { get; set; } = null!;

       
        public string? Address { get; set; } = null!;
        public DateTime? DateOfBirth { get; set; }

        public string? Grade { get; set; } = null!;

        public bool IsDeleted {  get; set; }
    /*    public string? CreatedById { get; set; }*/

        public DateTime CreatedOn {  get; set; } 

        /*        public string? LastUpdatedById { get; set; }
        */
        public DateTime? LastUpdatedOn { get; set; }
        public string? Parents {  get; set; }
        public string? Parents1 {  get; set; } = null!;
        public string? Parents2 {  get; set; } = null!;
        /*        public string? ParentsDetails {  get; set; }*/
        /*        public string? ProfilePicture {  get; set; }
        */
        [Display(Name = "File")]
        [NotMapped] // Mark this property as not mapped to the database
        public IFormFile? File { get; set; }
   
        public string? FileAttachment { get; set; }

        public string? FilePath { get; set; }
        public string? FileName { get; set; }
       
    }
}
