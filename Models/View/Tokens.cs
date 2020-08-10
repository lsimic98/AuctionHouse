using System;
using System.Collections.Generic;
using AuctionHouse.Models.Database;
using X.PagedList;

namespace AuctionHouse.Models.View
{
    public class TokensOverview
    {
        public IPagedList<TokenTransaction> transactions {get; set;}
        public int tokens{get; set;}
        public double moneyspent{get; set;}
    }
}