using Core.Models;
using Microsoft.AspNetCore.Components;

namespace InventoryManagment.UI.Components.Pages
{
    public partial class NewItem
    {
        public Item newItem  = new Item();
        public async Task AddItem()
        {
            await ItemService.AddItem(newItem);
            NavigationManager.NavigateTo("/");
        }
    }
}
