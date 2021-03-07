using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Data.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public  int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Email { get; set; }
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual List<Message> Messages{ get; set; }
    }
}
