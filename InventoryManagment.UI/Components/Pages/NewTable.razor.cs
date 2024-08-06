using Application.Services;
using Core.Interfaces;
using Core.Models;
using Microsoft.AspNetCore.Components;

namespace InventoryManagment.UI.Components.Pages
{
    public partial class NewTable
    {
        public Table newTable = new Table();
        public async Task AddTable()
        {
            await ITableService.AddTable(newTable);
            NavigationManager.NavigateTo("/");
        }
    }
}
