using Application.Services;
using Core.DTOs;
using Core.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace InventoryManagment.UI.Components.Pages
{
    public partial class Home
    {
        protected List<TableDTO> tables = new();
        private ElementReference reservationModalRef;
        private bool modalVisible = false;
        private int selectedTableId;
        protected override async Task OnInitializedAsync()
        {
            tables = await ITableService.GetAllTables();
        }
        private async Task DeleteItem (int id)
        {
            await ITableService.RemoveTable(id);
            await OnInitializedAsync();
            StateHasChanged();
        }
        public async Task GetItems()
        {
            tables = await ITableService.GetAllTables();
            StateHasChanged();
        }
        private void NavigateToOrder(int id)
        {
            NavigationManager.NavigateTo($"Orders/{id}");
        }
        private void ShowReservationModal(int tableId)
        {
            selectedTableId = tableId;
            modalVisible = true;
        }

        // Method to handle modal close
        private async Task HandleCloseModal()
        {
            modalVisible = false;
            tables = await ITableService.GetAllTables();
        }

    }
}
