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

    public class UserController : Controller{

        private AuctionHouseContext context;
        private UserManager<User> userManager;
        private IMapper mapper;
        private SignInManager<User> signInManager;
        private ISchedulerFactory schedulerFactory;
        private IScheduler scheduler;
        private bool schedulerStarted = false;

        
    

        public UserController(AuctionHouseContext context, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, ISchedulerFactory schedulerFactory)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
            this.signInManager = signInManager;
            this.schedulerFactory = schedulerFactory;
          
        
        }

        

        public IActionResult Register(){
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register (RegisterModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            /*
                S obzirom da koristimo Identity biblioteku, ona vec u sebi ima mehanizam za dodavanje novog korsnika. Postoji klasa UserManager koja to radi,.
            */
            User user = this.mapper.Map<User>(model);
            IdentityResult result = await this.userManager.CreateAsync(user, model.password);
            if(!result.Succeeded)
            {
                foreach(IdentityError error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                
                return View(model);
            }

            result = await this.userManager.AddToRoleAsync(user, Roles.user.Name);
            if(!result.Succeeded)
            {
                foreach(IdentityError error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                
                return View(model);
            }

            return RedirectToAction(nameof(UserController.LogIn), "User");

            
        }


        
        public IActionResult LogIn(string returnUrl)
        {
            LogInModel model = new LogInModel()
            {
                returnUrl = returnUrl
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInModel model)
        {
            if(!ModelState.IsValid)
                return View(model);

            User user = this.context.Users.Where(u => u.UserName == model.username).FirstOrDefault();
            bool canSignIn = await this.signInManager.CanSignInAsync(user);

            if(canSignIn && user.state.Equals("Active"))
            {
                await this.signInManager.PasswordSignInAsync(model.username, model.password, false, false);
                if(model.returnUrl != null)
                    return Redirect(model.returnUrl);
                else
                    return RedirectToAction(nameof(AuctionController.Index), "Auction");               
            }
            else if(!canSignIn)
            {
                ModelState.AddModelError("", "Username or password not valid!");
                return View(model);
            }
            else
            {
                ModelState.AddModelError("", "Your Account Has Been Temporarily Suspended!");
                return View(model);                
            }

        }


        

       
      


       
      

        public IActionResult isEmailUnique(string email) 
        {
            /*
                Ova meto da se poziva tako sto se salje zahtev serveru tj. GET zahetv. Sto znaci da se parametri prosledjuju korz adresu, tako sto
                se navodi ImePolja jednako VrednostPOlja. Sto znaci da metoda koja vrsi proveru mora da ima parametar koji se zove isto kao i polje
                u Modelu da bi mogo de se izvrsi povezivanje GET parametara sa tim.
            */

            bool exists = this.context.Users.Where(user=>user.Email == email).Any();

            if(exists)
                return Json("Email already taken!");
            else
                return Json(true);

        }

        public IActionResult isUsernameUnique(string username) 
        {
            /*
                Ova meto da se poziva tako sto se salje zahtev serveru tj. GET zahetv. Sto znaci da se parametri prosledjuju korz adresu, tako sto
                se navodi ImePolja jednako VrednostPOlja. Sto znaci da metoda koja vrsi proveru mora da ima parametar koji se zove isto kao i polje
                u Modelu da bi mogo de se izvrsi povezivanje GET parametara sa tim.
            */

            bool exists = this.context.Users.Where(user=>user.UserName == username).Any();

            if(exists)
                return Json("Username already taken!");
            else
                return Json(true);

        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
      
        }
        
        [Authorize]
        public async Task<IActionResult> LogOutPasswordChange()
        {
            await this.signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }


      



        [Authorize]
        public async Task<IActionResult> UserInfo()
        {
            User loggedInUser = await this.userManager.GetUserAsync(base.User);
            return View(loggedInUser);
        }


        [Authorize]
        public async Task<IActionResult> EditUserInfo()
        {
            User loggedInUser = await this.userManager.GetUserAsync(base.User);
            EditUserModel model = new EditUserModel()
            {
                firstName = loggedInUser.firstName,
                lastName = loggedInUser.lastName,
                gender = loggedInUser.gender,
                email = loggedInUser.Email,
                newPassword = "",
                oldPassword =""
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserInfo(EditUserModel model)
        {

            if(!ModelState.IsValid)
            {
                return View(model);
            }

            User loggedInUser = await this.userManager.GetUserAsync(base.User);

            if(model.newPassword==null && model.oldPassword==null)
            {
                
                loggedInUser.firstName = model.firstName;
                loggedInUser.lastName = model.lastName;
                loggedInUser.gender = model.gender;
                
                if(model.email!=loggedInUser.Email)
                {
                    bool exists = this.context.Users.Where(user=>user.Email == model.email).Any();
                    if(exists)
                    {
                        ModelState.AddModelError("", "Email already exists");
                        return View(model);
                    }
                    loggedInUser.Email = model.email;
                    loggedInUser.NormalizedEmail = model.email.ToUpper();
                    
                }

                await this.userManager.UpdateAsync(loggedInUser);
                await this.signInManager.RefreshSignInAsync(loggedInUser);
                return RedirectToAction(nameof(UserController.UserInfo), "User");
            }
            else     
            {
                PasswordVerificationResult result  = this.userManager.PasswordHasher.VerifyHashedPassword(loggedInUser, loggedInUser.PasswordHash, model.oldPassword);
                if(result == PasswordVerificationResult.Failed)
                {
                    ModelState.AddModelError("", "Old password incorrect!");
                    return View(model);
                }
                if(model.newPassword==null)
                {
                    ModelState.AddModelError("", "You must enter new password!");
                    return View(model);
                }

                loggedInUser.firstName = model.firstName;
                loggedInUser.lastName = model.lastName;
                loggedInUser.gender = model.gender;
                
                if(model.email!=loggedInUser.Email)
                {
                    bool exists = this.context.Users.Where(user=>user.Email == model.email).Any();
                    if(exists)
                    {
                        ModelState.AddModelError("", "Email already exists");
                        return View(model);
                    }
                    loggedInUser.Email = model.email;
                    loggedInUser.NormalizedEmail = model.email.ToUpper();
                    
                }

                await this.userManager.RemovePasswordAsync(loggedInUser);
                await this.userManager.AddPasswordAsync(loggedInUser, model.newPassword);
                await this.userManager.UpdateAsync(loggedInUser);
                return RedirectToAction(nameof(UserController.LogOutPasswordChange), "User");

    
            }
            

         
       
            

          
        }





        public async Task<IActionResult> oldPassword(string password) 
        {
            bool exists = false;

            if(password!="")
            {
                User loggedInUser = await this.userManager.GetUserAsync(base.User);
                PasswordVerificationResult result  = this.userManager.PasswordHasher.VerifyHashedPassword(loggedInUser, loggedInUser.PasswordHash, password);
                if(result != PasswordVerificationResult.Failed)
                    exists = false;
                else
                    exists = true;
            }


            if(exists)
                return Json("Incorrect old password!");
            else
                return Json(true);

        }


      public async Task<IActionResult> TestTask()
      {
          if(!schedulerStarted)
          {
              schedulerStarted = true;
              this.scheduler = await this.schedulerFactory.GetScheduler();
              await this.scheduler.Start();
          }


          JobDataMap map = new JobDataMap();
          map.Add("username", "lsimic98");

          IJobDetail job = JobBuilder.Create<TestJob>()
          .SetJobData(map)
          .Build ();
	

          ITrigger trigger = TriggerBuilder
          .Create()
          .StartAt(DateTimeOffset.Now.AddSeconds(10))
          .Build();

           await this.scheduler.ScheduleJob(job, trigger);
           
           return RedirectToAction(nameof(HomeController.Index), "Home");
    
      }

      [Authorize(Roles="Administrator")]
      public async Task<IActionResult> BanUsers(int? page)
      {

          IList<User> usersList = await this.userManager.GetUsersInRoleAsync("User");
          UsersOverview users = new UsersOverview()
          {
              users = usersList.ToPagedList(page ?? 1,1)
    
          };
          return View(users);

      }

      [Authorize(Roles="Administrator")]
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> BanUser(string username)
      {
          User user = this.context.Users.FirstOrDefault(u => u.UserName==username);
          
          if(user==null)
           return Json(false);

          user.state="Deleted";

          await this.userManager.UpdateAsync(user);

          return PartialView ("UnbanUser", user);

      }
      
      [Authorize(Roles="Administrator")]
      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> UnbanUser(string username)
      {
          User user = this.context.Users.FirstOrDefault(u => u.UserName==username);

          if(user==null)
           return Json(false);

          user.state="Active";

          await this.userManager.UpdateAsync(user);

          return PartialView ("BanUser", user);

      }


















    }
}