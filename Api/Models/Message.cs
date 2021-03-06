using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    [Table("Messages")]
    public class Message
    {
        [Key]
        public int Id { get; set; }
     
        [Required]
        public string Text { get; set; }
        [Required]
        public int SenderId { get; set; }

        public virtual User Sender { get; set; }
    }
}
