using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ABSA.PB.Models
{
    [Table(nameof(Phonebook))]
    public class Phonebook
    {
        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();
        //[Required]
        public string Name { get; set; }

        public List<Entry> Entries { get; set; }
    }
}