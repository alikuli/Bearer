using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Bearer.Models
{
    public class Common
    {
        public Common()
        {
            //if (CreatedDate==default(DateTime))
            //    CreatedDate = DateTime.UtcNow;
        }

        [Key]
        public int Id { get; set; }
        


        [Column(TypeName = "DateTime2")]
        [Display(Name = "Created (UTC)")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime? CreatedDate { get; set; }



        [Display(Name = "Created By")]
        [MaxLength(50)]
        public string CreatedUser { get; set; }


        [Column(TypeName = "DateTime2")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Modified (UTC)")]
        public DateTime? ModifiedDate { get; set; }



        [Display(Name = "Modified By")]
        [MaxLength(50)]
        public string ModifiedUser { get; set; }

        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText="Enter Comment Here.")]
        public string Comment { get; set; }
    }

}