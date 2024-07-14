//EDIT
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SMSProject.Data;
using SMSProject.Data.Consts;
using SMSProject.Models;
using SMSProject.ViewModels;
using System.Security.Claims;

namespace SMSProject.Controllers
{
    [Authorize(Roles = AppRoles.Admin)]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        /*   public async Task<IActionResult> Index()
           {
               var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the logged-in user's ID
               var user = await _userManager.FindByIdAsync(userId); // Retrieve the user

               if (user == null)
               {
                   return NotFound();
               }

               var userViewModel = new UserViewModel
               {
                   // Map your user data to the view model
                   FilePath = user.FilePath,
                   // Add other properties as needed
               };

               return View(userViewModel); // Pass the view model to the view
           }*/


        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var viewModel = _mapper.Map<IEnumerable<UserViewModel>>(users);
            return View(viewModel);
        }
        /*        public async Task<IActionResult> Index()
                {
                    var user = await _userManager.GetUserAsync(User);
                    var viewModel = new UserViewModel
                    {
                        FilePath = user?.FilePath,
                        // Set other properties as necessary
                    };

                    return View(viewModel);
                }*/
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var viewModel = new UserFormViewModel
            {
                Roles = await _roleManager.Roles
                               .Select(r => new SelectListItem
                               {
                                   Text = r.Name,
                                   Value = r.Name
                               })
                               .ToListAsync()
            };

            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserFormViewModel model)
        {
            /*  try { */

            ApplicationUser user = new()
            {
                UserName = model.Username,
                Email = model.Email,
                FullName = model.FullName,
                DateOfBirth = model.DateOfBirth,
                Grade = model.Grade,
                IsDeleted = model.IsDeleted,
                CreatedOn = DateTime.Now,
                /*                Parents = model.Parents,
                */
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                Parents1 = model.Parents1,
                Parents2 = model.Parents2,

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

            var result = await _userManager.CreateAsync(user, model.Password); // Use UserManager to hash and save password

            /*          _context.SaveChanges();
            */
            if (result.Succeeded)
            {
                /*if (!string.IsNullOrEmpty(model.SelectedRoles))
				{*/
                await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                /*	}*/


                _context.SaveChanges(); // Save other changes to the context if needed
                                        // Optionally add the user to roles, send confirmation email, etc.
                TempData["Message"] = "This user has been successfully created!";
                return RedirectToAction("Index");

                /*                    return Json(new { success = true });
                */
            }
            /*    }*/
            /*   catch (Exception ex)
                {
                            // Log the exception (using your preferred logging framework)
                            return Json(new { success = false, errors = new[] { ex.Message } });
                        }*/

            /*	foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}*/
            return View("Create");
        }


        [HttpGet]
        /*        [AjaxOnly]
        */
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            var viewModel = _mapper.Map<UserFormViewModel>(user);
            viewModel.FilePath = Url.Content($"~/uploads/{user.FileName}"); // Adjust the file path accordingly

            TempData["FileName"] = user.FileAttachment;
            TempData["ExistingFileName"] = user.FileName;
            TempData["FileName"] = user.FileAttachment;
            viewModel.SelectedRoles = await _userManager.GetRolesAsync(user);
            viewModel.Roles = await _roleManager.Roles
                                .Select(r => new SelectListItem
                                {
                                    Text = r.Name,
                                    Value = r.Name
                                })
                                .ToListAsync();

            return View("Create", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserFormViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(model.Id);

            if (user is null)
                return NotFound();

            user = _mapper.Map(model, user);

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
                /* TempData.Remove("FileName");*/
            }
            /*  else
              {*/
            else
            {
                // Fetch the existing file information from TempData
                var existingFileName = TempData["ExistingFileName"]?.ToString();
                var FileName = TempData["FileName"]?.ToString();
                var uploadsFolder = Path.Combine("uploads");
                var filePath = Path.Combine(uploadsFolder, FileName);
                /*  var uploadsFolder = Path.Combine("uploads");
                  var uniqueFileName = Guid.NewGuid().ToString() + "_" + existingFileName;
                  var filePath = Path.Combine(uploadsFolder, uniqueFileName);*/
                /* var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                 using (var stream = new FileStream(filePath, FileMode.Create))
                 {
                     model.File.CopyTo(stream);
                 }*/
                /* var uploadsFolder = Path.Combine("uploads");
                 var filePath = Path.Combine(uploadsFolder, existingFileName);*/
                if (!string.IsNullOrEmpty(existingFileName))
                {
                    // Use existing file information
                    user.FileAttachment = existingFileName;
                    user.FilePath = filePath; // Optionally set to null if you want to keep existing FilePath
                    user.FileName = existingFileName;
                }
            }
            /*			user.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
			*/
            user.LastUpdatedOn = DateTime.Now;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                var rolesUpdated = !currentRoles.SequenceEqual(model.SelectedRoles);

                if (rolesUpdated)
                {
                    await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                }

                /*				await _signInManager.RefreshSignInAsync(user);
                */
                var viewModel = _mapper.Map<UserFormViewModel>(user);
                TempData["Message"] = "This user has been successfully edited!";
                return RedirectToAction("Index");

                /*                return Json(new { success = true });
                */
            }

            return BadRequest(string.Join(',', result.Errors.Select(e => e.Description)));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            user.IsDeleted = !user.IsDeleted;
            /*            user.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            */
            user.LastUpdatedOn = DateTime.Now;

            await _userManager.UpdateAsync(user);

            return Ok(user.LastUpdatedOn.ToString());
        }
        /*	[HttpGet]

            public async Task<IActionResult> Edit(string id)
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user is null)
                    return NotFound();

                var viewModel = _mapper.Map<UserFormViewModel>(user);

                viewModel.SelectedRoles = await _userManager.GetRolesAsync(user);
                viewModel.Roles = await _roleManager.Roles
                                    .Select(r => new SelectListItem
                                    {
                                        Text = r.Name,
                                        Value = r.Name
                                    })
                                    .ToListAsync();

                return View("Create", viewModel);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(UserFormViewModel model)
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var user = await _userManager.FindByIdAsync(model.Id);

                if (user is null)
                    return NotFound();

                user = _mapper.Map(model, user);
    *//*			user.LastUpdatedById = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
    *//*			user.LastUpdatedOn = DateTime.Now;

                var result = await _userManager.UpdateAsync(user); //this is for _context.savechanges() that is what this is for

                if (result.Succeeded)
                {
                    var currentRoles = await _userManager.GetRolesAsync(user);

                    var rolesUpdated = !currentRoles.SequenceEqual(model.SelectedRoles);

                    if (rolesUpdated)
                    {
                        await _userManager.RemoveFromRolesAsync(user, currentRoles);
                        await _userManager.AddToRolesAsync(user, model.SelectedRoles);
                    }

                    var viewModel = _mapper.Map<UserViewModel>(user);
                    return View("Create", viewModel);
                }

                return BadRequest(string.Join(',', result.Errors.Select(e => e.Description)));
            }
    */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Unlock(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return NotFound();

            var isLocked = await _userManager.IsLockedOutAsync(user);

            if (isLocked)
                await _userManager.SetLockoutEndDateAsync(user, null);

            return Ok();
        }

        public async Task<IActionResult> AllowUserName(UserFormViewModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            var isAllowed = user is null || user.Id.Equals(model.Id);

            return Json(isAllowed);
        }

        public async Task<IActionResult> AllowEmail(UserFormViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var isAllowed = user is null || user.Id.Equals(model.Id);

            return Json(isAllowed);
        }
    }




}

