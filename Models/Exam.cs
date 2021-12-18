using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Linq;
using System.Threading.Tasks;

namespace Aptitude.Models
{
    [Table("Exam")]
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Mark { get; set; }
        public string Result { get; set; }
        public string ExamTime { get; set; }
        public DateTime Date { get; set; }
        public string Answer { get; set; }
    }
}
