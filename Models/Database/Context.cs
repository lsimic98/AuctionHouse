using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.Models.Database{

    public class AuctionHouseContext : IdentityDbContext<User>{


        public DbSet<BagToken> BagTokens {get; set;}
        public DbSet<TokenTransaction> TokenTransactions {get; set;}
        public DbSet<Auction> Auctions {get; set;}
        public DbSet<Bid> Bids {get; set;}      

        public AuctionHouseContext(DbContextOptions options):base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

                builder.Entity<User>()
                .Property(u => u.tokens)
                .HasDefaultValue(0.00);

                builder.Entity<User>()
                .Property(u => u.state)
                .HasDefaultValue("Active");

                builder.Entity<User>()
                .HasMany(u => u.AuctionWinners)
                .WithOne(a => a.winner);

                builder.Entity<User>()
                .HasMany(u => u.AuctionOwners)
                .WithOne(a => a.owner);

            builder.ApplyConfiguration(new IdentityRoleConfiguration());
            builder.ApplyConfiguration(new BagTokenConfiguration());
            builder.ApplyConfiguration(new TokenTransactionConfiguration());           
            builder.ApplyConfiguration(new AuctionConfiguration());
            builder.ApplyConfiguration(new BidConfiguration());
            

            

        }



    }


}