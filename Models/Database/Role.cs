using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuctionHouse.Models.Database{
    public static class Roles
    {
         public static IdentityRole administrator = new IdentityRole(){
             Name="Administrator",
             NormalizedName = "ADMINISTRATOR"
         };  

          public static IdentityRole user = new IdentityRole(){
             Name="User",
             NormalizedName = "USER"
         } ;
    }


    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
           builder.HasData
           ( 
               Roles.user,
               Roles.administrator
           );
        }
    }


}