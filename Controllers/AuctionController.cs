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
using Microsoft.EntityFrameworkCore.Metadata;
using Quartz;
using X.PagedList;

namespace AuctionHouse.Controllers{

    public class AuctionController : Controller{

        private AuctionHouseContext context;
        private UserManager<User> userManager;
        //private IMapper mapper;
        private SignInManager<User> signInManager;
        private ISchedulerFactory schedulerFactory;
        private IScheduler scheduler;
        private bool schedulerStarted = false;

        
    

        public AuctionController(AuctionHouseContext context, UserManager<User> userManager, SignInManager<User> signInManager, /*IMapper mapper, */ ISchedulerFactory schedulerFactory)
        {
            this.context = context;
            this.userManager = userManager;
            //this.mapper = mapper;
            this.signInManager = signInManager;
            this.schedulerFactory = schedulerFactory;
          
        }




        [Authorize]
        public IActionResult CreateAuction()
        {
            DateTime date = DateTime.Now;

            string hours = date.Hour > 9 ? date.Hour.ToString() : date.Hour.ToString().PadLeft(2,'0');
            string minutes = date.Minute>9 ? date.Minute.ToString() : date.Minute.ToString().PadLeft(2,'0');
            string seconds = date.Second>9 ? date.Second.ToString() : date.Second.ToString().PadLeft(2,'0');
            string time = hours + ":" + minutes + ":" + seconds;

            CreateAuctionModel model = new CreateAuctionModel()
            {
                openDate = date,
                openTime = time,
                closeDate = date.AddDays(1),
                closeTime = time,
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAuction(CreateAuctionModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            string[] arr1 = model.openTime.Split(':',3);
            string[] arr2 = model.closeTime.Split(':',3);

            TimeSpan ts1 = new TimeSpan(Convert.ToInt32(arr1[0]),  Convert.ToInt32(arr1[1]), Convert.ToInt32(arr1[2]));
            TimeSpan ts2 = new TimeSpan(Convert.ToInt32(arr2[0]),  Convert.ToInt32(arr2[1]), Convert.ToInt32(arr2[2]));

            model.openDate = model.openDate.Date + ts1;
            model.closeDate = model.closeDate.Date + ts2; 

            if(DateTime.Compare(DateTime.Now, model.openDate)>0)
            {
                ModelState.AddModelError("", "Invalid open date! (minimal time is +5minuts form now)");
                return View(model);
            }
            else if(DateTime.Compare(model.openDate, model.closeDate)>0)
            {
                ModelState.AddModelError("", "Close date must be greater than open date!");
                return View(model);               

            }

            User user = await this.userManager.GetUserAsync(base.User);

            using ( BinaryReader reader = new BinaryReader ( model.file.OpenReadStream ( ) ) ) {
                Auction auction = new Auction ( ) {
                    name = model.name,
                    description = model.description,
                    startPrice = model.startPrice,
                    currentPrice = model.startPrice,
                    createDate = DateTime.Now,
                    openDate = model.openDate,
                    closeDate = model.closeDate,
                    state = "Draft",
                    winner = null,
                    owner = user,
                    image = reader.ReadBytes ( Convert.ToInt32 ( reader.BaseStream.Length ) )

                };

                await this.context.Auctions.AddAsync ( auction );
                await this.context.SaveChangesAsync ( );
                await this.AuctionOpenTask(auction.Id, auction.openDate);
            }



            return RedirectToAction(nameof(AuctionController.Index), "Auction");   

        }


        public IActionResult AuctionErrors()
        {   
            return View();
        }
        
        [Authorize]
        public async Task<IActionResult> Auctions(int? page)
        {
             User loggedInUser = await this.userManager.GetUserAsync(base.User);
             IList<Auction> list = await this.context.Auctions.Where(a => a.owner.Id == loggedInUser.Id).OrderByDescending(a => a.createDate).ToListAsync();
             AuctionsOverview auctions = new AuctionsOverview()
             {
                 auctions = list.ToPagedList(page ?? 1,5)
             };
             return View(auctions);

        }
        
        
        [Authorize]
        public async Task<IActionResult> EditAuction(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Auction auction = await this.context.Auctions.Include(a => a.owner).Where(a => a.Id == id).FirstOrDefaultAsync();
            User loggedInUser = await this.userManager.GetUserAsync(base.User);
                
            if (auction == null || (auction.owner.Id != loggedInUser.Id)) 
            {
                return NotFound();
            }



            EditAuctionModel model = new EditAuctionModel()
            {
                id = auction.Id,
                name = auction.name,
                description = auction.description,
                startPrice = auction.startPrice,
                openDate = auction.openDate,
                openTime = auction.openDate.ToString("HH:mm:ss"),
                closeDate = auction.closeDate,
                closeTime = auction.closeDate.ToString("HH:mm:ss"),
            };
            return View(model);

       
            
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAuction(EditAuctionModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            string[] arr1 = model.openTime.Split(':',3);
            string[] arr2 = model.closeTime.Split(':',3);

            TimeSpan ts1 = new TimeSpan(Convert.ToInt32(arr1[0]),  Convert.ToInt32(arr1[1]), Convert.ToInt32(arr1[2]));
            TimeSpan ts2 = new TimeSpan(Convert.ToInt32(arr2[0]),  Convert.ToInt32(arr2[1]), Convert.ToInt32(arr2[2]));

            model.openDate = model.openDate.Date + ts1;
            model.closeDate = model.closeDate.Date + ts2; 

            if(DateTime.Compare(DateTime.Now, model.openDate)>0)
            {
                ModelState.AddModelError("", "Invalid open date! (minimal time is +5minuts form now)");
                return View(model);
            }
            else if(DateTime.Compare(model.openDate, model.closeDate)>0)
            {
                ModelState.AddModelError("", "Close date must be greater than open date!");
                return View(model);               

            }

            Auction auction = await this.context.Auctions.Where(a => a.Id == model.id).FirstOrDefaultAsync();

            auction.name = model.name;
            auction.description = model.description;
            auction.startPrice = model.startPrice;
            auction.currentPrice = model.startPrice;
            auction.createDate = DateTime.Now;
            auction.openDate = model.openDate;
            auction.closeDate = model.closeDate;


            if(model.file==null)
            {
                try
                {
                    this.context.Update(auction);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
               
                }

            }
            else
                using (BinaryReader reader = new BinaryReader (model.file.OpenReadStream ( ))  ) {


                    auction.image = reader.ReadBytes ( Convert.ToInt32 ( reader.BaseStream.Length ) );

                    try
                    {
                        this.context.Update(auction);
                        await this.context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                
                    }

                }



        
            return RedirectToAction(nameof(AuctionController.Index), "Auction");   

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAuction(int id)
        {

            Auction auction = await this.context.Auctions.Include(a => a.owner).Where(a => a.Id == id).FirstOrDefaultAsync();
            User loggedInUser = await this.userManager.GetUserAsync(base.User);
                
            if (auction == null || (auction.owner.Id != loggedInUser.Id) || (!auction.state.Equals("Draft")) || (!auction.state.Equals("Expired"))) 
            {
                return Json(false);
            }

            try
            {
                auction.state="Deleted";
                this.context.Update(auction);
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return Json(true);

        }



      private async Task AuctionOpenTask(int auctionId, DateTime openDate)
      {
          if(!schedulerStarted)
          {
              schedulerStarted = true;
              this.scheduler = await this.schedulerFactory.GetScheduler();
              await this.scheduler.Start();
          }


          JobDataMap map = new JobDataMap();
          map.Add("AuctionId", auctionId);

          IJobDetail job = JobBuilder.Create<AuctionOpenJob>()
          .SetJobData(map)
          .Build ();
	

          ITrigger trigger = TriggerBuilder
          .Create()
          .StartAt(new DateTimeOffset(openDate))
          .Build();

           await this.scheduler.ScheduleJob(job, trigger);
           
      }
      
      
        [Authorize(Roles="Administrator")]
        public async Task<IActionResult> AuctionValidation(int? page)
        {
            User loggedInUser = await this.userManager.GetUserAsync(base.User);
                IList<Auction> list = await this.context.Auctions.OrderByDescending(a => a.createDate).ToListAsync();
                AuctionsOverview auctions = new AuctionsOverview()
                {
                    auctions = list.ToPagedList(page ?? 1,5)
                };
                return View(auctions);
        }

        [Authorize(Roles="Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ValidateAuction(int id)
        {

            Auction auction = await this.context.Auctions.Where(a => a.Id == id).FirstOrDefaultAsync();
                
            if (auction == null || (!auction.state.Equals("Draft"))) 
            {
                return Json(false);
            }

            try
            {
                auction.state="Ready";
                this.context.Update(auction);
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return Json(true);

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAuctionAdmin(int id)
        {

            Auction auction = await this.context.Auctions.Where(a => a.Id == id).FirstOrDefaultAsync();
        
            if (auction == null || (!auction.state.Equals("Draft"))) 
            {
                return Json(false);
            }

            try
            {
                auction.state="Deleted";
                this.context.Update(auction);
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return Json(true);

        }

        //Auctions show

        public async Task<IActionResult> Index()
        {

                IList<Auction> list = await this.context.Auctions.Include(a => a.winner).OrderByDescending(a => a.createDate).ToListAsync();
                // int numOfPages = await this.context.Auctions.CountAsync();
                int numOfPages = list.Count;
                AuctionPreviewModel auctions = new AuctionPreviewModel()
                {
                    currentPage = 1,
                    numOfPages = (int)Math.Ceiling(numOfPages/12.0),
                    auctions = list.ToPagedList(1,12)
                };
                return View(auctions);
        }

        [HttpPost]
        public async Task<IActionResult> JumpToPage(int pageNumber, string search, int? minPrice, int? maxPrice, string state)
        {
            IQueryable<Auction> queryBuilder = this.context.Auctions.Include(a => a.winner);
            if(search!=null)
            {
                queryBuilder = queryBuilder.Include(a => a.winner).Where(a => a.name.Contains(search) || a.description.Contains(search));
            }
            if(minPrice!=null && minPrice>=0)
            {
                queryBuilder = queryBuilder.Where(a => a.currentPrice >= minPrice);
            }
            if(maxPrice!=null && maxPrice>0)
            {
                queryBuilder = queryBuilder.Where(a => a.currentPrice <= maxPrice);
            }
            if(state!=null)
            {
                queryBuilder = queryBuilder.Where(a => a.state==state);
            }
            IList<Auction> list = await queryBuilder.OrderByDescending(a => a.createDate).ToListAsync();
            int numOfPages = list.Count;
            Console.WriteLine(numOfPages);
            AuctionPreviewModel auctions = new AuctionPreviewModel()
            {
                currentPage = pageNumber,
                numOfPages = (int)Math.Ceiling(numOfPages/12.0),
                auctions = list.ToPagedList(pageNumber,12)
            };
            return PartialView ("AuctionPage", auctions);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceBid(int auctionId, int bidOffer)
        {
            Console.WriteLine("AucrionId:"+auctionId +" BidOffer:"+bidOffer);
            if(auctionId<=0 || bidOffer<=0)
            {
                return  Json(new { success = false, responseText = "Invalid auction or bid offer!" });
            }




            


            User newBidder = await this.userManager.GetUserAsync(base.User);

            // Console.WriteLine("Time to sleep! " + newBidder.UserName);
            // Thread.Sleep(10000);


            Auction auction = this.context.Auctions.Include(a => a.winner).Where(a => a.Id==auctionId).FirstOrDefault();

            Console.WriteLine("Time to sleep! " + newBidder.UserName);
            Thread.Sleep(10000);

            if(auction==null)
            {
                return  Json(new { success = false, responseText = "Error, auction does not exist!" });
            }
            else if(!auction.state.Equals("Open"))
            {
                return  Json(new { success = false, responseText = "Sorry, the auction is not open!" });
            }

            int newAuctionPrice = auction.currentPrice + bidOffer;

            byte[] RowVersion = auction.RowVersion;

            User oldBidder = auction.winner;
            if(newBidder == oldBidder)
            {
                return  Json(new { success = false, responseText = "Sorry, you have already placed your bid offer!" });
            }

            if(newBidder.tokens - newAuctionPrice < 0)
            {
                return  Json(new { success = false, responseText = "Sorry, you dont have enought tokens on your account!" });
            }

            auction.currentPrice = auction.currentPrice + bidOffer;
       
            // newBidder.tokens -= newAuctionPrice;
            auction.winner = newBidder;

             
            TimeSpan timeLeft = auction.closeDate - DateTime.Now; //ArgumentOutOfRangeException e
            if(timeLeft.TotalSeconds <= 10)
            {
                auction.createDate.AddSeconds(10);
            }


  
            bool saved = false;
            while (!saved)
            {

                try
                {
                    // Attempt to save changes to the database
                    // if(oldBidder!=null)
                    // {
                    //     oldBidder.tokens = oldBidder.tokens + auction.currentPrice - bidOffer;
                    //     this.context.Update(oldBidder);
                    // }
                    this.context.Update(auction);
                    // this.context.Update(newBidder);     
                    this.context.SaveChanges();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {

                    Console.WriteLine("Puce izuzetak!");
                    Console.WriteLine(ex.ToString());

                    return Json(new { success = false, responseText = "Sorry, someone was faster than you, hehe !!!" });

                    // foreach (var entry in ex.Entries)
                    // {
                    //     if (entry.Entity is Auction)
                    //     {
                    //         var proposedValues = entry.CurrentValues;
                    //         var databaseValues = entry.GetDatabaseValues();

                    //         // Console.WriteLine("var proposedValues = entry.CurrentValues;\n\t"+proposedValues.ToObject().ToString());
                    //         // Console.WriteLine("var databaseValues = entry.GetDatabaseValues();\n\t"+databaseValues.ToObject().ToString());  


                    //         int proposedValue_currentPrice = 0;
                    //         int databaseValue_currentPrice = 0;

                    //         string proposedValue_winnerId = "";
                    //         string databaseValue_winnerId = "";

                    //         IProperty currentPriceProperty = null;
                    //         IProperty winnerIdProperty = null;


                    //         foreach (var property in proposedValues.Properties)
                    //         {
                    //             if(property.Name.Equals("currentPrice"))
                    //             {
                    //                 proposedValue_currentPrice = Convert.ToInt32(proposedValues[property]);
                    //                 databaseValue_currentPrice = Convert.ToInt32(databaseValues[property]);
                    //                 currentPriceProperty = property;
                    
                    //             }
                    //             if(property.Name.Equals("winnerId"))
                    //             {
                    //                 proposedValue_winnerId = proposedValues[property].ToString();
                    //                 databaseValue_winnerId = databaseValues[property].ToString();
                    //                 winnerIdProperty = property;
                    
                    //             }

                    //             // Console.WriteLine("DatabaseValues\n\t" + property.Name + " " + databaseValues[property]);
                    //             // Console.WriteLine("ProposedValues\n\t" + property.Name + " " + proposedValues[property]);

                                

                    //             // TODO: decide which value should be written to database
                    //             // proposedValues[property] = <value to be saved>;
                    //         }

                    //         if(currentPriceProperty!=null && winnerIdProperty!=null && proposedValue_currentPrice > databaseValue_currentPrice)
                    //         {
                    //             proposedValues[currentPriceProperty] = proposedValue_currentPrice;
                    //             proposedValues[winnerIdProperty] = proposedValue_winnerId;
                    //         }
                    //         else if(currentPriceProperty!=null && winnerIdProperty!=null)
                    //         {
                    //             proposedValues[currentPriceProperty] = databaseValue_currentPrice;
                    //             proposedValues[winnerIdProperty] = databaseValue_winnerId;
                    //         }

                

                    //         // Refresh original values to bypass next concurrency check
                    //         entry.OriginalValues.SetValues(databaseValues);
                    //         //entry.CurrentValues.SetValues(proposedValues);
                    //     }
                    //     else
                    //     {
                    //         throw new NotSupportedException(
                    //             "Don't know how to handle concurrency conflicts for "
                    //             + entry.Metadata.Name);
                    //     }
                    // }
                }
            }

            newBidder.tokens -= newAuctionPrice;
            if(oldBidder!=null)
            {
                oldBidder.tokens += newAuctionPrice - bidOffer;
                this.context.Update(oldBidder);
            }
            this.context.Update(newBidder);


            Bid bid = new Bid()
            {
                auctionId = auction.Id,
                auction = auction,
                userId = newBidder.Id,
                user = newBidder,
                bidDate = DateTime.Now,
                price = newAuctionPrice
            };
            await this.context.AddAsync(bid);

            await this.context.SaveChangesAsync();

            return Json(new {
            success = true,
            auctionId = auction.Id, 
            newCurrentPrice = auction.currentPrice, 
            bidder = auction.winner.UserName, 
            newCloseTime = auction.closeDate.ToString("yyyy,MM,d,H,m,s")
        });



            
            // this.context.Update(auction.winner);
            // await this.context.SaveChangesAsync();

            // if(auction!=null && auction.winner!=null && auction.winner==newBidder)
            // {
            //     newBidder.tokens -= newAuctionPrice;
            //         if(oldBidder!=null)
            //         {
            //             oldBidder.tokens += newAuctionPrice - bidOffer;
            //             this.context.Update(oldBidder);
            //         }
            //             this.context.Update(newBidder);
            //             await this.context.SaveChangesAsync();

            //             return Json(new {
            //             success = true, 
            //             newCurrentPrice = auction.currentPrice, 
            //             bidder = auction.winner.UserName, 
            //             newCloseTime = auction.closeDate.ToString("yyyy,MM,d,H,m,s")
            //         });
            
            // }
            // else
            // {
            //    return Json(false);
            // }


            // try
            // {
            // if(auction.winner==null)
            //     Console.WriteLine("Puko izuzetak NULL!");

            
            //     return Json(new {
            //         success = true, 
            //         newCurrentPrice = auction.currentPrice, 
            //         bidder = auction.winner.UserName, 
            //         newCloseTime = auction.closeDate.ToString("yyyy,MM,d,H,m,s")
            //     });

            // }
            // catch(Exception e)
            // {
            //     Console.WriteLine("E ovde me zeza!");
            //     Console.WriteLine(e.ToString());

            //     return Json(new {
            //         success = false, 
            //         newCurrentPrice = auction.currentPrice, 
            //         bidder = auction.winner.UserName, 
            //         newCloseTime = auction.closeDate.ToString("yyyy,MM,d,H,m,s")
            //     });
            // }
        
            
    

        }

        public async Task<IActionResult> AuctionDetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            Auction auction = await this.context.Auctions.Include(a => a.owner).Include(a => a.winner)
            .Where(a => a.Id == id).FirstOrDefaultAsync();

                
            if (auction == null) 
            {
                return NotFound();
            }
            else
                return View(auction);


        }



        




        

       

       



   
    }
}