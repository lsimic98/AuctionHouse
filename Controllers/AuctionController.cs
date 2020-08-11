using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionHouse.Models.Database;
using AuctionHouse.Models.View;
using AuctionHouse.QuartzJobs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateAuction()
        {
            return View();
        }


        

        
    }

  
}