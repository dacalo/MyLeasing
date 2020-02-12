namespace MyLeasing.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Data.Entities;

    public class ContractViewModel : Contract
    {
        public int OwnerId { get; set; }

        public int PropertyId { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Display(Name = "Arrendatario")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un arrendatario.")]
        public int LesseeId { get; set; }

        public IEnumerable<SelectListItem> Lessees { get; set; }
    }
}