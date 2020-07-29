

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

        public User user {get; set;}

        public BagToken bag {get; set;}
    }

    public class TokenTransactionConfiguration : IEntityTypeConfiguration<TokenTransaction>
    {
        public void Configure(EntityTypeBuilder<TokenTransaction> builder)
        {
            builder.Property(transaction => transaction.IdTra).ValueGeneratedOnAdd();
        }
    }

}