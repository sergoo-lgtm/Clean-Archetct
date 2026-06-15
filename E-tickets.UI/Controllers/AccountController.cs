using eTickets.DTO;
using eTickets.Models.IdentityEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eTickets.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager; 
            _signInManager = signInManager;
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View("Register");
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(Temp => Temp.Errors).Select(temp => temp.ErrorMessage);
                return View(registerDTO);
            }
            ApplicationUser user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.Phone,
                UserName = registerDTO.Email,
                PersonName = registerDTO.PersonName,

            };
           IdentityResult result = await _userManager.CreateAsync(user,registerDTO.Password);
            if (result.Succeeded)
            {
                 await _signInManager.SignInAsync(user, false);
                return RedirectToAction(nameof(MoviesController.Index), "Movies");
                
            }

            else
            {
               foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("Register", error.Description);
                }
                ViewBag.Errors = ModelState.Values.SelectMany(Temp => Temp.Errors).Select(temp => temp.ErrorMessage);
                return View(registerDTO);
            }
        }
        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDTO loginDtodto)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Errors = ModelState.Values.SelectMany(Temp => Temp.Errors).Select(temp => temp.ErrorMessage);
                return View(loginDtodto);
            }

          var user = await _userManager.FindByEmailAsync(loginDtodto.Email!);
          if (user == null)
          {
              ModelState.AddModelError("Login", "Invalid Email or  Password");
              return View(loginDtodto);
          }

          var result=  await _signInManager.PasswordSignInAsync(user.UserName!, loginDtodto.Password!, false, false);

          if (result.Succeeded)
          {
              return RedirectToAction(nameof(MoviesController.Index), "Movies");

          }
         ModelState.AddModelError("Login", "Invalid Email or  Password");
            return View(loginDtodto);
        }
        
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(MoviesController.Index), "Movies");
        }


    }
}
