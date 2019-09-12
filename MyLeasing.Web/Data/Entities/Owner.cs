namespace MyLeasing.Web.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Owner
    {
        //Repsents an owner
        public int Id { get; set; }

        public User User { get; set; }
        public ICollection<Property> Properties { get; set; }

        public ICollection<Contract> Contracts { get; set; }

    }
}
