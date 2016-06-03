using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;
using Microsoft.AspNet.Http;

namespace TheWorld.ViewModels
{
    public class WebPostViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(20,MinimumLength = 5)]
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;
       // [StringLength(255, MinimumLength = 5)]
        //public string MainText { get; set; }
       // public string SecondaryText { get; set; }
        public List<string> Texts { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<IFormFile> Gifs { get; set; }
        public List<IFormFile> Videos { get; set; }
        public List<string> YoutubeURLs { get; set; }
        public List<string> DisplayOrder { get; set; }

        //public string VideoURL { get; set; }
        public string Author { get; set; }
        public int Category { get; set; }
       
        
    }
}
