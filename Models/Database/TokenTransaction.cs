

using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionHouse.Models.Database
{
    public class TokenTransaction
    {
        [Key]
        public int IdTra {get; set;}

        [Required]
        public DateTime purchaseDate {get; set;}

        public string userId {get; set;}
        public int bagId {get; set;}
        public BagToken bag {get; set;}
    }

    public class TokenTransactionConfiguration : IEntityTypeConfiguration<TokenTransaction>
    {
        public void Configure(EntityTypeBuilder<TokenTransaction> builder)
        {
            builder.Property(transaction => transaction.IdTra).ValueGeneratedOnAdd();

            builder.HasOne<BagToken>(item => item.bag)
            .WithMany(item => item.TokenTransactionList)
            .HasForeignKey(item => new {item.bagId} );
        }
    }

}