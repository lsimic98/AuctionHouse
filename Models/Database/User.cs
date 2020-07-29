using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AuctionHouse.Models.View;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
//using AutoMapper;

namespace AuctionHouse.Models.Database{
    public class User: IdentityUser{

        [Required]
        public string firstName{get; set;}

        [Required]
        public string lastName{get; set;}

        [Required]
        public char gender{get; set;}

        [Required]
        public string state{get; set;}

        [Required]
        public int tokens{get; set;}

        public ICollection<TokenTransaction> TokenTransactionList {get; set;}
        public ICollection<Bid> BidList {get; set;}

        public ICollection<Auction> AuctionWinners {get; set;}
        public ICollection<Auction> AuctionOwners {get; set;}

      
        
    }

    public class UserProfile : Profile  //Mapper klaasa :D
    {
        public UserProfile()
        {

            base.CreateMap<RegisterModel, User>( )
            .ForMember(
                destination => destination.Email,
                options => options.MapFrom(data=>data.email)
            ) 
            .ForMember(
                destination => destination.UserName,
                options => options.MapFrom(data=>data.username)
            )
            .ReverseMap();
        
        }
    }
    

}