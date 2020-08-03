using System.Linq;
using System.Threading.Tasks;
using AuctionHouse.Models.Database;
using AuctionHouse.Models.View;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.Controllers{

    public class UserController : Controller{

        private AuctionHouseContext context;
        private UserManager<User> userManager;
        private IMapper mapper;
        private SignInManager<User> signInManager;


        public UserController(AuctionHouseContext context, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
            this.signInManager = signInManager;
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

            return RedirectToAction(nameof(HomeController.Index), "Home");

            
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

            var result = await this.signInManager.PasswordSignInAsync(model.username, model.password, false, false);
            
            

            if(!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password not valid!");
                return View(model);
            }

            if(model.returnUrl != null)
                return Redirect(model.returnUrl);
            else
                return RedirectToAction(nameof(HomeController.Index), "Home");
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



         public IActionResult Tokens()
         {
             return View();
         }






        

    }
}