

using Microsoft.AspNetCore.Mvc;
using PizzaShop.Service.Interface;
using PizzaShop.Domain.ViewModels;
using System;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using PizzaShop.Domain.DataModels;
using PizzaShop.Service.Implementation;

namespace PizzaShop.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        private async Task SetUserProfileInViewBag()
        {
            var token = Request.Cookies["AuthToken"];
            var email = _userServices.ExtractEmailFromToken(token);

            if (!string.IsNullOrEmpty(email))
            {
                var userProfile = await _userServices.GetUserProfileAsync(email);
                if (userProfile != null)
                {
                    ViewBag.UserName = userProfile.UserName;
                    ViewBag.UserImage = userProfile.PathOfProfilePicture;
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            await SetUserProfileInViewBag();

            var token = Request.Cookies["AuthToken"];
            var email = _userServices.ExtractEmailFromToken(token);

            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Auth");

            var model = await _userServices.GetUserProfileAsync(email);
            if (model == null)
                return RedirectToAction("Login", "Auth");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UpdateProfileModel model)
        {
            await SetUserProfileInViewBag();

            var token = Request.Cookies["AuthToken"];
            var email = _userServices.ExtractEmailFromToken(token);

            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login", "Auth");

            var success = await _userServices.UpdateUserProfileAsync(model, email);
            if (!success)
                return View(model);

            TempData["success"] = "Profile updated successfully!";
            return RedirectToAction("UpdateProfile");
        }



        [HttpGet]
        public async Task<IActionResult> GetStates(int countryId)
        {
            var states = await _userServices.GetStatesAsync(countryId);
            return Json(states);
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(int stateId)
        {
            var cities = await _userServices.GetCitiesAsync(stateId);
            return Json(cities);
        }

        [HttpGet]
        public async Task<IActionResult> UserList(string searchQuery = "", int pageNumber = 1, int pageSize = 10, string sortBy = "Name", string sortOrder = "asc")
        {
            await SetUserProfileInViewBag();

            var (users, totalCount) = await _userServices.GetUserListAsync(searchQuery, pageNumber, pageSize, sortBy, sortOrder);
            ViewBag.TotalCount = totalCount;


            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            ViewBag.CurrentPage = pageNumber;
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.SearchQuery = searchQuery;
            ViewBag.SortBy = sortBy;
            ViewBag.SortOrder = sortOrder;
            
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> SoftDeleteUser(int id)
        {
            await SetUserProfileInViewBag();

            var success = await _userServices.SoftDeleteUserAsync(id);
            if (!success)
            {
                TempData["error"] = "Failed to delete user.";
            }

            TempData["success"] = "user deleted successfully!";
            return RedirectToAction("UserList");
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(int id)
        {
            await SetUserProfileInViewBag();



            var model = await _userServices.GetUserByIdAsync(id);
            if (model == null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserModel model)
        {
            await SetUserProfileInViewBag();



            var success = await _userServices.UpdateUserAsync(model);
            if (!success) return NotFound();

            TempData["success"] = "User updated successfully!";
            return RedirectToAction("UserList");
        }

        [HttpGet]
        public async Task<IActionResult> AddUserAsync()
        {
            await SetUserProfileInViewBag();

            var model = new UserModel
            {
                Countries = _userServices.GetCountriesAsync().Result,
                States = new List<State>(),
                Cities = new List<City>()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddUserAsync(UserModel model)
        {
            await SetUserProfileInViewBag();

            // Check if the username already exists
            var existingUser = await _userServices.GetUserByUsernameAsync(model.UserName);
            if (existingUser != null)
            {
                TempData["error"] = "Username already exists.";
                 return RedirectToAction("AddUser");
                // return View(model);
                // throw new Exception("Username already exists.");
            }

            var existUser= await _userServices.GetCurrentUserAsync(model.Email);
            if(existUser!=null)
            {
                TempData["error"] = "Email already exists.";
                 return RedirectToAction("AddUser");
                // return View(model);
                // throw new Exception("Email already exists.");
            }

            _userServices.AddUser(model);

            // Send welcome email
            await _userServices.SendWelcomeEmailAsync(model.Email, model.Password);

            TempData["success"] = "User added successfully!";
            return RedirectToAction("AddUser");
        }
    }
}