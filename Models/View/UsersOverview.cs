using System;
using System.Collections.Generic;
using AuctionHouse.Models.Database;
using X.PagedList;

namespace AuctionHouse.Models.View
{
    public class UsersOverview
    {
        public IPagedList<User> users {get; set;}
    }
}