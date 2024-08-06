using Core.DTOs;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IItemService
    {
        Task<List<ItemDTO>> GetItems();
        Task<bool> AddItem(Item item);
        Task<bool> RemoveItem(int id);
        Task<List<ItemDTO>> GetItemsByCategoryId(int categoryId);
        Task<List<Item>> SearchItems(string searchParam);
    }
}
