using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionHouse.Models.Database{
    public class Bid {
        
        [Key]
        public int Id {get; set;}

        public int price {get; set;} //broj ulozenih tokena fakticki
 
        public DateTime bidDate {get; set;}

        public string userId {get; set;}
        public User user {get; set;}

        public int auctionId {get; set;}
        public Auction auction {get; set;}
        
        
 
    }

       public class BidConfiguration : IEntityTypeConfiguration<Bid>
        {
        public void Configure(EntityTypeBuilder<Bid> builder)
        {
            // builder.HasKey(
            //     entity => new {entity.userId, entity.auctionId}  //Kompozitni kljuc :D
            // );
            
            builder.Property(bid => bid.Id).ValueGeneratedOnAdd();

            builder.HasOne<Auction>(item => item.auction)
            .WithMany(item => item.BidList)
            .HasForeignKey(item => new {item.auctionId} );

            builder.HasOne<User>(item => item.user)
            .WithMany(item => item.BidList)
            .HasForeignKey(item => new {item.userId} );
        }
    }
 
}