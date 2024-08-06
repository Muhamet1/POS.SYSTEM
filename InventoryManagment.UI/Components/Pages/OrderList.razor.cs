using Core.DTOs;
using Core.Interfaces;
using Core.Models;

namespace InventoryManagment.UI.Components.Pages
{
    public partial class OrderList
    {
        protected List<OrderDTO> orders = new();

        protected override async Task OnInitializedAsync()
        {
            orders = await IOrderService.GetAllOrders();
        }
        public async void DeleteOrder (int id)
        {
            await IOrderService.DeleteOrder(id);
            orders = await IOrderService.GetAllOrders();
            StateHasChanged();
        }
    }
}
