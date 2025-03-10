using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace AuctionHouse.Hubs
{
    public class AuctionHub:Hub
    {
        public async Task NewBidOffered(int auctionId, int newCurrentPrice, string winnerUsername, string newCloseDate)
        {
            await base.Clients.All.SendAsync("updateAuction", auctionId, newCurrentPrice, winnerUsername, newCloseDate);

        }
        public async Task AuctionClosed(int auctionId, string newState)
        {
            await base.Clients.All.SendAsync("closeAuction", auctionId, newState);

        }
    }

}