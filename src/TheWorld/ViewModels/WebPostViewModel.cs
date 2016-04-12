using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
    public class WebPostViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20,MinimumLength = 5)]
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;
        [StringLength(255, MinimumLength = 5)]
        public string Text { get; set; }
        public string VideoURL { get; set; }
        public string Author { get; set; }
    }
}
