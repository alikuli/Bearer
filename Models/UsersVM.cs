using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bearer.Models
{
    [NotMapped]
    public class UsersVM
    {
        [Key]

        public int Id { get; set; }
        public string UsersName { get; set; }
    }
}