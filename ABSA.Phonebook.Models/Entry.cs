using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ABSA.PB.Models
{
    [Table(nameof(Entry))]
    public class Entry
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();
        //[Required]
        [ForeignKey("PhoneBookID")]
        public Guid PhoneBookID { get; set; }
        //[Required]

        public string Name { get; set; }
        //[Required]
        public string PhoneNumber { get; set; }
    }
}