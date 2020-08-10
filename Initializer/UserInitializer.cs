using AuctionHouse.Models.Database;
using Microsoft.AspNetCore.Identity;

namespace AuctionHouse.Models.Initializers {
    public class UserInitializer {

        private static string[] administrator = new string [] {
            "Admin","Admin","admin","admin@microsoft.com","Admin12345"
        };

        private static bool addUser ( string[] row, UserManager<User> userManager, string role ) {
            string firstName = row[0];
            string lastName = row[1];
            string username = row[2];
            string email = row[3];
            string password = row[4];
           

            //Ne mozemo da koristimo await operator jer ovo pozivamo u Configure metodi(svaki put kad se pokrene aplikacija), a u njoj ne smemo nista da dodajemo i da menjamo, tako da ona mora da bude sinhrona. 
            if ( userManager.FindByNameAsync ( username ).Result != null ) { //Nit koja je ovo pozvala se zablokira dok ne zavrsi, tako smo dobili sinhronu implementaciju metode. 
                return false;
            }

            User user = new User ( ) {
                firstName = firstName,
                lastName = lastName,
                UserName = username,
                Email = email,
                gender = 'M',
                tokens = 999,
                state = "Active"
            };

            IdentityResult result = userManager.CreateAsync ( user, password ).Result; 
            if ( !result.Succeeded ) {
                return false;
            }

            result = userManager.AddToRoleAsync ( user, role ).Result;  

            if ( !result.Succeeded ) {
                return false;
            }

            return true;
        }

        public static void initialize  ( UserManager<User> userManager ) {
            addUser ( UserInitializer.administrator, userManager, Roles.administrator.Name );
        }
    }
}