using System.Collections.Generic;
using TheWorld.Models;

namespace TheWorld
{
    public interface IWebPostRepository
    {
        
        IEnumerable<WebPost> GetAllWebPosts();
        IEnumerable<WebPost> GetWebPostsInPage(int page);
        WebPost GetWebPostById(int id);
        WebPostList GetWebPostsList(int page);
        void AddWebPost(WebPost newWebPost);
        void RemoveWebPostById(int id);
        bool SaveAll();
    }
}