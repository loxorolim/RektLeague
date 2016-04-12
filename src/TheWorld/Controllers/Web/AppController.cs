using System;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using System.Linq;
using Microsoft.AspNet.Authorization;
using TheWorld.ViewModels;
using AutoMapper;
using System.Net;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IWebPostRepository _repository;

        public AppController(IWebPostRepository context)
        {
            _repository = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult WebPosts(int id)
        {
            //  var web_posts = _repository.GetWebPostsInPage(id);
            var web_posts = _repository.GetWebPostsList(id);
            return View(web_posts);
        }

        public IActionResult About()
        {
            return View();
        }
        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult WritePost(WebPostViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var newWebPost = Mapper.Map<WebPost>(model);
                    _repository.AddWebPost(newWebPost);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return RedirectToAction("WebPosts", "App", new { id = 1 });
                        //return Json(Mapper.Map<WebPostViewModel>(newWebPost));
                    }
                }
                catch (Exception ex)
                {
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return RedirectToAction("WebPosts", "App", new { id = 1 });
                    // return Json(new { Message = ex.Message });
                }

            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed");
        }
        [Authorize]
        public IActionResult WritePost()
        {
            return View();
        }
        [Authorize]
        public IActionResult RemovePost(int id)
        {
            _repository.RemoveWebPostById(id);
            return RedirectToAction("WebPosts", "App", new { id = 1 });
            //return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpGet]
        public IActionResult UserSettings()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult UserSettings(SignUpViewModel model)
        {
            return View();
        }

    }

}