using System;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using System.Linq;
using Microsoft.AspNet.Authorization;
using TheWorld.ViewModels;
using AutoMapper;
using System.Net;
using Microsoft.AspNet.Http;
using System.IO;
using Microsoft.Net.Http.Headers;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IWebPostRepository _repository;
        private UserManager<WorldUser> _userManager;

        public AppController(IWebPostRepository context, UserManager<WorldUser> userManager)
        {
            _repository = context;
            _userManager = userManager;
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
        public async Task<IActionResult> UserSettings(UserSettingsViewModel model)
        {

            IFormFile img = model.Image;
            Stream stream = img.OpenReadStream();
            byte[] imgBytes;
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                imgBytes = memoryStream.ToArray();
            }
            WorldUser toModify = await _userManager.FindByNameAsync(User.Identity.Name);
            
            if (toModify != null)
            {
                toModify.Image = imgBytes;
                await _userManager.UpdateAsync(toModify);
                
            }


            //using (var reader = new StreamReader(img.OpenReadStream()))
            //{
            //    var fileContent = reader.ReadToEnd();
            //    var parsedContentDisposition = ContentDispositionHeaderValue.Parse(img.ContentDisposition);
            //    var fileName = parsedContentDisposition.FileName;
            //    var content = parsedContentDisposition.
            //}


            if (ModelState.IsValid)
            {
            }
            return View();
        }
        //public static byte[] CreateImage(Stream imageStream, int width, int height)
        //{
        //    Bitmap original = null;
        //    original = Bitmap.FromStream(imageStream) as Bitmap;

        //    var img = new Bitmap(width, height);

        //    using (var g = Graphics.FromImage(img))
        //    {
        //        g.SmoothingMode = SmoothingMode.AntiAlias;
        //        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        g.DrawImage(original, new Rectangle(0, 0, width, height), 0, 0, original.Width, original.Height, GraphicsUnit.Pixel);
        //    }

        //    var stream = new MemoryStream();
        //    img.Save(stream, ImageFormat.Png);

        //    return stream.ToArray();
        //}

    }

}