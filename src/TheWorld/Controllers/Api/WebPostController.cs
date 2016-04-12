using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using System.Net;
using TheWorld.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.AspNet.Authorization;

namespace TheWorld.Controllers.Api
{
    [Authorize]
    [Route("api/webposts")]
    public class WebPostController : Controller
    {
        private ILogger<WebPostController> _logger;
        private IWebPostRepository _repository;
        public WebPostController(IWebPostRepository repository, ILogger<WebPostController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        [HttpGet("")]
        public JsonResult Get()
        {
            var results = Mapper.Map<IEnumerable<WebPostViewModel>>(_repository.GetAllWebPosts());
            return Json(results);
        }
        [HttpPost("")]
        public JsonResult Post([FromBody] WebPostViewModel vm)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var newWebPost = Mapper.Map<WebPost>(vm);
                    _logger.LogInformation("Attempting to save a new web post");
                    _repository.AddWebPost(newWebPost);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<WebPostViewModel>(newWebPost));
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError("Failed to save new web post", ex);
                    Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return Json(new { Message = ex.Message});
                }

            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Failed");
            
        }
    }
}
