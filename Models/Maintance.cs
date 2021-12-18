using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aptitude.Models
{
    [Table("Maintance")]
    public class Maintance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string MaintanceDetail { get; set; }
        public string MaintanceDate { get; set; }
        public int Status { get; set; }
    }
}
