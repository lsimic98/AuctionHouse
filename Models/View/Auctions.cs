using System;
using System.Collections.Generic;
using AuctionHouse.Models.Database;
using X.PagedList;

namespace AuctionHouse.Models.View
{
    public class AuctionsOverview
    {
        public IPagedList<Auction> auctions {get; set;}
    }
}