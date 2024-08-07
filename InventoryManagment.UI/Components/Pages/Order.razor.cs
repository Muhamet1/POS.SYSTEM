using Application.Services;
using Blazored.Toast.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Components;

namespace InventoryManagment.UI.Components.Pages
{
    public partial class Order
    {
        protected List<ItemDTO> items = new();
        protected List<ItemDTO> checkoutItems = new();
        protected List<Category> categories = new();
        private double Total = 0.00;
        private decimal Total_TVSH = 0.00m;
        private int counter = 0;

        [Parameter]
        public int TableId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            items = await IItemService.GetItems();
            categories = await ICategoryService.GetCategories();
        }
        private async Task DeleteItem(int id)
        {
            await IItemService.RemoveItem(id);
            await OnInitializedAsync();
            StateHasChanged();
        }
        public async Task GetItems()
        {
            items = await IItemService.GetItems();
            StateHasChanged();
            NavigationManager.NavigateTo($"Orders/{TableId}");
        }
        public async Task FilterByCategory(int categoryId)
        {
            items = await IItemService.GetItemsByCategoryId(categoryId);
            StateHasChanged();
            NavigationManager.NavigateTo($"Orders/{TableId}");
        }
        public void AddItemToCheckout(ItemDTO item)
        {
            if (checkoutItems.Contains(item))
            {
                item.CounterSameItem++;
            }
            else
            {
                checkoutItems.Add(item);
            }
            Total = 0.0;
            foreach (var total in checkoutItems)
            {
                if (total.CounterSameItem > 1)
                {
                    Total += Math.Round((total.CounterSameItem * total.SellingPrice), 1);
                }
                else if (total.CounterSameItem == 1)
                {
                    Total += Math.Round(total.SellingPrice, 1);
                }
            }
            item.Total_By_Item =Math.Round(item.CounterSameItem * item.SellingPrice,1);
            Total_TVSH = (decimal)Math.Round((Total + (Total * 0.17)), 1);
        }
        public async void DeleteItemFromCheckout(ItemDTO item)
        {
            checkoutItems.Remove(item);
            Total = 0.0;
            foreach (var total in checkoutItems)
            {
                if (total.CounterSameItem > 1)
                {
                    Total += Math.Round((total.CounterSameItem * total.SellingPrice), 1);
                }
                else if (total.CounterSameItem == 1)
                {
                    Total += Math.Round(total.SellingPrice, 1);
                }
            }
            item.CounterSameItem = 1;
            Total_TVSH = (decimal)Math.Round((Total + (Total * 0.17)), 1);
            await OnInitializedAsync();
            StateHasChanged();
        }
        public async void SubmitOrder()
        {
            if (checkoutItems.Count > 0)
            {
                foreach (var item in checkoutItems)
                {
                    await IOrderService.CreateOrder(TableId, item.Id, Total);
                }
                ToastServices.ShowSuccess("Order submitted successfully");
                NavigationManager.NavigateTo("/");
            }
        }
        public void IncrementItem(ItemDTO item)
        {
            item.CounterSameItem++;
            Total = 0.0;
            foreach (var total in checkoutItems)
            {
                if (total.CounterSameItem > 1)
                {
                    Total += Math.Round((total.CounterSameItem * total.SellingPrice),1);
                }
                else if (total.CounterSameItem == 1)
                {
                    Total += Math.Round(total.SellingPrice,1);
                }
            }
            item.Total_By_Item = Math.Round(item.CounterSameItem * item.SellingPrice, 1);
            Total_TVSH = (decimal)Math.Round((Total + (Total * 0.17)), 1);
        }
        public void DecrementItem(ItemDTO item)
        {
            item.CounterSameItem--;
            Total = 0.0;
            foreach (var total in checkoutItems)
            {
                if (total.CounterSameItem > 1)
                {
                    Total += Math.Round((total.CounterSameItem * total.SellingPrice),1);
                }
                else if (total.CounterSameItem == 1)
                {
                    Total += Math.Round(total.SellingPrice,1);
                }
            }
            item.Total_By_Item = Math.Round(item.CounterSameItem * item.SellingPrice, 1);
            Total_TVSH = (decimal)Math.Round((Total + (Total * 0.17)),1);
        }
    }
}
