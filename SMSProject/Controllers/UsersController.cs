using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMSProject.Data.Consts;
using SMSProject.Models;
using SMSProject.ViewModels;

namespace SMSProject.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UsersController(UserManager<ApplicationUser> userManager , IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var viewModel = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserFormViewModel model)
        {

            var user = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                DateOfBirth = model.DateOfBirth,
                Grade = model.Grade,
                IsDeleted = model.IsDeleted,
                CreatedOn = DateTime.Now,
                Parents = model.Parents,
                PhoneNumber = model.PhoneNumber,

           
                //PasswordHash = model.Password,
                //ProfilePicture = model.ProfilePicture,
                FileAttachment = model.FileAttachment,
                FilePath = model.FilePath,
                FileName = model.FileName,
                // Set other attributes here
            };
            if (model.File != null)
            {
                var FileName = model.File.FileName;
                // Save the uploaded file to a folder (you need to define the folder path)

                var uploadsFolder = Path.Combine("uploads");
                var uniqueFileName = Guid.NewGuid().ToString() + "_" + model.File.FileName;
                var filePath = Path.Combine(uploadsFolder, FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.File.CopyTo(stream);
                }

                // Save the file name to the database
                user.FileAttachment = FileName;
                user.FilePath = filePath;
                user.FileName = FileName;
            }

            var result = await _userManager.CreateAsync(user); 
            // Assuming you're also setting the password
            if (result.Succeeded)
            {
                // Optionally add the user to roles, send confirmation email, etc.
                return RedirectToAction("Index");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View("Create");
        }
    }

          

    }

