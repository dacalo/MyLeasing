﻿namespace MyLeasing.Web.Data.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public class Lessee
    {
        //Represents an lessee FunctionalityB
        public int Id { get; set; }

        public User User { get; set; }

        public ICollection<Contract> Contracts { get; set; }

    }
}
