using System.ComponentModel.DataAnnotations;
using AuctionHouse.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.Models.View
{
    public class EditUserModel{
        [Required]
        [Display(Name = "First name")]
        public string firstName{get; set;}

        [Required]
        [Display(Name = "Last name")]
        public string lastName{get; set;}

        [Required]
        [Display(Name = "Gender")]
        public char gender{get; set;}

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string email{get; set;}

        [Display(Name = "Old Password")]
        [DataType(DataType.Password)]
        public string oldPassword{get; set;}

        [Display(Name = "New password")]
        [DataType(DataType.Password)]
        public string newPassword{get; set;}

    }
}