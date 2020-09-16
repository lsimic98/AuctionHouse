using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionHouse.Models.Database{
    public class Auction {
 
        [Key]
        public int Id{get; set;}
 
        [Required]
        public string name{get; set;}
 
        [Required]
        public string description{get; set;}
 
        [Required]
        public byte[] image{get; set;} //ovo je za sliku, slika se cuva kao niz bajtova
        
        [Required]
        public int startPrice{get; set;}

        [Required]
        public int currentPrice{get; set;}
 
        [Required]
        public DateTime createDate{get; set;}
 
        [Required]
        public DateTime openDate{get; set;}
 
        [Required]
        public DateTime closeDate{get; set;}
 
        [Required]
        public string state{get; set;} //Draft, Ready, Open, Sold, Expired i Deleted

    
        [Timestamp]
        public byte[] RowVersion { get; set; }


        public User winner {get; set;} 
        public User owner {get; set;}

        public ICollection<Bid> BidList{get; set;}


    }


    public class AuctionConfiguration : IEntityTypeConfiguration<Auction>
    {
        public void Configure(EntityTypeBuilder<Auction> builder)
        {
            builder.Property(auction => auction.Id).ValueGeneratedOnAdd();
            
            
        }
    }
}