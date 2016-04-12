using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class WebPostList
    {
        public IEnumerable<WebPost> WebPosts { get; set; }
        public int TotalWebPostsSize { get; set; }
        public int MaxWebPostsPerSize { get; set; }
    }
}
