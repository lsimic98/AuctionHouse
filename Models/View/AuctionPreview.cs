using System;
using System.Collections.Generic;
using AuctionHouse.Models.Database;
using X.PagedList;

namespace AuctionHouse.Models.View
{
    public class AuctionPreviewModel
    {
        public int currentPage {get;set;}
        public int numOfPages {get; set;}
        public IPagedList<Auction> auctions {get; set;}
    }
}