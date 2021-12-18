using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Aptitude.Models
{
    [Table("HR")]
    public class HR
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string University { get; set; }
        public int Mark { get; set; }
        public string PersonalDetail { get; set; }
        public string Advantage { get; set; }
        public string Disadvantage { get; set; }
        public string Hobby { get; set; }
        public string Language { get; set; }
        public string Experience { get; set; }
        public string Project { get; set; }
        public string LastPostion { get; set; }
        public int Status { get; set; }
        public int ExamId { get; set; }
        public int DepartmentId { get; set; }
    }
}
