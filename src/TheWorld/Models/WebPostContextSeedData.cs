using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheWorld.Models
{
	public class WebPostContextSeedData
    {
        private WebPostContext _context;
        private UserManager<WorldUser> _userManager;
        private RoleStore<IdentityRole> _roleStore; 

        public WebPostContextSeedData(WebPostContext context, UserManager<WorldUser> userManager)
        {
            _context = context;
            _userManager = userManager;
            _roleStore = new RoleStore<IdentityRole>(_context);

        }
		public async Task EnsureSeedDataAsync()
        {
            if (!_context.Roles.Any(r => r.Name == "admin"))
            {
                await _roleStore.CreateAsync(new IdentityRole { Name = "admin", NormalizedName = "admin" });
            }
            if (await _userManager.FindByEmailAsync("doge@wow.com") == null)
            {
                //Add user

                byte[] imageBytes = File.ReadAllBytes("C:\\Users\\Guilherme\\Source\\Repos\\RektLeague\\src\\TheWorld\\wwwroot\\img\\heresdoge100x100.png");


                var newUser = new WorldUser()
                {
                    UserName = "doge",
                    Email = "doge@wow.com",
                    Image = imageBytes
                    
                };              
                await _userManager.CreateAsync(newUser, "V3ry!p4ssw0rd");
                await _userManager.AddToRoleAsync(newUser, "admin");
            }
            if (await _userManager.FindByEmailAsync("gaivota@wow.com") == null)
            {
                //Add user
                var newUser = new WorldUser()
                {
                    UserName = "gaivota",
                    Email = "gaivota@wow.com"
                };
                await _userManager.CreateAsync(newUser, "V3ry!p4ssw0rd");
            }
            if (!_context.WebPosts.Any())
            {
                // Add new Data
                var WebPost1 = new WebPost()
                {
                    Title = "Primeiro Post Teste",
                    PublicationDate = DateTime.UtcNow,
                    MainText = "Olá amiguinhos!",
                    Author = "doge",
                    VideoURL = "https://www.youtube.com/embed/tmJ0tzAZ4aM",
                    //Texts = new List<PostText>{
                    //    new PostText() {PostString="Very Wow" },
                    //    new PostText() {PostString="Much lol" }
                    //}
                    
                    
                    

                };
                byte[] image = System.IO.File.ReadAllBytes("C:\\Users\\Guilherme\\Source\\Repos\\RektLeague\\src\\TheWorld\\wwwroot\\img\\heresdoge.jpg");
                byte[] gif = System.IO.File.ReadAllBytes("C:\\Users\\Guilherme\\Source\\Repos\\RektLeague\\src\\TheWorld\\wwwroot\\gif\\oraora.gif");
                byte[] video = System.IO.File.ReadAllBytes("C:\\Users\\Guilherme\\Source\\Repos\\RektLeague\\src\\TheWorld\\wwwroot\\video\\SubWooferDaZoeira.mp4");
                var WebPost2 = new WebPost()
                {
                    Title = "GET BIRDED!!!",
                    PublicationDate = DateTime.UtcNow,
                    Elements = new List<Entry>
                    {
                        new Entry() {ElementType=Entry.EntryType.Text, PostString="Very Test" },
                        new Entry() {ElementType=Entry.EntryType.Image, PostBytes = image },
                        new Entry() {ElementType=Entry.EntryType.Text, PostString="Much Wow" },
                        new Entry() {ElementType=Entry.EntryType.Image, PostBytes = gif },
                        new Entry() {ElementType=Entry.EntryType.YoutubeURL, PostString="https://www.youtube.com/embed/SRmj_VdLIL8"},
                        

                    },
                    //MainText = "Este é um post teste! Se liguem na birdada no vídeo abaixo! GET REKT!!!!!",
                    Author = "doge",
                    //VideoURL = "https://www.youtube.com/embed/SRmj_VdLIL8"
                };
                //var WebPost3 = new WebPost()
                //{
                //    Title = "NOSSA SINHOOOOOOOORA!",
                //    PublicationDate = DateTime.UtcNow,
                //    MainText = "Viradinha BR épica!",
                //    Author = "doge",
                //    VideoURL = "https://www.youtube.com/embed/WNDabV23cRo"
                //};
                //var WebPost4 = new WebPost()
                //{
                //    Title = "DOUBLE SAVURUUUUUU",
                //    PublicationDate = DateTime.UtcNow,
                //    MainText = "BORA CUMPADI!",
                //    Author = "doge",
                //    VideoURL = "https://www.youtube.com/embed/vLiv3yabEjQ"
                //};
                //var WebPost5 = new WebPost()
                //{
                //    Title = "DIBRE DUPLO",
                //    PublicationDate = DateTime.UtcNow,
                //    MainText = "GET DIBRED",
                //    Author = "doge",
                //    VideoURL = "https://www.youtube.com/embed/5O0Fj6gmkIs"
                //};
                //var WebPost6 = new WebPost()
                //{
                //    Title = "TOP GOALS!",
                //    PublicationDate = DateTime.UtcNow,
                //    MainText = "Se liguem nesses TOP GOALS!",
                //    Author = "doge",
                //    VideoURL = "https://www.youtube.com/embed/DKhAGtosZgo"
                //};
                //var WebPost7 = new WebPost()
                //{
                //    Title = "GET PELICANED",
                //    PublicationDate = DateTime.UtcNow,
                //    MainText = "RANGARIA!",
                //    Author = "doge",
                //    VideoURL = "https://www.youtube.com/embed/0b4TU_R7J3c"
                //};


                _context.WebPosts.Add(WebPost1);

                try
                {
                    _context.WebPosts.Add(WebPost2);
                    _context.Entries.AddRange(WebPost2.Elements);
                }
                catch(Exception ex)
                {

                }

                //_context.WebPosts.Add(WebPost3);
                //_context.WebPosts.Add(WebPost4);
                //_context.WebPosts.Add(WebPost5);
                //_context.WebPosts.Add(WebPost6);
                //_context.WebPosts.Add(WebPost7);

                _context.SaveChanges();
            }
           
        }
    }
}