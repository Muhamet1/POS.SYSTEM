using Core.DTOs;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
namespace Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IApplicationDbContext _context;
        public ItemService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<ItemDTO>> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            var newDto = new List<ItemDTO>();
            foreach (var item in items)
            {
                var dto = new ItemDTO();
                dto.Id = item.Id;
                dto.Name = item.Name;
                dto.Description = item.Description;
                dto.SellingPrice = item.SellingPrice;
                dto.PurchasePrice = item.PurchasePrice;
                dto.Quantity = item.Quantity;
                dto.CategoryId = item.CategoryId;
                dto.Total_By_Item = item.SellingPrice;
                dto.ImageUrl = item.ImageUrl;
                newDto.Add(dto);
            }
            return newDto;
        }
        public async Task<bool> AddItem(Item item)
        {
            var newItem = new Item();
            newItem.Name = item.Name;
            newItem.Description = item.Description;
            newItem.ExpirationDate = item.ExpirationDate;
            newItem.PurchasePrice = item.PurchasePrice;
            newItem.Quantity = item.Quantity;
            newItem.SellingPrice = item.SellingPrice;
            newItem.CategoryId = item.CategoryId;
            newItem.ImageUrl = item.ImageUrl;   
            await _context.Items.AddAsync(newItem);
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> RemoveItem(int id)
        {
            var item = await _context.Items.Where(x => x.Id == id).FirstAsync();
            if (item != null)
            {
                _context.Items.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<List<ItemDTO>> GetItemsByCategoryId(int categoryId)
        {
           var items = await _context.Items.Where(x => x.CategoryId == categoryId).ToListAsync();
            var newDto = new List<ItemDTO>();
            foreach (var item in items)
            {
                var dto = new ItemDTO();
                dto.Id = item.Id;
                dto.Name = item.Name;
                dto.Description = item.Description;
                dto.SellingPrice = item.SellingPrice;
                dto.PurchasePrice = item.PurchasePrice;
                dto.Quantity = item.Quantity;
                dto.CategoryId = item.CategoryId;
                dto.Total_By_Item = item.SellingPrice;
                dto.ImageUrl= item.ImageUrl;
                newDto.Add(dto);
            }
            return newDto;
        }
        public async Task<List<Item>> SearchItems(string searchParam)
        {
            return await _context.Items.Where(x => x.Name.ToLower().Contains(searchParam.ToLower())).ToListAsync();
        }
    }
}
