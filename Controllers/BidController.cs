using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AuctionHouse.Models.Database;
using AuctionHouse.Models.View;
using AuctionHouse.QuartzJobs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Quartz;
using X.PagedList;

namespace AuctionHouse.Controllers{


    public class BidController : Controller{

        private AuctionHouseContext context;
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;


        
        public BidController(AuctionHouseContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
          
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceBid(int auctionId, int bidOffer)
        {
            if(auctionId<=0 || bidOffer<=0)
            {
                return  Json(new { success = false, responseText = "Invalid auction or bid offer!" });
            }

            Auction auction = await this.context.Auctions.Include(a => a.winner).Where(a => a.Id==auctionId).FirstOrDefaultAsync();

            if(auction==null)
            {
                return  Json(new { success = false, responseText = "Error, auction does not exist!" });
            }
            else if(!auction.state.Equals("Open"))
            {
                return  Json(new { success = false, responseText = "Sorry, the auction is not open!" });
            }

            int newAuctionPrice = auction.currentPrice + bidOffer;
            User newBidder = await this.userManager.GetUserAsync(base.User);
            User oldBidder = auction.winner;
            if(newBidder == oldBidder)
            {
                return  Json(new { success = false, responseText = "Sorry, you have already placed your bid offer!" });
            }

            if(newBidder.tokens - newAuctionPrice < 0)
            {
                return  Json(new { success = false, responseText = "Sorry, you dont have enought tokens on your account!" });
            }

            auction.currentPrice = auction.currentPrice += bidOffer;
            oldBidder.tokens += auction.currentPrice;
            newBidder.tokens -= newAuctionPrice;
            auction.winner = newBidder;

             
            TimeSpan timeLeft = auction.closeDate - DateTime.Now; //ArgumentOutOfRangeException e
            if(timeLeft.TotalSeconds <= 10)
            {
                auction.createDate.AddSeconds(10);
            }

            this.context.Update(oldBidder);
            this.context.Update(auction);
            bool saved = false;
            while (!saved)
            {
                Thread.Sleep(3000);
                try
                {
                    // Attempt to save changes to the database
                    await this.context.SaveChangesAsync();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is Auction)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                var databaseValue = databaseValues[property];

                                // TODO: decide which value should be written to database
                                // proposedValues[property] = <value to be saved>;
                            }

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(databaseValues);
                            //entry.CurrentValues.SetValues
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }
            }

            this.context.Update(auction.winner);
            await this.context.SaveChangesAsync();

            return Json(new {
                 success = true, 
                 newCurrentPrice = auction.currentPrice, 
                 bidder = auction.winner.UserName, 
                 newCloseTime = auction.closeDate.ToString("yyyy,MM,d,H,m,s")
            });
        }

    }
}