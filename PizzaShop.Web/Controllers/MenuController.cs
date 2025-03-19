

using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
using PizzaShop.Domain.DataModels;
using PizzaShop.Domain.ViewModels;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Service.Interface;
using PizzaShop.Services.Interfaces;
using System;
// using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PizzaShop.Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IUserServices _userServices;

        public MenuController(ICategoryService categoryService, IUserServices userServices)
        {
            _categoryService = categoryService;
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

        public async Task<IActionResult> Menu()
        {
            await SetUserProfileInViewBag();

            var model = new MenuViewModel
            {
                Categories = await _categoryService.GetCategoriesAsync(),
                ModifierGroups = await _categoryService.GetModifierAsync()
            };
            return View(model);

            // var categories = await _categoryService.GetCategoriesAsync();
            // return View(categories);
        }

        public async Task<IActionResult> Modifier()
        {
            await SetUserProfileInViewBag();

            var Modifier = await _categoryService.GetModifierAsync();
            return View(Modifier);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            try
            {
                if (category == null || string.IsNullOrWhiteSpace(category.CategoryName))
                    return BadRequest(new { message = "Invalid category data" });

                await _categoryService.AddCategoryAsync(category);

                TempData["success"] = "category added successfully";

                return Json(new { message = "Category added successfully", name = category.CategoryName });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error adding category", details = ex.Message });
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddModifierGroup([FromBody] ModifierGroupViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool result = await _categoryService.AddModifierGroupAsync(model);
                if (result)
                {
                    return Ok();
                }
                return StatusCode(500, "Failed to add modifier group.");
            }
            return BadRequest("Invalid data.");
        }

      

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] Category category)
        {
            if (category == null || id != category.Id)
                return BadRequest(new { message = "Invalid category data" });

            try
            {
                await _categoryService.UpdateCategoryAsync(category);
                TempData["success"] = "category updated successfully";
                return Ok(new { message = "Category updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating category", details = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateModifier(int id, [FromBody] Modifiergroup modifiergroup)
        {
            if (modifiergroup == null || id != modifiergroup.Id)
                return BadRequest(new { message = "Invalid category data" });

            try
            {
                await _categoryService.UpdateModifierAsync(modifiergroup);
                TempData["success"] = "category updated successfully";
                return Ok(new { message = "Category updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error updating category", details = ex.Message });
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                await _categoryService.DeleteCategoryAsync(id);
                TempData["success"] = "category deleted successfully";
                return Ok(new { message = "Category deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting category", details = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> SoftDeleteCategory(int id)
        {
            await _categoryService.SoftDeleteCategoryAsync(id);
            TempData["success"] = "category deleted successfully";
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> SoftDeleteModifier(int id)
        {
            await _categoryService.SoftDeleteModifierAsync(id);
            TempData["success"] = "category deleted successfully";
            return NoContent();
        }



        [HttpGet]
        public async Task<IActionResult> GetMenuItemsByCategory(int categoryId)
        {
            var menuItems = await _categoryService.GetMenuItemsByCategoryAsync(categoryId);
            return Json(menuItems);
        }

         [HttpGet]
        public async Task<IActionResult> GetModifierByModifierGroup(int id)
        {
            var menuItems = await _categoryService.GetModifiersByGroupAsync(id);
            return Json(menuItems);
        }

            [HttpGet]
        public async Task<IActionResult> GetModifierGroups()
        {
 
            var modifierGroups = await _categoryService.GetModifierAsync();
            return Json(new { modifierGroups});
        }



        [HttpGet]
        public async Task<IActionResult> GetDropdownData()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            var itemTypes = await _categoryService.GetItemTypesAsync();
            var units = await _categoryService.GetUnitsAsync();
            var modifierGroups = await _categoryService.GetModifierAsync();
            return Json(new { categories,modifierGroups, itemTypes, units });
        }

        [HttpPost]
        public async Task<IActionResult> AddMenuItem([FromForm] MenuItemViewModel model)
        {
            var result = await _categoryService.AddMenuItemAsync(model);

            if (result != null)
            {
                TempData["success"] = "menu item added successfully";
                return Ok(new { message = "Menu item added successfully!" });
            }
            else
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem([FromForm] MenuItemViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Menu item is null.");
            }

            await _categoryService.UpdateMenuItemAsync(model);
            TempData["success"] = "menu item updated successfully";
            return Ok(new { message = "Menu item updated successfully" });
        }

        [HttpPut]
        public async Task<IActionResult> SoftDeleteItem(int id)
        {
            var result = await _categoryService.SoftDeleteItemAsync(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }


        [HttpPut]
        public async Task<IActionResult> BulkSoftDeleteItems([FromBody] BulkDeleteRequest request)
        {
            if (request == null || request.ItemIds == null || request.ItemIds.Count == 0)
            {
                return BadRequest("No items selected for deletion.");
            }

            bool result = await _categoryService.BulkSoftDeleteItems(request.ItemIds);
            if (result)
            {
                return Ok();
            }
            return StatusCode(500, "Failed to delete items.");
        }

        [HttpPost]
        public async Task<IActionResult> AddModifier([FromBody] ModifiersViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Log validation errors for debugging
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();
                Console.WriteLine("ModelState Errors: " + string.Join(", ", errors));

                return BadRequest(new { success = false, message = "Invalid data", errors });
            }


            if (ModelState.IsValid)
            {
                bool isAdded = await _categoryService.AddModifierAsync(model);
                if (isAdded)
                {
                    return Json(new { success = true });
                }
            }

            return Json(new { success = false });
        }

    }
}