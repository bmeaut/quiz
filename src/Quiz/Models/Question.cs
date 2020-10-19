using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Quiz.Models
{
    public class Question
    {   
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(500)")]
        public string Name{ get; set; }
        [Required]
        public string Text { get; set; }
        public virtual ICollection<Answer> anwsers { get; set; }
    }
}
