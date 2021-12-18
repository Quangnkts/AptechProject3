using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aptitude.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+(?:-[a-zA-Z] +)*$",ErrorMessage ="Department name mustn't have number")]
        public string Name { get; set; }
        [Required]
        public string Detail { get; set; }
        [Required]
        public string Requirement { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Welfare { get; set; }
        [Required]
        [Range(100,9999999,ErrorMessage ="Salary must be higher than 100$ and not over 9,999,999")]
        public int Salary { get; set; }
    }
}
