using System;

namespace TheWorld.Models
{
    public class WebPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Text { get; set; }
        public string Author { get; set; }
        public string VideoURL { get; set; }


    }
}