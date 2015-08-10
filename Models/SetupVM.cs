using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bearer.Models
{
    [NotMapped]
    public class SetupVM
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }
        
        public string Value { get; set; }

        public string ApplicationValue { get; set; }

        public string Name { get; set; }
    }
}