﻿namespace MyLeasing.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;
    using Data.Entities;

    public class PropertyImageViewModel : PropertyImage
    {
        [Display(Name = "Foto")]
        public IFormFile ImageFile { get; set; }
    }
}
