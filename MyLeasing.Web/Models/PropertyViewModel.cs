namespace MyLeasing.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Data.Entities;
    public class PropertyViewModel : Property
    {
        public int OwnerId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Tipo de Propiedad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de propiedad.")]
        public int PropertyTypeId { get; set; }

        public IEnumerable<SelectListItem> PropertyTypes { get; set; }
    }
}
