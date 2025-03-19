using PizzaShop.Domain.DataModels;
using PizzaShop.Domain.ViewModels;
using PizzaShop.Repository.Interfaces;
using PizzaShop.Services.Interfaces;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace PizzaShop.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;



        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;

        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _categoryRepository.GetCategoriesAsync();
        }

        public async Task<List<Modifiergroup>> GetModifierAsync()
        {
            return await _categoryRepository.GetModifierAsync();
        }

        public async Task AddCategoryAsync(Category category)
        {
            if (category == null || string.IsNullOrWhiteSpace(category.CategoryName))
                throw new ArgumentException("Category data is invalid.");

            await _categoryRepository.AddCategoryAsync(category);
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            await _categoryRepository.UpdateCategoryAsync(category);
        }

        public async Task UpdateModifierAsync(Modifiergroup modifiergroup)
        {
            await _categoryRepository.UpdateModifierAsync(modifiergroup);
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            await _categoryRepository.DeleteCategoryAsync(categoryId);
        }

        public async Task SoftDeleteCategoryAsync(int id)
        {
            await _categoryRepository.SoftDeleteCategoryAsync(id);
        }

        public async Task SoftDeleteModifierAsync(int id)
        {
            await _categoryRepository.SoftDeleteModifierAsync(id);
        }

        public async Task<List<Menuitem>> GetMenuItemsByCategoryAsync(int categoryId)
        {
            return await _categoryRepository.GetMenuItemsByCategoryAsync(categoryId);
        }

        public async Task<List<Modifier>> GetModifiersByGroupAsync(int groupId)
        {
            return await _categoryRepository.GetModifiersByGroupAsync(groupId);
        }

        public async Task<List<string>> GetItemTypesAsync()
        {
            return await _categoryRepository.GetItemTypesAsync();
        }

        public async Task<List<Unit>> GetUnitsAsync()
        {
            return await _categoryRepository.GetUnitsAsync();
        }

        // public async Task<Menuitem> AddMenuItemAsync(MenuItemViewModel model)
        // {
        //     var menuItem = new Menuitem
        //     {
        //         CategoryId = model.CategoryId,
        //         ItemName = model.ItemName,
        //         ItemType = model.ItemType,
        //         Rate = model.Rate,
        //         Quantity = model.Quantity,
        //         UnitId = int.Parse(model.Unit),
        //         IsAvailable = model.IsAvailable,
        //         DefaultTax = model.DefaultTax,
        //         TaxPercentage = (decimal)model.TaxPercentage,
        //         Shortcode = model.Shortcode,
        //         Description = model.Description,
        //         ImagePath = model.ImagePath,


        //     };

        //      if (model.Image != null)
        // {
        //     string uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users");


        //     if (!Directory.Exists(uploads))
        //     {
        //         Directory.CreateDirectory(uploads);
        //     }

        //     string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
        //     string filePath = Path.Combine(uploads, uniqueFileName);

        //     using (var fileStream = new FileStream(filePath, FileMode.Create))
        //     {
        //         await model.Image.CopyToAsync(fileStream);
        //     }

        //     menuItem.ImagePath = "/images/users/" + uniqueFileName;
        // }
        //         // ImagePath = model.Image != null ? model.Image.ToString() : null // Handle file upload 

        //     await _categoryRepository.AddAsync(menuItem);
        //     return menuItem;
        // }

        public async Task<Menuitem> AddMenuItemAsync(MenuItemViewModel model)
        {
            var menuItem = new Menuitem
            {
                CategoryId = model.CategoryId,
                ItemName = model.ItemName,
                ItemType = model.ItemType,
                Rate = model.Rate,
                Quantity = model.Quantity,
                UnitId = int.Parse(model.Unit),
                IsAvailable = model.IsAvailable,
                DefaultTax = model.DefaultTax,
                TaxPercentage = model.TaxPercentage,
                Shortcode = model.Shortcode,
                Description = model.Description,
                ImagePath = model.ImagePath,
            };

            // File upload logic (as you have it)
            if (model.Image != null)
            {
                string uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users");

                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                string uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploads, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(fileStream);
                }

                menuItem.ImagePath = "/images/users/" + uniqueFileName;
            }

            // Add the menu item to the database
            await _categoryRepository.AddAsync(menuItem);

            // Add the modifier mappings
            if (model.selectedModifierLists != null && model.selectedModifierLists.Any())
            {
                foreach (var modifier in model.selectedModifierLists)
                {
                    await _categoryRepository.AddMenuItemModifierAsync(new MappingMenuItemWithModifier
                    {
                        MenuItemId = menuItem.Id, // Use the ID generated after adding the menuItem
                        ModifierGroupId = modifier.ModifierGroupId,
                        MinSelectionRequired = modifier.MinValue,
                        MaxSelectionAllowed = modifier.MaxValue
                    });
                }
            }

            return menuItem;
        }


        public async Task<bool> BulkSoftDeleteItems(List<int> itemIds)
        {
            return await _categoryRepository.BulkSoftDeleteItems(itemIds);
        }




        public async Task UpdateMenuItemAsync(int id, Menuitem menuItem)
        {
            await _categoryRepository.UpdateMenuItemAsync(id, menuItem);
        }

        public async Task UpdateMenuItemAsync(MenuItemViewModel model)
        {
            await _categoryRepository.UpdateMenuItemAsync(model);
        }

        public async Task<bool> SoftDeleteItemAsync(int id)
        {
            return await _categoryRepository.SoftDeleteItemAsync(id);
        }

        public async Task<bool> AddModifierGroupAsync(ModifierGroupViewModel model)
        {
            var utcNow = DateTime.UtcNow;
            var modifierGroup = new Modifiergroup
            {
                Name = model.Name,
                Description = model.Description,
                CreatedAt = DateTime.SpecifyKind(utcNow, DateTimeKind.Unspecified), // Set to Unspecified
                UpdatedAt = DateTime.SpecifyKind(utcNow, DateTimeKind.Unspecified) // Set to Unspecified
            };

            return await _categoryRepository.AddAsync(modifierGroup);
        }

        public async Task<bool> AddModifierAsync(ModifiersViewModel model)
        {
            return await _categoryRepository.AddModifierAsync(model);
        }
    }
}

