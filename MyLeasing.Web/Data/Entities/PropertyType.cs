namespace MyLeasing.Web.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class PropertyType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de Propiedad*")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede ser mayor a {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Name { get; set; }

        public ICollection<Property> Properties { get; set; }
    }
}
