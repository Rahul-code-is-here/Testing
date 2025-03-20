using PizzaShop.Domain.ViewModels;

namespace PizzaShop.Service.Interface;

public interface ITableAndSectionService
{
    Task<List<SectionViewModel>> GetSectionList();
    Task<Pagination<TableViewModel>> GetTablesListForSection(Pagination<TableViewModel> tableList, string sectionId);
    Task<string> AddSection(SectionViewModel addSection);
    Task<SectionViewModel> GetSectionForEdit(string sectionId);
    Task<string> EditSection(SectionViewModel addSection);
    Task<string> DeleteSection(string sectionId);
    Task<string> AddTable(TableViewModel addTable);
    Task<TableViewModel> GetTableForEdit(string tableId);
    Task<string> EditTable(TableViewModel editTable);
    Task<string> DeleteTable(List<string> tableIds);
}
