using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace AuctionHouse.Models.Database{
    public class User: IdentityUser{

        [Required]
        public string firstName{get; set;}

        [Required]
        public string lastName{get; set;}

        [Required]
        public char sex{get; set;}

        [Required]
        public string state{get; set;}

        [Required]
        public int tokens{get; set;}

        public ICollection<TokenTransaction> TokenTransactionList {get; set;}
        public ICollection<Bid> BidList {get; set;}

        public ICollection<Auction> AuctionWinners {get; set;}
        public ICollection<Auction> AuctionOwners {get; set;}

      
        
    }

}