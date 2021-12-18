using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aptitude.Models
{
    [Table("CV")]
    public class CV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        public string University { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        [Range(0, maximum:100, ErrorMessage = "The input points from 0 to 100")]
        public int Mark { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        public string PersonalDetail { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        public string Advantage { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        public string Disadvantage { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        public string Hobby { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        public string Language { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        public string Experience { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        public string Project { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        public string LastPostion { get; set; }
        [Required(ErrorMessage = "You cannot leave this field blank")]
        public DateTime DateCV { get; set; }
        public int Status { get; set; }
        public int ExamId { get; set; }
        public int DepartmentId { get; set; }
    }

}
