// using BussinessLogicLayer.Interface;
// using DataAccessLayer.Models;
using PizzaShop.Domain.DataModels;
// using DataAccessLayer.ViewModels;
using PizzaShop.Domain.ViewModels;  
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Repository.Interfaces;

namespace PizzaShopRMS.Controllers;

public class TableAndSectionController : Controller
{
    private readonly ITableAndSectionRepository _tableAndSectionRepository;

    // private readonly ICommonRepository _commonRepository;
    public TableAndSectionController(ITableAndSectionRepository tableAndSectionRepository )
    {
        _tableAndSectionRepository = tableAndSectionRepository;

    }
    public async Task<IActionResult> TableAndSection()
    {
        TableAndSectionViewModel tableAndSection = new TableAndSectionViewModel();
        tableAndSection.sectionList = await _tableAndSectionRepository.GetSectionList();
        return View(tableAndSection);
    }

    public async Task<IActionResult> GetTablesListForSection(Pagination<TableViewModel> tableList, string sectionId)
    {
        try
        {
            var data = await _tableAndSectionRepository.GetTablesListForSection(tableList, sectionId);
            return PartialView("_TableListPartialView", data);
        }
        catch (Exception ex)
        {

            TempData["ToasterMessage"] = "An Unexpected error got occured";
            TempData["ToasterType"] = "error";
            Console.WriteLine($"Error from controller :{ex.Message}");
            return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
        }
    }
    [HttpPost]
    public async Task<IActionResult> AddSection(SectionViewModel AddSection)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                TempData["ToasterMessage"] = "Model state is not valid";
                TempData["ToasterType"] = "error";
                return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
            }
            // var token = Request.Cookies["JwtToken"];
            // var decodedToken = await _commonRepository.ValidateToken(token);
            // var userId = decodedToken.UserId;
            // AddSection.CreatedBy = userId;
            var result = await _tableAndSectionRepository.AddSection(AddSection);
            if (result == "Section Added Successfully")
            {
                TempData["ToasterMessage"] = result;
                TempData["ToasterType"] = "success";
                return Json(new { success = true, redirectUrl = "/TableAndSection/TableAndSection" });
            }
            TempData["ToasterMessage"] = result;
            TempData["ToasterType"] = "error";
            return Json(new { success = true, redirectUrl = "/TableAndSection/TableAndSection" });
        }
        catch (Exception ex)
        {
            TempData["ToasterMessage"] = "An Unexpected error got occured";
            TempData["ToasterType"] = "error";
            Console.WriteLine($"Error from controller :{ex.Message}");
            return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
        }
    }

    public async Task<IActionResult> GetSectionForEdit(string sectionId)
    {
        try
        {
            var section = await _tableAndSectionRepository.GetSectionForEdit(sectionId);
            return Json(section);
        }
        catch (Exception ex)
        {
            TempData["ToasterMessage"] = "An Unexpected error got occured";
            TempData["ToasterType"] = "error";
            Console.WriteLine($"Error from controller :{ex.Message}");
            return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
        }
    }
    [HttpPost]
    public async Task<IActionResult> EditSection(SectionViewModel AddSection)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                TempData["ToasterMessage"] = "Model state is not valid";
                TempData["ToasterType"] = "error";
                return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
            }
            // var token = Request.Cookies["JwtToken"];
            // var decodedToken = await _commonRepository.ValidateToken(token);
            // var userId = decodedToken.UserId;
            // AddSection.EditedBy = userId;
            var result = await _tableAndSectionRepository.EditSection(AddSection);
            switch (result)
            {
                case "Section with the name already exist":
                    TempData["ToasterMessage"] = result;
                    TempData["ToasterType"] = "error";
                    return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
                default:
                    TempData["ToasterMessage"] = result;
                    TempData["ToasterType"] = "success";
                    return Json(new { success = true, redirectUrl = "/TableAndSection/TableAndSection" });
            }
        }
        catch (Exception ex)
        {
            TempData["ToasterMessage"] = "An Unexpected error got occured";
            TempData["ToasterType"] = "error";
            Console.WriteLine($"Error from controller :{ex.Message}");
            return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
        }
    }
    public async Task<IActionResult> DeleteSection(string sectionId)
    {
        try
        {
            var result = await _tableAndSectionRepository.DeleteSection(sectionId);
            if (result == "section Deleted Successfully")
            {
                TempData["ToasterMessage"] = result;
                TempData["ToasterType"] = "success";
                return Json(new { success = true, redirectUrl = "/TableAndSection/TableAndSection" });   
            }
            TempData["ToasterMessage"] = result;
            TempData["ToasterType"] = "error";
            return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
        }
        catch (Exception ex)
        {
            TempData["ToasterMessage"] = "An Unexpected error got occured";
            TempData["ToasterType"] = "error";
            Console.WriteLine($"Error from controller :{ex.Message}");
            return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
        }
    }
    [HttpPost]
    public async Task<IActionResult> AddTable(TableViewModel AddTable){
       try
        {
            if (!ModelState.IsValid)
            {
                TempData["ToasterMessage"] = "Model state is not valid";
                TempData["ToasterType"] = "error";
                return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
            }
            // var token = Request.Cookies["JwtToken"];
            // var decodedToken = await _commonRepository.ValidateToken(token);
            // var userId = decodedToken.UserId;
            // AddTable.CreatedBy = userId;
            var result = await _tableAndSectionRepository.AddTable(AddTable);
            if (result == "Table Added Successfully")
            {
                TempData["ToasterMessage"] = result;
                TempData["ToasterType"] = "success";
                return Json(new { success = true, redirectUrl = "/TableAndSection/TableAndSection" });
            }
            TempData["ToasterMessage"] = result;
            TempData["ToasterType"] = "error";
            return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
        }
        catch (Exception ex)
        {
            TempData["ToasterMessage"] = "An Unexpected error got occured";
            TempData["ToasterType"] = "error";
            Console.WriteLine($"Error from controller :{ex.Message}");
            return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
        } 
    }

    public async Task<IActionResult> GetTableForEdit(string tableId){
        try
        {
            var table = await _tableAndSectionRepository.GetTableForEdit(tableId);
            return Json(table);
        }
        catch (Exception ex)
        {
            TempData["ToasterMessage"] = "An Unexpected error got occured";
            TempData["ToasterType"] = "error";
            Console.WriteLine($"Error from controller :{ex.Message}");
            return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
        }
    }
    [HttpPost]
    public async Task<IActionResult> EditTable(TableViewModel AddTable){
        try
        {
            if (!ModelState.IsValid)
            {
                TempData["ToasterMessage"] = "Model state is not valid";
                TempData["ToasterType"] = "error";
                return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
            }
            // var token = Request.Cookies["JwtToken"];
            // var decodedToken = await _commonRepository.ValidateToken(token);
            // var userId = decodedToken.UserId;
            // AddTable.EditedBy = userId;
            var result = await _tableAndSectionRepository.EditTable(AddTable);
            switch (result)
            {
                case "Table with the name already exist":
                    TempData["ToasterMessage"] = result;
                    TempData["ToasterType"] = "error";
                    return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
                default:
                    TempData["ToasterMessage"] = result;
                    TempData["ToasterType"] = "success";
                    return Json(new { success = true, redirectUrl = "/TableAndSection/TableAndSection" });
            }
        }
        catch (Exception ex)
        {
            TempData["ToasterMessage"] = "An Unexpected error got occured";
            TempData["ToasterType"] = "error";
            Console.WriteLine($"Error from controller :{ex.Message}");
            return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
        }
    }
    [HttpPost]
    public async Task<IActionResult> DeleteTable(List<string> tableIds){
        try
        {
            var result = await _tableAndSectionRepository.DeleteTable(tableIds);
            TempData["ToasterMessage"] = result;
            TempData["ToasterType"] = "success";
            return Json(new { success = true,redirectUrl = "/TableAndSection/TableAndSection" });
        }
        catch (Exception ex)
        {
            TempData["ToasterMessage"] = "An Unexpected error got occured";
            TempData["ToasterType"] = "error";
            Console.WriteLine($"Error from controller :{ex.Message}");
            return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection"});
        }
    }
}
// using Microsoft.AspNetCore.Mvc;
// using PizzaShop.Services.Interfaces;
// using PizzaShop.Domain.ViewModels;
// using PizzaShop.Service.Interface;

// namespace PizzaShopRMS.Controllers
// {
//     public class TableAndSectionController : Controller
//     {
//         private readonly ITableAndSectionService _tableAndSectionService;

//         public TableAndSectionController(ITableAndSectionService tableAndSectionService)
//         {
//             _tableAndSectionService = tableAndSectionService;
//         }

//         public async Task<IActionResult> TableAndSection()
//         {
//             TableAndSectionViewModel tableAndSection = new TableAndSectionViewModel
//             {
//                 sectionList = await _tableAndSectionService.GetSectionList()
//             };
//             return View(tableAndSection);
//         }

//         public async Task<IActionResult> GetTablesListForSection(Pagination<TableViewModel> tableList, string sectionId)
//         {
//             try
//             {
//                 var data = await _tableAndSectionService.GetTablesListForSection(tableList, sectionId);
//                 return PartialView("_TableListPartialView", data);
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Error from controller: {ex.Message}");
//                 TempData["ToasterMessage"] = "An unexpected error occurred";
//                 TempData["ToasterType"] = "error";
//                 return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
//             }
//         }

//         [HttpPost]
//         public async Task<IActionResult> AddSection(SectionViewModel addSection)
//         {
//             try
//             {
//                 if (!ModelState.IsValid)
//                 {
//                     TempData["ToasterMessage"] = "Model state is not valid";
//                     TempData["ToasterType"] = "error";
//                     return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
//                 }

//                 var result = await _tableAndSectionService.AddSection(addSection);
//                 TempData["ToasterMessage"] = result;
//                 TempData["ToasterType"] = result == "Section Added Successfully" ? "success" : "error";
//                 return Json(new { success = true, redirectUrl = "/TableAndSection/TableAndSection" });
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Error from controller: {ex.Message}");
//                 TempData["ToasterMessage"] = "An unexpected error occurred";
//                 TempData["ToasterType"] = "error";
//                 return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
//             }
//         }

//         [HttpPost]
//         public async Task<IActionResult> AddTable(TableViewModel addTable)
//         {
//             try
//             {
//                 if (!ModelState.IsValid)
//                 {
//                     TempData["ToasterMessage"] = "Model state is not valid";
//                     TempData["ToasterType"] = "error";
//                     return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
//                 }

//                 var result = await _tableAndSectionService.AddTable(addTable);
//                 TempData["ToasterMessage"] = result;
//                 TempData["ToasterType"] = result == "Table Added Successfully" ? "success" : "error";
//                 return Json(new { success = true, redirectUrl = "/TableAndSection/TableAndSection" });
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Error from controller: {ex.Message}");
//                 TempData["ToasterMessage"] = "An unexpected error occurred";
//                 TempData["ToasterType"] = "error";
//                 return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
//             }
//         }

//         [HttpPost]
//         public async Task<IActionResult> EditTable(TableViewModel editTable)
//         {
//             try
//             {
//                 if (!ModelState.IsValid)
//                 {
//                     TempData["ToasterMessage"] = "Model state is not valid";
//                     TempData["ToasterType"] = "error";
//                     return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
//                 }

//                 var result = await _tableAndSectionService.EditTable(editTable);
//                 TempData["ToasterMessage"] = result;
//                 TempData["ToasterType"] = result == "Table Updated Successfully" ? "success" : "error";
//                 return Json(new { success = true, redirectUrl = "/TableAndSection/TableAndSection" });
//             }
//             catch (Exception ex)
//             {
//                 Console.WriteLine($"Error from controller: {ex.Message}");
//                 TempData["ToasterMessage"] = "An unexpected error occurred";
//                 TempData["ToasterType"] = "error";
//                 return Json(new { success = false, redirectUrl = "/TableAndSection/TableAndSection" });
//             }
//         }
//     }
// }
