using Application.Services;
using Core.DTOs;
using Microsoft.AspNetCore.Components;

namespace InventoryManagment.UI.Components.Pages
{
    public partial class OrderDetails
    {
        [Parameter]
        public int Id { get; set; }

        private bool showSuccess = false;
        private OrderDTO? orderDto;

        protected override async Task OnInitializedAsync()
        {
            orderDto = await ITableService.GetOrderByTableId(Id);
        }
        public async void FinishOrder ()
        {
            await ITableService.ChangeTableStatus(Id);
            showSuccess = true;
            StateHasChanged();
            await Task.Delay(3000); 
            NavigationManager.NavigateTo("/");
        }
        public void Cancel()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}
