using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld
{
    public class WebPostRepository : IWebPostRepository
    {
        private WebPostContext _context;
        private ILogger<WebPostRepository> _logger;
        
        public WebPostRepository(WebPostContext context, ILogger<WebPostRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public void AddWebPost(WebPost newWebPost)
        {
            _context.Add(newWebPost);
        }
        public void RemoveWebPostById(int id)
        {
            try
            {
                WebPost wp = new WebPost() { Id = id };
                _context.WebPosts.Attach(wp);
                _context.WebPosts.Remove(wp);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {

            }
        }
        public IEnumerable<WebPost> GetAllWebPosts()
        {
            try
            {
                return _context.WebPosts.OrderBy(t => t.PublicationDate).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get Web Posts from database", ex);
                return null;
            }
        }
        public IEnumerable<WebPost> GetWebPostsInPage(int page)
        {

            try
            {
                return _context.WebPosts.OrderBy(t => t.PublicationDate).Skip((page - 1) * Config.PageSize).Take(Config.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get Web Posts from database", ex);
                return null;
            }
        }
        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public WebPostList GetWebPostsList(int page)
        {
            try
            {
                WebPostList wpl = new WebPostList();
                wpl.WebPosts = _context.WebPosts.OrderByDescending(t => t.Id).Skip((page - 1) * Config.PageSize).Take(Config.PageSize);
                wpl.TotalWebPostsSize = _context.WebPosts.Count();
                wpl.MaxWebPostsPerSize = Config.PageSize;
                return wpl;
            }
            catch (Exception ex)
            {
                _logger.LogError("Could not get Web Posts List from database", ex);
                return null;
            }
        }
    }
}
