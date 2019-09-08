namespace MyLeasing.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Data.Entities;
    public class PropertyViewModel : Property
    {
        public int OwnerId { get; set; }

        [Required(ErrorMessage = "The field {0} is mandatory.")]
        [Display(Name = "Property Type")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a property type.")]
        public int PropertyTypeId { get; set; }

        public IEnumerable<SelectListItem> PropertyTypes { get; set; }
    }
}
