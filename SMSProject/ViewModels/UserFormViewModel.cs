using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Framework;
using SMSProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UoN.ExpressiveAnnotations.NetCore.Attributes;
namespace SMSProject.ViewModels
{
    public class UserFormViewModel
    {
        public string? Id { get; set; }

        [MaxLength(100, ErrorMessage = Errors.MaxLength), Display(Name = "Full Name"),
        RegularExpression(RegexPatterns.CharactersOnly_Eng, ErrorMessage = Errors.OnlyEnglishLetters)]
        public string FullName { get; set; } = null!;

        [MaxLength(20, ErrorMessage = Errors.MaxLength), Display(Name = "Username"),
        RegularExpression(RegexPatterns.Username, ErrorMessage = Errors.InvalidUsername)]
        [Remote("AllowUserName", null!, AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]
        public string Username { get; set; } = null!;

        [MaxLength(200, ErrorMessage = Errors.MaxLength), EmailAddress]
        [Remote("AllowEmail", null!, AdditionalFields = "Id", ErrorMessage = Errors.Duplicated)]
        public string Email { get; set; } = null!;

        public DateTime? DateOfBirth { get; set; }


        [DataType(DataType.Password),
             StringLength(100, ErrorMessage = Errors.MaxMinLength, MinimumLength = 8),
             RegularExpression(RegexPatterns.Password, ErrorMessage = Errors.WeakPassword)]
        [RequiredIf("Id == null", ErrorMessage = Errors.RequiredField)]
        public string? Password { get; set; } = null!;

        [DataType(DataType.Password), Display(Name = "Confirm password"),
              Compare("Password", ErrorMessage = Errors.ConfirmPasswordNotMatch)]
        [RequiredIf("Id == null", ErrorMessage = Errors.RequiredField)]
        public string? ConfirmPassword { get; set; } = null!;


        public IList<string> SelectedRoles { get; set; } = new List<string>();

        public IEnumerable<SelectListItem>? Roles { get; set; }
        [MaxLength(100, ErrorMessage = Errors.MaxLength), Display(Name = "Grade"),]
        public string Grade { get; set; } = null!;
        public bool IsDeleted { get; set; }
        /*        public DateTime CreatedOn { get; set; }
        */
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
        [MaxLength(100, ErrorMessage = Errors.MaxLength), Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = null!;

        [MaxLength(100, ErrorMessage = Errors.MaxLength), Display(Name = "Address"),
          RegularExpression(RegexPatterns.CharactersOnly_Eng, ErrorMessage = Errors.OnlyEnglishLetters)]
        public string Address { get; set; } = null!;

        [MaxLength(100, ErrorMessage = Errors.MaxLength), Display(Name = "Parent1"),
          RegularExpression(RegexPatterns.CharactersOnly_Eng, ErrorMessage = Errors.OnlyEnglishLetters)]
        public string Parents1 { get; set; } = null!;

        [MaxLength(100, ErrorMessage = Errors.MaxLength), Display(Name = "Parent2"),
          RegularExpression(RegexPatterns.CharactersOnly_Eng, ErrorMessage = Errors.OnlyEnglishLetters)]
        public string Parents2 { get; set; } = null!;

    }
}
