using System;
using System.ComponentModel.DataAnnotations;
using AuctionHouse.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuctionHouse.Models.View
{
    public class EditAuctionModel{
        [Required]
        [MinLength(10)]
        [MaxLength(40)]
        [Display(Name = "Auction name (Max 40 characters)")]
        public string name{get; set;}

        [Required]
        [MinLength(10)]
        [MaxLength(200)]
        [Display(Name = "Description (Max 200 characters)")]
        public string description{get; set;}

        [Required]
        [Display(Name = "Start price")]
        public int startPrice{get; set;}

        [Required]
        [Display(Name = "Open date")]
        public DateTime openDate{get; set;}

        [Required]
        [Display(Name = "Open time")]
        public string openTime{get; set;}
        
        [Required]
        [Display(Name = "Close date")]
        public DateTime closeDate{get; set;}

        [Required]
        [Display(Name = "Close time")]
        public string closeTime{get; set;}

        [Display (Name = "Image")]
        public IFormFile file { get; set; }

        [Required]
        public int id {get; set;}

        
    }
}