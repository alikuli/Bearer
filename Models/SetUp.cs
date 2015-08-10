using System.ComponentModel.DataAnnotations;

namespace Bearer.Models
{

    public enum EnumTypes
    {
        boolean,
        Integer,
        EmailingMethod,
        String

    }
    
    public class SetUp
    {


        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public EnumTypes Type { get; set; }

        
        public string Value { get; set; }

        

    }


}