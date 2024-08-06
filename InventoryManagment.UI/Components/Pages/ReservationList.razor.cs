using Core.DTOs;
using Core.Interfaces;
using Core.Models;

namespace InventoryManagment.UI.Components.Pages
{
    public partial class ReservationList
    {
        protected List<Core.Models.Reservation> reservations = new();

        protected override async Task OnInitializedAsync()
        {
            reservations = await IReservationService.GetAllReservations();
        }
        public async void DeleteReservation(int id)
        {
            await IReservationService.DeleteReservation(id);
            reservations = await IReservationService.GetAllReservations();
            StateHasChanged();
        }
    }
}
