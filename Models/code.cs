using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace iSnippets.Models
{
    public class code
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [Display(Name = "code Snippet")]
        public string codeSnippet { get; set; }
        public string Language { get; set; }
    }
}
