using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionHouse.Models.Database;
using AuctionHouse.Models.View;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionHouse.Controllers
{
    [Authorize]
    public class PaymentController : Controller
    {
        private UserManager<User> userManager;
        private AuctionHouseContext context;

        public PaymentController(AuctionHouseContext context, UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }


         public async Task<IActionResult> Tokens()
         {
             User loggedInUser = await this.userManager.GetUserAsync(base.User);
             int tokens = loggedInUser.tokens;
             return View(tokens);
         }

         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Tokens(string bagName)
         {
             User loggedInUser = await this.userManager.GetUserAsync(base.User);
             int idBag = 0;
             int tokenAmount = 0;
             switch(bagName)
             {
                 case "7.5":
                 {
                     idBag=1;
                     tokenAmount = 5;
                 }
                 break;
                 case "12.5":
                {
                     idBag=2;
                     tokenAmount = 10;
                 }
                 break;
                 case "19.5":
                {
                     idBag=3;
                     tokenAmount = 20;
                 }
                 break;
             }

             TokenTransaction transaction = new TokenTransaction()
             {
                 bagId = idBag,
                 userId = loggedInUser.Id,
                 purchaseDate = DateTime.Now,
             };

             await this.context.TokenTransactions.AddAsync(transaction);
             await this.context.SaveChangesAsync();

             loggedInUser.tokens += tokenAmount;
             await this.userManager.UpdateAsync(loggedInUser);

             return Json(true);
         }    


    }

}


