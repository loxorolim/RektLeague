using System;
using System.Collections.Generic;

namespace TheWorld.Models
{
    public class WebPost
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime PublicationDate { get; set; }
        public string MainText { get; set; }
        public string SecondaryText { get; set; }
       // public ICollection<Image> Images { get; set; }
        public string Author { get; set; }
        public string VideoURL { get; set; }
        public int Category { get; set; }
        //public ICollection<Order> DisplayOrder { get;set;}
        //public ICollection<PostText> Texts { get; set; }
        //public ICollection<PostFile> Images { get; set; }
        //public ICollection<PostFile> Gifs { get; set; }
        //public ICollection<PostFile> Videos { get; set; }
        //public ICollection<PostText> YoutubeURLs { get; set; }
        //public ICollection<PostText> DisplayOrder { get; set; }

        public virtual List<Entry> Elements { get; set; }
    }

    public class Entry
    {
        public enum EntryType
        {
            Text,
            Image,
            YoutubeURL,
        }
        public int Id { get; set; }
        public int WebPostId { get; set; }
        public virtual WebPost WebPost { get; set; }
        public EntryType ElementType { get; set; }
        public int DisplayOrder { get; set; }
        public byte[] PostBytes { get; set; }
        public string PostString { get; set; }

    }
    //public class PostFile : Entry
    //{
    //    public byte[] PostBytes { get; set; }
    //}
    //public class PostText : Entry
    //{
    //    public string PostString { get; set; }
    //}

}