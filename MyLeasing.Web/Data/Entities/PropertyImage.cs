namespace MyLeasing.Web.Data.Entities
{
    using MyLeasing.Common.Business;
    using System.ComponentModel.DataAnnotations;

    public class PropertyImage
    {
        public int Id { get; set; }

        [Display(Name = "Foto")]
        public string ImageUrl { get; set; }

        public Property Property { get; set; }

        // TODO: Change the path when publish
        public string ImageFullPath => string.IsNullOrEmpty(ImageUrl) ? null : $"{Constants.URL_API}{ImageUrl.Substring(1)}";

    }
}
